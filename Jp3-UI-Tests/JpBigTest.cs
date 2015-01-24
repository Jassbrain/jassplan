using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace Jp3_UI_Tests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class JpBigFatIntegrationTest
    {
        public JpBigFatIntegrationTest()
        {
        }

        enum ControlType {
            Button, // input type submit
            Edit,   // input type text
            Pane,   //Div
            Hyperlink,   //<a>
            Link   //<a>
        }

        UITestControl getControl(ControlType controlType, BrowserWindow browser, string buttonId){
           UITestControl button = new UITestControl(browser);
           button.TechnologyName = "Web";
           var controlTypeString = controlType.ToString();
           button.SearchProperties.Add("ControlType", controlTypeString);
           button.SearchProperties.Add("Id", buttonId);
           var buttons = button.FindMatchingControls();
           button.Find();
           return button;
        }

        BrowserWindow openBrowserOn(string url){
            BrowserWindow browser = BrowserWindow.Launch(url);
            return browser;
        }

        void User_Logins_to_Jassplan(BrowserWindow browser){
            var loginSubmitButton = getControl(ControlType.Button,browser, "login_submit_button");
            var loginName = getControl(ControlType.Edit, browser, "loginName");
            var password = getControl(ControlType.Edit, browser, "password");
            loginName.SetProperty("value", "test");
            password.SetProperty("value", "password");
            Mouse.Click(loginSubmitButton);
        }

        void Assert_Default_View_Correcteness(BrowserWindow browser){
            Assert_View_Name_Correcteness("Do", browser);
        }

        void Assert_View_Name_Correcteness(string viewName, BrowserWindow browser)
        {
            var viewModelState = getControl(ControlType.Pane, browser, "view-model-state");
            var state = viewModelState.GetProperty("innerHTML");
            Assert.AreEqual(state, viewName);
        }

        void Planner_Access_Plan_View(BrowserWindow browser)
        {
            var PlanButton = getControl(ControlType.Pane, browser, "plan-button");
            Mouse.Click(PlanButton);
        }

        [TestMethod]
        public void Big_Fat_Integration_Test()
        {
            var browserWindow = openBrowserOn("http://jassplan.azurewebsites.net/account/logoff");    
            User_Logins_to_Jassplan(browserWindow);
            Assert_Default_View_Correcteness(browserWindow);
            Planner_Access_Plan_View(browserWindow);
            Assert_View_Name_Correcteness("Plan", browserWindow);
            Playback.Wait(60000);
        }


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;
    }
}
