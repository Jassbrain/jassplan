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
    public class JpBigTest
    {
        public JpBigTest()
        {
        }

        enum ControlType {
            Button,
            Edit
        }

        UITestControl getControl(ControlType controlType, BrowserWindow browser, string buttonId){
           UITestControl button = new UITestControl(browser);
           button.TechnologyName = "Web";
           var controlTypeString = controlType.ToString();
           button.SearchProperties.Add("ControlType", controlTypeString);
           button.SearchProperties.Add("Id", buttonId);
           return button;
        }

        BrowserWindow openBrowserOn(string url){
            BrowserWindow browser = BrowserWindow.Launch(url);
            return browser;
        }

        void loginIntoJassplan(BrowserWindow browser){
            var loginSubmitButton = getControl(ControlType.Button,browser, "login_submit_button");
            var loginName = getControl(ControlType.Edit, browser, "loginName");
            var password = getControl(ControlType.Edit, browser, "password");
            loginName.SetProperty("value", "test");
            password.SetProperty("value", "password");
            Mouse.Click(loginSubmitButton);
        }

        [TestMethod]
        public void First_Big_Jassplan_UI_Test()
        {
            var browserWindow = openBrowserOn("http://jassplan.azurewebsites.net/account/logoff");    
            loginIntoJassplan(browserWindow);
        }


        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

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
