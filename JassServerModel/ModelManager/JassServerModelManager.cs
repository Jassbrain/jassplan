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
using JassServerModel.Model;

namespace Jassplan.JassServerModelManager
{
    public class JassModelManager: IDisposable
    {
        private JassContext _db = new JassContext();

        private string _username;

            public JassModelManager(string username)
            {
                _username = username;
            }


            #region Single User Ilusion Layer
            /*
         * This set of methods produce an abstraction to let the API 
         * think that this is a single user database as it was in the origins
         */

            public IQueryable<JassActivity> myActivities()
            {
                return _db.JassActivities.Where(ac => ac.UserName == _username);
            }

            public IQueryable<JassActivityReview> myActivityReviews()
            {
                return _db.JassActivityReviews.Where(ac => ac.UserName == _username);
            }

            public IQueryable<JassActivityHistory> myActivityHistories()
            {
                return _db.JassActivityHistories.Where(ac => ac.UserName == _username);
            }

            public void checkIfMine(IUserOwned activity)
            {
                if (activity.UserName != _username) throw new Exception("id does not belong to user");
            }

            #endregion Single User Ilusion Layer

        #region Activity Model API

        //activity is the primary object, we track them and assignthem to, at least, one area.
        //in thefuture will be assinged to more than one area.
        //activities represent a current state, when an activity is done it can be archived
        //and they will go to the activities log that will be used as a primary source of tracking
        //somehow, the activity log is a fact table.

        public List<JassActivity> ActivitiesGetAll()
        {
            var allActivities = myActivities().OrderBy(ac => ac.title).ToList<JassActivity>();
            foreach (var activity in allActivities) {
                if (activity.ActualDuration == null) activity.ActualDuration = activity.EstimatedDuration;
            }
            return allActivities;
        }

        public List<JassActivityReview> ActivityReviewsGetAll()
        {
            
            var allActivityReviews = myActivityReviews().OrderByDescending(ac => ac.ReviewDate).ToList<JassActivityReview>();
            foreach (var review in allActivityReviews)
            {
                var allActivityHistories = myActivityHistories().Where(h=>h.JassActivityReviewID==review.JassActivityReviewID).OrderBy(h=>h.title).ToList<JassActivityHistory>();
                review.ActivityHistories = allActivityHistories; 
            }
            
            return allActivityReviews;
        }

        public List<JassActivity> ActivitiesArchiveGetAll()
        {
            var activities =  myActivities().ToList<JassActivity>();

            //before anything we find out the 'done day' of this task list

            DateTime? doneDate=null;
            foreach (var activity in activities)
            {
                if (doneDate == null && activity.DoneDate != null)
                {
                    doneDate = activity.DoneDate;
                }
            }

            if (doneDate == null) return ActivitiesGetAll();

            //firt of all let's check some conditions
            //1. for now only one review a day
            DateTime doneDate2 = (DateTime)doneDate;
            var now = DateTime.Now;
            var existingReview = myActivityReviews().Where(r => r.ReviewYear == doneDate2.Year &&
                r.ReviewMonth == doneDate2.Month &&
                r.ReviewDay == doneDate2.Day).ToList();

            if (existingReview.Count>0) return ActivitiesGetAll();

            //2. Now, let's make sure all scheduled tasks are done or doneplus

            int tasks = 0; int stared = 0; int done = 0; int doneplus = 0;
            foreach (var activity in activities)
            {
                tasks++;
                if (activity.Status == "stared") { 
                    stared++; };
                if (activity.Status == "done") done++;
                if (activity.Status == "doneplus") doneplus++;
            }

            //we will only accept a review if something is done and nothing is still stared

           // if ( (stared > 0) || (done + doneplus == 0) )return ActivitiesGetAll();

            var review = new JassActivityReview();
            _db.JassActivityReviews.Add(review);
            review.UserName = _username;
            review.ReviewDate = DateTime.Now;
            review.ReviewYear = doneDate2.Year;
            review.ReviewMonth = doneDate2.Month;
            review.ReviewDay = doneDate2.Day;

            _db.SaveChanges();

            foreach (var activity in activities)
            {
                var activityHistory = ActivitySave(activity,review);
                activity.Status="asleep";
                activity.DoneDate = null;
                ActivitySave(activity);
            }
            return ActivitiesGetAll();
        }

        public JassActivity ActivityGetById(int id)
        {
            var JassActivity = _db.JassActivities.Find(id);
            checkIfMine(JassActivity);
            return JassActivity;
        }
        public int ActivityCreate(JassActivity Activity)
        {
            _db.JassActivities.Add(Activity);
            Activity.UserName =_username;
            Activity.Created = DateTime.Now;
            Activity.dateCreated = DateTime.Now;
            Activity.LastUpdated = DateTime.Now;
            _db.SaveChanges();
            ActivitySaveHistory(Activity);
            return Activity.JassActivityID;
        }

        private JassActivityHistory ActivitySaveHistory(JassActivity activity)
        {
            JassActivityHistory activityHistory = new JassActivityHistory();
            var mapper = new JassCommonAttributesMapper<JassActivityCommon, JassActivity, JassActivityHistory>();
            mapper.map(activity, activityHistory);
            activityHistory.JassActivityID = activity.JassActivityID;
            ActivityHistoryCreate(activityHistory);
            return activityHistory;
        }

        private JassActivityHistory ActivitySaveHistory(JassActivity activity, JassActivityReview review)
        {
            JassActivityHistory activityHistory = new JassActivityHistory();
            var mapper = new JassCommonAttributesMapper<JassActivityCommon, JassActivity, JassActivityHistory>();
            mapper.map(activity, activityHistory);
            activityHistory.JassActivityID = activity.JassActivityID;
            activityHistory.JassActivityReviewID = review.JassActivityReviewID;
            ActivityHistoryCreate(activityHistory);
            return activityHistory;
        }

        public JassActivityHistory ActivitySave(JassActivity Activity)
        {
            
            JassActivity ActivityCurrent = _db.JassActivities.Find(Activity.JassActivityID);
            checkIfMine(ActivityCurrent);
            if (Activity.Status == null) Activity.Status = "asleep";

            if ((Activity.DoneDate ==null) &&
                (ActivityCurrent.Status == "asleep" ||
                 ActivityCurrent.Status == "stared" ||
                 ActivityCurrent.Status == null) && 
                (Activity.Status == "done")
                ){
                Activity.DoneDate = DateTime.Now;
            }
            Activity.LastUpdated = DateTime.Now;
            //db.Entry(Activity).State = EntityState.Modified;

            var mapper = new JassCommonAttributesMapper<JassActivityCommon, JassActivity, JassActivity>();
            mapper.map(Activity, ActivityCurrent);

            _db.Entry(ActivityCurrent).State = EntityState.Modified;
            _db.SaveChanges();
            var activityHistory = ActivitySaveHistory(Activity);
            return activityHistory;
        }

        public JassActivityHistory ActivitySave(JassActivity Activity, JassActivityReview review)
        {
            checkIfMine(Activity); checkIfMine(review); 
            if (Activity.Status == null) Activity.Status = "asleep";
            Activity.LastUpdated = DateTime.Now;
            _db.Entry(Activity).State = EntityState.Modified;
            _db.SaveChanges();
            var activityHistory = ActivitySaveHistory(Activity, review);
            return activityHistory;
        }
        public void ActivityDelete(int id)
        {
            JassActivity JassActivity = this.ActivityGetById(id);
            checkIfMine(JassActivity);
            _db.JassActivities.Remove(JassActivity);
            _db.SaveChanges();
        }

        public void ActivityDeleteAll()
        {
            if (_username != "test") { throw new Exception("Delete all is only for test user"); };
            foreach (var activity in myActivities())
            {
                _db.JassActivities.Remove(activity);
            }
            _db.SaveChanges();
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
            return myActivityHistories().ToList<JassActivityHistory>();
        }
        public JassActivityHistory ActivityHistoryGetById(int id)
        {
            var JassActivityHistory = _db.JassActivityHistories.Find(id);
            checkIfMine(JassActivityHistory);
            return JassActivityHistory;
        }
        public int ActivityHistoryCreate(JassActivityHistory ActivityHistory)
        {
            _db.JassActivityHistories.Add(ActivityHistory);
            ActivityHistory.UserName = _username;
            ActivityHistory.TimeStamp= DateTime.Now;
            ActivityHistory.Created = DateTime.Now;
            ActivityHistory.LastUpdated = DateTime.Now;
            
            _db.SaveChanges();
            return ActivityHistory.JassActivityHistoryID;
        }
        public void ActivityHistorySave(JassActivityHistory ActivityHistory)
        {
            checkIfMine(ActivityHistory);
            _db.Entry(ActivityHistory).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void ActivityHistoryDelete(int id)
        {
            JassActivityHistory JassActivityHistory = this.ActivityHistoryGetById(id);
            checkIfMine(JassActivityHistory);
            _db.JassActivityHistories.Remove(JassActivityHistory);
            _db.SaveChanges();
        }

        #endregion ActivityHistory Model API


        #region IDispose
        protected virtual void Dispose(bool flag){
            _db.Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion IDispose
    }
}