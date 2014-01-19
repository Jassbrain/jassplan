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
        JassModelManager mm = new JassModelManager();

        [TestMethod]
        public void mmActivitiesCRUD()
        {
            //Purpose: basic test of all CRUD operations on JassActivity       
            
            DBClean();

            //see if we can get all activities
            List<JassActivity> Activities0 = mm.ActivitiesGetAll();

            // Ok, now we have the are and we can crate the activity
            JassActivity newActivity0 = new JassActivity();
            newActivity0.Name = "TestActivity0";
            int newActivity0Id = mm.ActivityCreate(newActivity0);

            refreshManager(); //

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
            mm.ActivitySave(newActivity1);

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

            DBCleanStateCheck();
        }


        [TestMethod]
        public void mmActivitiesHistory()
        {
            /* 
             The purpose of this test is to verify that after any CRUD operation is
             * performed on an Activity the corresponding previouis state is stored in the 
             * history. The idea of the test is simple, I create an area and test that the
             * history is created and make sense. The I update the area and check the history again.
             */


            DBClean();  //we clean the DB

            var startTime = DateTime.Now;

            //Create an activity and verify that we have got a history

    

            JassActivity newActivity = new JassActivity();
            newActivity.Name = "TestActivity0";
            int newActivityId = mm.ActivityCreate(newActivity);

            //So at least ewe need one record in this activityu history
            var allActivityHistories = mm.ActivityHistoriesGetAll();

            Assert.IsTrue(allActivityHistories.Count == 1);

       //now , let's really verify that the history record is what is supposed to be

            var activityHistory = allActivityHistories[0];//we only have one so..
            var activity = mm.ActivityGetById(newActivityId);

            var mapper = new JassCommonAttributesMapper<JassActivityCommon, JassActivity, JassActivityHistory>();

            var result = mapper.compare(activity, activityHistory);

            Assert.IsTrue(result); //this test should habe been enough
            //but just to be paranoid I will add some test manually as well not all fields.. but just a few

            Assert.IsTrue(activityHistory.Description == activity.Description);
            Assert.IsTrue(activityHistory.Name == activity.Name);
            Assert.IsTrue(activityHistory.TimeStamp < DateTime.Now);
            Assert.IsTrue(activityHistory.TimeStamp > startTime);

            Assert.IsTrue(activityHistory.JassActivityID == activity.JassActivityID);

            //at this point that is pointing to the right area history.

            //So, the idea is that is I ask this activity historu about this area history
            //I wil get an area history who original are is the are i created in the beginning of the test



/*
            var startTime2 = DateTime.Now;

            //Now we will update that area and verify that the history is saved again.

            newActivity.Name += "X";
            newActivity.Description += "X";

            mm.AreaSave(newActivity);

            var allAreaHistories2 = mm.AreaHistoriesGetAll();


            Assert.IsTrue(allAreaHistories2.Count == 2);

            var areaHistory2 = allAreaHistories2[1];
            var area2 = mm.AreaGetById(newArea0Id);

            var mapper2 = new JassProperties<JassAreaCommon, JassArea, JassAreaHistory>();

            var result2 = mapper.Compare(area, areaHistory);

            Assert.IsTrue(result);

            Assert.IsTrue(areaHistory2.Description == area.Description);
            Assert.IsTrue(areaHistory2.Name == area.Name);
            Assert.IsTrue(areaHistory2.TimeStamp < DateTime.Now);
            Assert.IsTrue(areaHistory2.TimeStamp > startTime2);

            DBClean();  //we clean the DB
            DBCleanStateCheck(); //we make sure the DB is clean again
*/
        }

        [TestMethod]
        public void mmActivityHistoriesCRUD()

        {
            //Purpose: basic test of all CRUD operations on JassActivityHistories       

            DBClean();

            //see if we can get all activities
            List<JassActivityHistory> ActivityHistories0 = mm.ActivityHistoriesGetAll();

            //Now we can create the activity history
            JassActivityHistory newActivityHistory0 = new JassActivityHistory();
            newActivityHistory0.Name = "TestActivityHistory0";

            int newActivityHistory0Id = mm.ActivityHistoryCreate(newActivityHistory0);

            refreshManager(); //

            //we verify that the new get all includes this Activity
            List<JassActivityHistory> ActivityHistories1 = mm.ActivityHistoriesGetAll();
            Assert.IsTrue(ActivityHistories1.Count == ActivityHistories0.Count + 1);

            //we can get it back
            JassActivityHistory newActivityHistory1 = mm.ActivityHistoryGetById(newActivityHistory0Id);

            Assert.IsFalse(newActivityHistory0 == newActivityHistory1);  //this is to make sure are refreshing the context

            //and both are equal
            AssertEqualActivityHistories(newActivityHistory0, newActivityHistory1); //however, the ActivityHistories should be the same

            //Now, we change Activity 2
            newActivityHistory1.Name = "TestActivity1";

            //we save the changes
            mm.ActivityHistorySave(newActivityHistory1);

            refreshManager(); //

            //and we get it back
            JassActivityHistory newActivityHistory2 = mm.ActivityHistoryGetById(newActivityHistory1.JassActivityHistoryID);

            Assert.IsFalse(newActivityHistory1 == newActivityHistory2);  //this is to make sure are refreshing the context

            //and both are equal
            AssertEqualActivityHistories(newActivityHistory1, newActivityHistory2);


            //finally we delete the created Activity
            mm.ActivityHistoryDelete(newActivityHistory0Id);

            //And we also delete the created AreaHistory

            List<JassActivityHistory> ActivityHistories2 = mm.ActivityHistoriesGetAll();
            Assert.IsTrue(ActivityHistories2.Count == ActivityHistories0.Count);

            DBCleanStateCheck();
        }


        public void AssertEqualActivities(JassActivity activity0, JassActivity activity1)
        {
            var activityProps = typeof(JassActivity).GetProperties();

            foreach (PropertyInfo activityProperty in activityProps)
            {
                if (!activityProperty.GetMethod.IsVirtual
                    && activityProperty.Name!="LastUpdated")
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

        public void AssertEqualActivityHistories(JassActivityHistory activity0, JassActivityHistory activity1)
        {
            var activityProps = typeof(JassActivityHistory).GetProperties();

            foreach (PropertyInfo activityProperty in activityProps)
            {
                if (!activityProperty.GetMethod.IsVirtual
                    && activityProperty.Name != "LastUpdated")
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

        public void DBCleanStateCheck()
        {
            List<JassActivity> activities = mm.ActivitiesGetAll();
            Assert.IsTrue(activities.Count == 0);
        }

        public void DBClean()
        {
                    
            List<JassActivity> activities = mm.ActivitiesGetAll();
            foreach(JassActivity activity in activities){ mm.ActivityDelete(activity.JassActivityID);};

            List<JassActivityHistory> activityHistories = mm.ActivityHistoriesGetAll();
            foreach (JassActivityHistory activity in activityHistories) { mm.ActivityHistoryDelete(activity.JassActivityHistoryID); };

            DBCleanStateCheck();

        }

        public void refreshManager()
        {
            mm.Dispose();
            mm = new JassModelManager();
        }

    }
}
