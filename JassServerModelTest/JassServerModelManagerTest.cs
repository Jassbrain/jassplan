using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jassplan.JassServerModelManager;
using Jassplan.Model;
using System.Collections.Generic;
using System.Reflection;
using JassTools;

namespace Jassplan.Tests.ModelManager
{
    [TestClass] //Test of the Model Manager
    public class JassServerModelManagerTest
    {
        IJassDataModelManager mm = new JassDataModelManager("test");

        [TestInitialize()]
        public void Initialize() {

            mm.ActivityDeleteAll();
            var numberOfActivities = mm.ActivitiesGetAll().Count;
            var numberOfActivityReviews = mm.ActivityReviewsGetAll().Count;
            Assert.AreEqual(numberOfActivities, 0);
        }

        [TestMethod]
        public void mmActivitiesCRUD()
        {
            //Purpose: basic test of all CRUD operations on JassActivity       
            var Activities0 = mm.ActivitiesGetAll();
            // Get all Activities and Reviews and make sure they are 0 - done bofore but just in case

            var numberOfActivities = mm.ActivitiesGetAll().Count;
            var numberOfActivityReviews = mm.ActivityReviewsGetAll().Count;
            Assert.AreEqual(numberOfActivities, 0);
            Assert.AreEqual(numberOfActivityReviews, 0);

            // Create an Activity and verify previous numbers change accordingly and 
            JassActivity newActivity0 = CreateSampleRootActivity(0);

            string newActivity0StringBefore = ToStringWithExceptions(newActivity0,null);

            int newActivity0Id = mm.ActivityCreate(newActivity0);

            string newActivity0StringAfter = ToStringWithExceptions(newActivity0, null);

            Assert.AreEqual(newActivity0StringBefore, newActivity0StringAfter);

            refreshManager(); 

            //let's verify that the activity created is equivalente to the activity suplied

            //we verify that the new get all includes this Activity
            List<JassActivity> Activities1 = mm.ActivitiesGetAll();
            Assert.IsTrue(Activities1.Count == Activities0.Count + 1);

            //we can get it back
            JassActivity newActivity1 = mm.ActivityGetById(newActivity0Id);

            Assert.IsFalse(newActivity0 == newActivity1);  //this is to make sure are refreshing the context

            //and both are equal
            AssertEqualActivities(newActivity0, newActivity1); //however, the Activities should be the same

            //Now, we change Activity 2
            newActivity1.Name = "TestActivity1";

            //we save the changes
            mm.ActivitySave(newActivity1, true);

            refreshManager(); //

            //and we get it back
            JassActivity newActivity2 = mm.ActivityGetById(newActivity1.JassActivityID);

            Assert.IsFalse(newActivity1 == newActivity2);  //this is to make sure are refreshing the context

            //and both are equal
            AssertEqualActivities(newActivity1, newActivity2);


            //finally we delete the created Activity
            mm.ActivityDelete(newActivity0Id);

            //and we verify that get all remains same.
            List<JassActivity> Activities2 = mm.ActivitiesGetAll();
            Assert.IsTrue(Activities2.Count == Activities0.Count);

        }

        [TestMethod]
        public void mmActivityReviews()
        {
            Assert.AreEqual(true, false);
        }


        public string ToStringWithExceptions(JassActivity activity, List<string> Exceptions)
        {
            var activityProps = typeof(JassActivity).GetProperties();
            string result="";
            foreach (PropertyInfo activityProperty in activityProps)
            {
                if (true)
                {
                    var value0 = activityProperty.GetValue(activity);
                    if (value0 != null)
                    {
                        result += value0.ToString();
                    }
                }
            }
            return result;
        }

        public void AssertEqualActivities(JassActivity activity0, JassActivity activity1)
        {
            var activityProps = typeof(JassActivity).GetProperties();

            foreach (PropertyInfo activityProperty in activityProps)
            {
                if (true)
                {
                    var value0 = activityProperty.GetValue(activity0);
                    var value1 = activityProperty.GetValue(activity1);

                    if (value0 != null)
                    {
                        value0 = value0.ToString();
                        value1 = value1.ToString();
                    }

                    Assert.AreEqual(value0, value1);
                }
            }
        }

        public JassActivity CreateSampleRootActivity(int token)
        {
            var newActivity = new JassActivity()
            {
                ParentID = null,
                Name = "Name" + token,
                Description = "Description" + token,
                title = "title" + token,
                narrative = "narrative" + token,
                Status = "sleep",
                Flag = "white",
                dateCreated = DateTime.Now,
                EstimatedDuration = 1,
                EstimatedStartHour = 9,
                ActualDuration = 1,
                TodoToday = false,
                DoneToday = false,
                DoneDate = null
            };

            return newActivity;
        }

        public void refreshManager()
        {
            mm.Dispose();
            mm = new JassDataModelManager("test");
        }

    }
}
