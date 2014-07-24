using Jassplan.Model;
using Jassplan.JassServerModelManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Reflection;
using JassTools;

namespace Jassplan.JassServerModelManager
{
    public class JassModelManager: IDisposable
    {
        private JassContext db = new JassContext();

            public JassModelManager()
            {
            }

        #region Activity Model API

        //activity is the primary object, we track them and assignthem to, at least, one area.
        //in thefuture will be assinged to more than one area.
        //activities represent a current state, when an activity is done it can be archived
        //and they will go to the activities log that will be used as a primary source of tracking
        //somehow, the activity log is a fact table.

        public List<JassActivity> ActivitiesGetAll()
        {
            var allActivities = db.JassActivities.OrderBy(ac => ac.title).ToList<JassActivity>();
            foreach (var activity in allActivities) {
                if (activity.ActualDuration == null) activity.ActualDuration = activity.EstimatedDuration;
            }
            return allActivities;
        }

        public List<JassActivity> ActivitiesArchiveGetAll()
        {
            var activities =  db.JassActivities.ToList<JassActivity>();
            foreach (var activity in activities)
            {
                if (activity.Status == "done")
                {
                    activity.Status = "doneArchived";
                    ActivitySave(activity);
                }

                activity.Status="asleep";
                ActivitySave(activity);
            }
            return ActivitiesGetAll();
        }

        public JassActivity ActivityGetById(int id)
        {
            var JassActivity = db.JassActivities.Find(id);
            return JassActivity;
        }
        public int ActivityCreate(JassActivity Activity)
        {
            db.JassActivities.Add(Activity);
            Activity.Created = DateTime.Now;
            Activity.dateCreated = DateTime.Now;
            Activity.LastUpdated = DateTime.Now;
            db.SaveChanges();
            ActivitySaveHistory(Activity);
            return Activity.JassActivityID;
        }

        private void ActivitySaveHistory(JassActivity activity)
        {
            JassActivityHistory activityHistory = new JassActivityHistory();
            var mapper = new JassCommonAttributesMapper<JassActivityCommon, JassActivity, JassActivityHistory>();
            mapper.map(activity, activityHistory);
            activityHistory.JassActivityID = activity.JassActivityID;

            ActivityHistoryCreate(activityHistory);
        }

        public void ActivitySave(JassActivity Activity)
        {
            if (Activity.Status == null) Activity.Status = "asleep";
            db.Entry(Activity).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void ActivityDelete(int id)
        {
            JassActivity JassActivity = this.ActivityGetById(id);
            db.JassActivities.Remove(JassActivity);
            db.SaveChanges();
        }

        #endregion Activity Model API

        #region ActivityHistory Model API

        //activity is the primary object, we track them and assignthem to, at least, one area.
        //in thefuture will be assinged to more than one area.
        //activities represent a current state, when an activity is done it can be archived
        //and they will go to the activities log that will be used as a primary source of tracking
        //somehow, the activity log is a fact table.

        public List<JassActivityHistory> ActivityHistoriesGetAll()
        {
            return db.JassActivityHistories.ToList<JassActivityHistory>();
        }
        public JassActivityHistory ActivityHistoryGetById(int id)
        {
            var JassActivityHistory = db.JassActivityHistories.Find(id);
            return JassActivityHistory;
        }
        public int ActivityHistoryCreate(JassActivityHistory ActivityHistory)
        {
            db.JassActivityHistories.Add(ActivityHistory);
            ActivityHistory.TimeStamp= DateTime.Now;
            ActivityHistory.Created = DateTime.Now;
            ActivityHistory.LastUpdated = DateTime.Now;
            
            db.SaveChanges();
            return ActivityHistory.JassActivityHistoryID;
        }
        public void ActivityHistorySave(JassActivityHistory ActivityHistory)
        {
            db.Entry(ActivityHistory).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void ActivityHistoryDelete(int id)
        {
            JassActivityHistory JassActivityHistory = this.ActivityHistoryGetById(id);
            db.JassActivityHistories.Remove(JassActivityHistory);
            db.SaveChanges();
        }

        #endregion ActivityHistory Model API


        #region IDispose
        protected virtual void Dispose(bool flag){
            db.Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion IDispose
    }
}