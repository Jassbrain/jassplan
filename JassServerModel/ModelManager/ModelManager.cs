using Jassplan.Model;
using Jassplan.ModelManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Reflection;
using JassTools;

namespace Jassplan.ModelManager
{
    public class JassModelManager: IDisposable
    {
        private JassContext db = new JassContext();

            public JassModelManager()
            {
            }

        #region Area Model API
        //and are is a pretty static object, that may change once in a while 
        //and represents a primary focus subject like 'mind', 'body',.etc
        //areas will be a primary source of measure. In the future and activity 
        //may impact more than one area but there will alwasy be a primary area.
 
        public List<JassArea> AreasGetAll()
        {
            return db.JassAreas.ToList<JassArea>();
        }
        public JassArea AreaGetById(int id){
            var JassArea = db.JassAreas.Find(id);
            return JassArea;
        }
        public int AreaCreate(JassArea area)
        {
            db.JassAreas.Add(area);
            db.SaveChanges();
            AreaSaveHistory(area);
            return area.JassAreaID;
        }

        private void AreaSaveHistory(JassArea area)
        {
            JassAreaHistory areaHistory = new JassAreaHistory();
            var mapper = new JassProperties<JassAreaCommon, JassArea, JassAreaHistory>();
            mapper.map(area, areaHistory);
            areaHistory.JassAreaKey = area.JassAreaID;
            AreaHistoryCreate(areaHistory);

        }
        public void AreaSave(JassArea area)
        {
            db.Entry(area).State = EntityState.Modified;
            db.SaveChanges();
            AreaSaveHistory(area);
        }
        public void AreaDelete(int id)
        {
            JassArea Jassarea = this.AreaGetById(id);
            db.JassAreas.Remove(Jassarea);
            db.SaveChanges();
        }

        #endregion Area Model API

        #region AreaHistory Model API
        //and are is a pretty static object, that may change once in a while 
        //and represents a primary focus subject like 'mind', 'body',.etc
        //areas will be a primary source of measure. In the future and activity 
        //may impact more than one area but there will alwasy be a primary area.

        public List<JassAreaHistory> AreaHistoriesGetAll()
        {
            return db.JassAreaHistories.ToList<JassAreaHistory>();
        }
        public JassAreaHistory AreaHistoryGetById(int id)
        {
            var JassAreaHistory = db.JassAreaHistories.Find(id);
            return JassAreaHistory;
        }
        public int AreaHistoryCreate(JassAreaHistory area)
        {
            db.JassAreaHistories.Add(area);
            area.TimeStamp = DateTime.Now;
            db.SaveChanges();
            return area.JassAreaHistoryID;
        }
        public void AreaHistorySave(JassAreaHistory area)
        {
            db.Entry(area).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void AreaHistoryDelete(int id)
        {
            JassAreaHistory Jassarea = this.AreaHistoryGetById(id);
            db.JassAreaHistories.Remove(Jassarea);
            db.SaveChanges();
        }

        #endregion AreaHistory Model API

        #region Activity Model API

        //activity is the primary object, we track them and assignthem to, at least, one area.
        //in thefuture will be assinged to more than one area.
        //activities represent a current state, when an activity is done it can be archived
        //and they will go to the activities log that will be used as a primary source of tracking
        //somehow, the activity log is a fact table.

        public List<JassActivity> ActivitiesGetAll()
        {
            return db.JassActivities.ToList<JassActivity>();
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
            Activity.LastUpdated = DateTime.Now;
            db.SaveChanges();
            return Activity.JassActivityID;
        }
        public void ActivitySave(JassActivity Activity)
        {
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

        #region List Model API

        //a list is a kind of area (actually is a subclass) but is dynamic in nature.
        //This is going to evolve and become a very generic mechanism butfor the moment 
        //we only have one method that will give us the list of tasks to-do today.
        //which is defined as the set of all activityes with a 'todo' true for today.
        public JassList ListTodoTodayGet()
        {
            var Jassactivities = db.JassActivities.Where(a => a.TodoToday == true).ToList();
            JassList JassTodayList = new JassList();
            JassTodayList.Activities = Jassactivities.ToList();
            JassTodayList.Name = "Today";
            JassTodayList.JassAreaID = 0;

            return JassTodayList;
        }

        #endregion


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