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

        [TestMethod]
        public void First_Big_Jassplan_UI_Test()
        {

            BrowserWindow.ClearCookies();
            BrowserWindow browser = BrowserWindow.Launch(new Uri("http://jassplan.azurewebsites.net/"));
            
            browser.Maximized = true;


           //The idea is to test with the user "test/password" which is based on cookie. 
           //if we are already logged we proceed with testing. if not we will log in.

           UITestControl loginSubmitButton = new UITestControl(browser);
           loginSubmitButton.TechnologyName = "Web";
           loginSubmitButton.SearchProperties.Add("ControlType", "Button");
           loginSubmitButton.SearchProperties.Add("Id", "login_submit_button");

           var loginSubmitButtonExists = loginSubmitButton.Exists;

           
            if (loginSubmitButtonExists) {


               UITestControl loginName = new UITestControl(browser);
               loginName.TechnologyName = "Web";
               loginName.SearchProperties.Add("ControlType", "Edit");
               loginName.SearchProperties.Add("Id", "loginName");

               var loginNameExists = loginName.Exists;
               if (loginNameExists) { 

               loginName.SetProperty("value", "test");

               }

               UITestControl password = new UITestControl(browser);
               password.TechnologyName = "Web";
               password.SearchProperties.Add("ControlType", "Edit");
               password.SearchProperties.Add("Id", "password");

               var passwordExists = password.Exists;
               if (passwordExists)
               {

                   password.SetProperty("value", "password");

               }
               Playback.Wait(1000);

           Mouse.Click(loginSubmitButton);


           Playback.Wait(100000);

           }
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
