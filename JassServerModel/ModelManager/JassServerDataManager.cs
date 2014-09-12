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
    public class JassDataManager: IDisposable
    {
        private JassContext _db = new JassContext();
        private string _username;

            public JassDataManager(string username)
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

            #endregion Single User Ilusion Layer

            public JassActivityReview addActivityReview(DateTime doneDate2) {

                var review = new JassActivityReview();
                _db.JassActivityReviews.Add(review);
                review.ReviewDate = DateTime.Now;
                review.ReviewYear = doneDate2.Year;
                review.ReviewMonth = doneDate2.Month;
                review.ReviewDay = doneDate2.Day;

                _db.SaveChanges();
                return review;
            }

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