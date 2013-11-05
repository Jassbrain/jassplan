using Jassplan.Model;
using Jassplan.ModelManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Jassplan.ModelManager
{
    public class JassModelManager: IDisposable
    {
        private JassContext db = new JassContext();

        #region Area Model API
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

        #endregion Activity Model API

        #region Activity Model API
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

        #region Area X Model API

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
        public List<JassActivityLog> ActivityLogsGetAll()
        {
            return db.JassActivityLogs.ToList<JassActivityLog>();
        }

        public JassActivityLog ActivityLogGetById(int id)
        {
            var JassActivityLog = db.JassActivityLogs.Find(id);
            return JassActivityLog;
        }

        public void ActivityLogCreate(JassActivityLog ActivityLog)
        {
            db.JassActivityLogs.Add(ActivityLog);
            db.SaveChanges();
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

        protected virtual void Dispose(bool flag){
            db.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}