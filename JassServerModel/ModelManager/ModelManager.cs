using Jassplan.Model;
using Jassplan.ModelManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Reflection;

namespace Jassplan.ModelManager
{
    public class JassModelManager: IDisposable
    {
        private JassContext db = new JassContext();

        #region Refleccion on Activities and ActivityLogs

            private PropertyInfo[] activityProps = typeof(JassActivity).GetProperties();
            private PropertyInfo[] activityLogProps = typeof(JassActivityLog).GetProperties();
            Dictionary<string, PropertyInfo> activityLogPropDict = new Dictionary<string, PropertyInfo>();
            PropertyInfo logProperty;
        #endregion

            public JassModelManager()
            {
                foreach (PropertyInfo activityLogProp in activityLogProps)
                { activityLogPropDict.Add(activityLogProp.Name, activityLogProp); }
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
            return area.JassAreaID;
        }
        public void AreaSave(JassArea area)
        {
            db.Entry(area).State = EntityState.Modified;
            db.SaveChanges();
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

        #region Activity Log Model API
        //The activity log is a central object and will used to store the fact table of 
        //completed tasks. This log is carefully filled by a specific action 'archive' that will
        //check certain rules before archiving a day. I esepcualte that maybe in the future 
        //I will have a few different ways to archive, the initial way is based on a completed day.
        //i want to enforce certain criteria to avoid having a funky log and filter strange things 
        //like a task marked todo but then remarked not done by mistake.
        //the log can still be modified but this process will be more cumbersome and with rules to avoid
        //trashing the log.

        public List<JassActivityLog> ActivityLogsGetAll()
        {
            return db.JassActivityLogs.ToList<JassActivityLog>();
        }
        public JassActivityLog ActivityLogGetById(int id)
        {
            var JassActivityLog = db.JassActivityLogs.Find(id);
            return JassActivityLog;
        }
        public int ActivityLogCreate(JassActivity activity)
        {
            JassActivityLog activityLog = new JassActivityLog();
            foreach (PropertyInfo activityProperty in activityProps)
            {
                if (!activityProperty.GetMethod.IsVirtual)
                {
                    var result = activityLogPropDict.TryGetValue(activityProperty.Name, out logProperty);
                    var value = activityProperty.GetValue(activity);
                    logProperty.SetValue(activityLog, value);
                }

            }
            activityLog.Logged = DateTime.Now;
            
            db.JassActivityLogs.Add(activityLog);
            db.SaveChanges();
            return activityLog.JassActivityLogID;
        }
        public void ActivityLogSave(JassActivityLog ActivityLog)
        {
            db.Entry(ActivityLog).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void ActivityLogDelete(int id)
        {
            JassActivityLog JassActivityLog = this.ActivityLogGetById(id);
            db.JassActivityLogs.Remove(JassActivityLog);
            db.SaveChanges();
        }

        #endregion AcivityLog Model API

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