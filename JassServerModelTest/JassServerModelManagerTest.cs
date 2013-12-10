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
        public void mmAreasCRUD()
        {
            //Purpose: basic test of all CRUD operations on JassArea       
            DBClean();
            //Get all Areas
            List<JassArea> areas0 = mm.AreasGetAll();

            //Crete an area
            JassArea newArea0 = new JassArea();
            newArea0.Name = "TestArea0";
            newArea0.Activities = new List<JassActivity>();

            int newArea0Id = mm.AreaCreate(newArea0);

            refreshManager(); //to make sure we can read from DB

            //now, we should get one more area
            List<JassArea> areas1 = mm.AreasGetAll();
            Assert.IsTrue(areas1.Count == areas0.Count + 1);

            //we can get the area back back
            JassArea newArea1 = mm.AreaGetById(newArea0Id);
            //Just being paranoid, we check that the object area actually different
            Assert.IsFalse(newArea0 == newArea1);  //this is to make sure are refreshing the context

            //but the areas area actually the same
            AssertEqualAreas(newArea0, newArea1); //however, the areas should be the same

            //Now, we change the area to test how the same function works
            newArea1.Name = "TestArea1";

            //we save the changes
            mm.AreaSave(newArea1);

            refreshManager(); //

            //and we get it back
            JassArea newArea2 = mm.AreaGetById(newArea1.JassAreaID);

            Assert.IsFalse(newArea1 == newArea2);  //this is to make sure are refreshing the context

            //and both are equal again.
            AssertEqualAreas(newArea1, newArea2);


            //finally we delete the created area
            mm.AreaDelete(newArea0Id);

            //and we verify that get all remains same.
            List<JassArea> areas2 = mm.AreasGetAll();
            Assert.IsTrue(areas2.Count == areas0.Count);

            DBCleanStateCheck();
        }

        [TestMethod]
        public void mmAreasHistory()
        {
            /* 
             The purpose of this test is to verify that after any CRUD operation is
             * performed on an Area the corresponding previois state is stored in the 
             * history. The idea of the test is simple, I create an area and test that the
             * history is created and make sense. The I update the area and check the history again.
             */
            
            DBClean();  //we clean the DB

            var startTime = DateTime.Now;

            //Create an area and we test that the history was saved properly

            JassArea newArea0 = new JassArea();
            newArea0.Name = "TestArea0";
            newArea0.Activities = new List<JassActivity>();
            int newArea0Id = mm.AreaCreate(newArea0);

            var allAreaHistories = mm.AreaHistoriesGetAll();

            Assert.IsTrue(allAreaHistories.Count == 1);

            var areaHistory = allAreaHistories[0];
            var area = mm.AreaGetById(newArea0Id);

            var mapper = new JassProperties<JassAreaCommon, JassArea, JassAreaHistory>();

            var result = mapper.Compare(area, areaHistory);

            Assert.IsTrue(result);

            Assert.IsTrue(areaHistory.Description == area.Description);
            Assert.IsTrue(areaHistory.Name == area.Name);
            Assert.IsTrue(areaHistory.TimeStamp < DateTime.Now);
            Assert.IsTrue(areaHistory.TimeStamp > startTime);

            Assert.IsTrue(areaHistory.JassAreaKey == area.JassAreaID);

            var startTime2 = DateTime.Now;

            //Now we will update that area and verify that the history is saved again.

            newArea0.Name += "X";
            newArea0.Description += "X";

            mm.AreaSave(newArea0);

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
    
        }

        [TestMethod]
        public void mmAreaHistoriesCRUD()
        {
            //Purpose: basic test of all CRUD operations on JassAreaHistory       

            DBClean();

            //we get all Area Histories
            List<JassAreaHistory> areas0 = mm.AreaHistoriesGetAll();

            //we can create an Area History
            JassAreaHistory newAreaHistory0 = new JassAreaHistory();
            newAreaHistory0.Name = "TestAreaHistory0";
            newAreaHistory0.ActivityHistories = new List<JassActivityHistory>();

            int newAreaHistory0Id = mm.AreaHistoryCreate(newAreaHistory0);

            refreshManager(); //

            //we verify that the new get all includes this area history as well
            List<JassAreaHistory> areas1 = mm.AreaHistoriesGetAll();
            Assert.IsTrue(areas1.Count == areas0.Count + 1);

            //we can get this arean back in particular
            JassAreaHistory newAreaHistory1 = mm.AreaHistoryGetById(newAreaHistory0Id);

            Assert.IsFalse(newAreaHistory0 == newAreaHistory1);  //this is to make sure are refreshing the context

            //and both are equal
            AssertEqualAreaHistories(newAreaHistory0, newAreaHistory1); //however, the areas should be the same

            //Now, we change area 2
            newAreaHistory1.Name = "TestAreaHistory1";

            //we save the changes
            mm.AreaHistorySave(newAreaHistory1);

            refreshManager(); //

            //and we get it back
            JassAreaHistory newAreaHistory2 = mm.AreaHistoryGetById(newAreaHistory1.JassAreaHistoryID);

            Assert.IsFalse(newAreaHistory1 == newAreaHistory2);  //this is to make sure are refreshing the context

            //and both are equal
            AssertEqualAreaHistories(newAreaHistory1, newAreaHistory2);


            //finally we delete the created area
            mm.AreaHistoryDelete(newAreaHistory0Id);

            //and we verify that get all remains same.
            List<JassAreaHistory> areas2 = mm.AreaHistoriesGetAll();
            Assert.IsTrue(areas2.Count == areas0.Count);

            DBCleanStateCheck();
        }
        [TestMethod]
        public void mmActivitiesCRUD()
        {
            //Purpose: basic test of all CRUD operations on JassActivity       
            
            DBClean();

            //see if we can get all activities
            List<JassActivity> Activities0 = mm.ActivitiesGetAll();

            //se if we can create an Activity with the nimimun information
            //We need to create the area first

            //we can create an area
            JassArea newArea0 = new JassArea();
            newArea0.Name = "TestArea0";
            newArea0.Activities = new List<JassActivity>();

            int newArea0Id = mm.AreaCreate(newArea0);

            // Ok, now we have the are and we can crate the activity
            JassActivity newActivity0 = new JassActivity();
            newActivity0.Name = "TestActivity0";
            newActivity0.JassAreaID = newArea0Id; 
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

            //And we also delete the created Area

            mm.AreaDelete(newArea0Id);

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

            //first we need an area becuase is mandatory

            JassArea newArea0 = new JassArea();
            newArea0.Name = "TestArea0";
            newArea0.Activities = new List<JassActivity>();
            int newAreaId = mm.AreaCreate(newArea0);


            var allAreaHistories = mm.AreaHistoriesGetAll();

            JassActivity newActivity = new JassActivity();
            newActivity.Name = "TestActivity0";
            newActivity.JassAreaID = newAreaId;
            int newActivityId = mm.ActivityCreate(newActivity);

            //So at least ewe need one record in this activityu history
            var allActivityHistories = mm.ActivityHistoriesGetAll();

            Assert.IsTrue(allActivityHistories.Count == 1);

       //now , let's really verify that the history record is what is supposed to be

            var activityHistory = allActivityHistories[0];//we only have one so..
            var activity = mm.ActivityGetById(newActivityId);

            var mapper = new JassProperties<JassActivityCommon, JassActivity, JassActivityHistory>();

            var result = mapper.Compare(activity, activityHistory);

            Assert.IsTrue(result); //this test should habe been enough
            //but just to be paranoid I will add some test manually as well not all fields.. but just a few

            Assert.IsTrue(activityHistory.Description == activity.Description);
            Assert.IsTrue(activityHistory.Name == activity.Name);
            Assert.IsTrue(activityHistory.TimeStamp < DateTime.Now);
            Assert.IsTrue(activityHistory.TimeStamp > startTime);

            Assert.IsTrue(activityHistory.JassActivityKey == activity.JassActivityID);

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

            //se if we can create an ActivityHistories with the nimimun information


            //We need to create the area history first

            //we can create an area
            JassAreaHistory newAreaHistory0 = new JassAreaHistory();
            newAreaHistory0.Name = "TestAreaHistory0";
            newAreaHistory0.ActivityHistories = new List<JassActivityHistory>();

            int newAreaHistory0Id = mm.AreaHistoryCreate(newAreaHistory0);

            //Now we can create the activity history
            JassActivityHistory newActivityHistory0 = new JassActivityHistory();
            newActivityHistory0.Name = "TestActivityHistory0";
            newActivityHistory0.JassAreaHistoryID = newAreaHistory0Id;


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

            mm.AreaHistoryDelete(newAreaHistory0Id);

            //and we verify that get all remains same.
            List<JassActivityHistory> ActivityHistories2 = mm.ActivityHistoriesGetAll();
            Assert.IsTrue(ActivityHistories2.Count == ActivityHistories0.Count);

            DBCleanStateCheck();
        }

        public void AssertEqualAreas(JassArea area0, JassArea area1)
        {
            Assert.IsTrue(area0.JassAreaID == area1.JassAreaID);
            Assert.IsTrue(area0.Name == area1.Name);
            Assert.IsTrue(area0.Activities.Count == area1.Activities.Count);


            List<JassActivity> activities0 = area0.Activities;
            List<JassActivity> activities1 = area1.Activities;

            for (int i = 0; i < activities0.Count; i++)
            {
                AssertEqualActivities(activities0[i], activities1[i]);
            }

        }

        public void AssertEqualAreaHistories(JassAreaHistory area0, JassAreaHistory area1)
        {
            Assert.IsTrue(area0.JassAreaHistoryID == area1.JassAreaHistoryID);
            Assert.IsTrue(area0.Name == area1.Name);
            Assert.IsTrue(area0.ActivityHistories.Count == area1.ActivityHistories.Count);


            List<JassActivityHistory> activities0 = area0.ActivityHistories;
            List<JassActivityHistory> activities1 = area1.ActivityHistories;

            for (int i = 0; i < activities0.Count; i++)
            {
                AssertEqualActivityHistories(activities0[i], activities1[i]);
            }

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
            List<JassArea> areas = mm.AreasGetAll();
            Assert.IsTrue(areas.Count == 0);
            List<JassActivity> activities = mm.ActivitiesGetAll();
            Assert.IsTrue(activities.Count == 0);

        }

        public void DBClean()
        {

            List<JassArea> areas = mm.AreasGetAll();
            foreach (JassArea area in areas) { mm.AreaDelete(area.JassAreaID); };
            
            List<JassActivity> activities = mm.ActivitiesGetAll();
            foreach(JassActivity activity in activities){ mm.ActivityDelete(activity.JassActivityID);};

            List<JassAreaHistory> areaHistories = mm.AreaHistoriesGetAll();
            foreach (JassAreaHistory area in areaHistories) { mm.AreaHistoryDelete(area.JassAreaHistoryID); };

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
