﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jassplan.ModelManager;
using Jassplan.Model;
using System.Collections.Generic;
using System.Reflection;

namespace Jassplan.Tests.ModelManager
{
    [TestClass]
    public class ModelManagerTest
    {

        JassModelManager mm = new JassModelManager();
        [TestMethod]
        public void ModelManagerAreasCRUD()
        {
            //Purpose: basic test of all CRUD operations on JassArea       


            DBClean();

            List<JassArea> areas0 = mm.AreasGetAll();

            //we can create an area
            JassArea newArea0 = new JassArea();
            newArea0.Name = "TestArea0";
            newArea0.Activities = new List<JassActivity>();

            int newArea0Id = mm.AreaCreate(newArea0);

            refreshManager(); //

            //we verify that the new get all includes this area
            List<JassArea> areas1 = mm.AreasGetAll();
            Assert.IsTrue(areas1.Count == areas0.Count + 1);

            //we can get it back
            JassArea newArea1 = mm.AreaGetById(newArea0Id);

            Assert.IsFalse(newArea0 == newArea1);  //this is to make sure are refreshing the context

            //and both are equal
            AssertEqualAreas(newArea0, newArea1); //however, the areas should be the same

            //Now, we change area 2
            newArea1.Name = "TestArea1";

            //we save the changes
            mm.AreaSave(newArea1);

            refreshManager(); //

            //and we get it back
            JassArea newArea2 = mm.AreaGetById(newArea1.JassAreaID);

            Assert.IsFalse(newArea1 == newArea2);  //this is to make sure are refreshing the context

            //and both are equal
            AssertEqualAreas(newArea1, newArea2);


            //finally we delete the created area
            mm.AreaDelete(newArea0Id);

            //and we verify that get all remains same.
            List<JassArea> areas2 = mm.AreasGetAll();
            Assert.IsTrue(areas2.Count == areas0.Count);

            DBCleanStateCheck();
        }
        [TestMethod]
        public void ModelManagerAreaHistoriesCRUD()
        {
            //Purpose: basic test of all CRUD operations on JassAreaHistory       


            DBClean();

            List<JassAreaHistory> areas0 = mm.AreaHistoriesGetAll();

            //we can create an area
            JassAreaHistory newAreaHistory0 = new JassAreaHistory();
            newAreaHistory0.Name = "TestAreaHistory0";
            newAreaHistory0.Activities = new List<JassActivityHistory>();

            int newAreaHistory0Id = mm.AreaHistoryCreate(newAreaHistory0);

            refreshManager(); //

            //we verify that the new get all includes this area
            List<JassAreaHistory> areas1 = mm.AreaHistoriesGetAll();
            Assert.IsTrue(areas1.Count == areas0.Count + 1);

            //we can get it back
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
        public void ModelManagerActivityLogCRUD()
        {
            //Purpose: basic test of all CRUD operations on JassActivityLog   
            //To test that we can create this we need at least 1 area and 1 activity


            DBClean();

            List<JassActivityLog> activityLogs0 = mm.ActivityLogsGetAll();

            //So, we create an area
            JassArea newArea0 = new JassArea();
            newArea0.Name = "TestArea0";
            newArea0.Activities = new List<JassActivity>();
            int newArea0Id = mm.AreaCreate(newArea0);

            //Then, we create an activity
            JassActivity newActivity0 = new JassActivity();
            newActivity0.Name = "TestActivity0";
            newActivity0.JassAreaID = newArea0Id;
            int newActivity0Id = mm.ActivityCreate(newActivity0);

            refreshManager(); //

            //we get the activity again just in case

            newActivity0 = mm.ActivityGetById(newActivity0Id);

            //Finally, we will create an ActivityLog based on that activity and area
            //WARNING: This test actually will not prove too much, we will just check that the
            //CRUD is moslty working. Hwoever, we will have another specifric method

            //Then, we create an activityLog based on that activity

            int newActivityLog0Id = mm.ActivityLogCreate(newActivity0);

            //we verify that the new get all includes this activitylog
            List<JassActivityLog> activityLogs1 = mm.ActivityLogsGetAll();
            Assert.IsTrue(activityLogs1.Count == activityLogs0.Count + 1);

            refreshManager();

            //and we get it back
            JassActivityLog newActivityLog1 = mm.ActivityLogGetById(newActivityLog0Id);

            //now we will delete the original area and activity

            mm.AreaDelete(newArea0Id);
 
//crap delete deletes everything...!!!
      

            DBCleanStateCheck();
        }

        [TestMethod]
        public void ModelManagerActivitiesCRUD()
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
            Assert.IsTrue(area0.Activities.Count == area1.Activities.Count);


            List<JassActivityHistory> activities0 = area0.Activities;
            List<JassActivityHistory> activities1 = area1.Activities;

            for (int i = 0; i < activities0.Count; i++)
            {
                AssertEqualActivities(activities0[i], activities1[i]);
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

        public void AssertEqualActivities(JassActivityHistory activity0, JassActivityHistory activity1)
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
            List<JassActivityLog> activityLogs = mm.ActivityLogsGetAll();
            Assert.IsTrue(activityLogs.Count == 0);

        }

        public void refreshManager()
        {
            mm.Dispose();
            mm = new JassModelManager();
        }

        public void DBClean()
        {

            List<JassActivity> activities = mm.ActivitiesGetAll();
            foreach(JassActivity activity in activities){ mm.ActivityDelete(activity.JassActivityID);};

            List<JassActivityLog> activityLogs = mm.ActivityLogsGetAll();
            foreach(JassActivityLog activityLog in activityLogs){ mm.ActivityLogDelete(activityLog.JassActivityLogID); };

            List<JassArea> areas = mm.AreasGetAll();
            foreach(JassArea area in areas){ mm.AreaDelete(area.JassAreaID); };

            DBCleanStateCheck();

        }
    }
}
