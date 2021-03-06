﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 12.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace JassWebCodedUI
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public partial class UIMap
    {
        
        /// <summary>
        /// RecordedMethod1 - Use 'RecordedMethod1Params' to pass parameters into this method.
        /// </summary>
        public void RecordedMethod1()
        {
            #region Variable Declarations
            WinButton uIJassTestsDataContextButton = this.UIToDoInternetExplorerWindow.UIFavoritesBarToolBar.UIJassTestsDataContextButton;
            WinButton uIJassplanButton = this.UIToDoInternetExplorerWindow.UIFavoritesBarToolBar.UIJassplanButton;
            HtmlHyperlink uINewHyperlink = this.UIToDoInternetExplorerWindow.UIToDoDocument.UINoteslistpagePane.UINewHyperlink;
            HtmlHyperlink uISaveHyperlink = this.UIToDoInternetExplorerWindow.UIToDoDocument.UISaveHyperlink;
            HtmlDiv uITitleToDoToDoTodaySPane = this.UIToDoInternetExplorerWindow.UIToDoDocument.UINoteeditorpagePane.UITitleToDoToDoTodaySPane;
            HtmlCustom uINoteeditorformCustom = this.UIToDoInternetExplorerWindow.UIToDoDocument.UINoteeditorformCustom;
            HtmlEdit uITitleEdit = this.UIToDoInternetExplorerWindow.UIToDoDocument.UITitleEdit;
            HtmlEdit uIPointsEdit = this.UIToDoInternetExplorerWindow.UIToDoDocument.UIPointsEdit;
            HtmlEdit uITodayPointsEdit = this.UIToDoInternetExplorerWindow.UIToDoDocument.UITodayPointsEdit;
            HtmlDiv uINoteslistpagePane = this.UIToDoInternetExplorerWindow.UIToDoDocument.UINoteslistpagePane;
            #endregion

            // Click 'JassTests DataContext' button
            Mouse.Click(uIJassTestsDataContextButton, new Point(98, 16));

            // Click 'Jassplan' button
            Mouse.Click(uIJassplanButton, new Point(36, 24));

            // Click 'New' link
            Mouse.Click(uINewHyperlink, new Point(34, 7));

            // Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
            Playback.PlaybackSettings.ContinueOnError = true;

            // Mouse hover 'Save' link at (1, 1)
            Mouse.Hover(uISaveHyperlink, new Point(1, 1));

            // Mouse hover 'Title: To-Do: To-Do-Today: S' pane at (1, 1)
            Mouse.Hover(uITitleToDoToDoTodaySPane, new Point(1, 1));

            // Mouse hover 'note-editor-form' custom control at (1, 1)
            Mouse.Hover(uINoteeditorformCustom, new Point(1, 1));

            // Reset flag to ensure that play back stops if there is an error.
            Playback.PlaybackSettings.ContinueOnError = false;

            // Type 'xxxx' in 'Title:' text box
            uITitleEdit.Text = this.RecordedMethod1Params.UITitleEditText;

            // Type '1' in 'Points:' text box
            uIPointsEdit.Text = this.RecordedMethod1Params.UIPointsEditText;

            // Type '1' in 'TodayPoints:' text box
            uITodayPointsEdit.Text = this.RecordedMethod1Params.UITodayPointsEditText;

            // Click 'Save' link
            Mouse.Click(uISaveHyperlink, new Point(34, 16));

            // Set flag to allow play back to continue if non-essential actions fail. (For example, if a mouse hover action fails.)
            Playback.PlaybackSettings.ContinueOnError = true;

            // Mouse hover 'New' link at (1, 1)
            Mouse.Hover(uINewHyperlink, new Point(1, 1));

            // Reset flag to ensure that play back stops if there is an error.
            Playback.PlaybackSettings.ContinueOnError = false;

            // Type '{Enter}' in 'notes-list-page' pane
            Keyboard.SendKeys(uINoteslistpagePane, this.RecordedMethod1Params.UINoteslistpagePaneSendKeys, ModifierKeys.None);
        }
        
        #region Properties
        public virtual RecordedMethod1Params RecordedMethod1Params
        {
            get
            {
                if ((this.mRecordedMethod1Params == null))
                {
                    this.mRecordedMethod1Params = new RecordedMethod1Params();
                }
                return this.mRecordedMethod1Params;
            }
        }
        
        public UIToDoInternetExplorerWindow UIToDoInternetExplorerWindow
        {
            get
            {
                if ((this.mUIToDoInternetExplorerWindow == null))
                {
                    this.mUIToDoInternetExplorerWindow = new UIToDoInternetExplorerWindow();
                }
                return this.mUIToDoInternetExplorerWindow;
            }
        }
        #endregion
        
        #region Fields
        private RecordedMethod1Params mRecordedMethod1Params;
        
        private UIToDoInternetExplorerWindow mUIToDoInternetExplorerWindow;
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'RecordedMethod1'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class RecordedMethod1Params
    {
        
        #region Fields
        /// <summary>
        /// Type 'xxxx' in 'Title:' text box
        /// </summary>
        public string UITitleEditText = "xxxx";
        
        /// <summary>
        /// Type '1' in 'Points:' text box
        /// </summary>
        public string UIPointsEditText = "1";
        
        /// <summary>
        /// Type '1' in 'TodayPoints:' text box
        /// </summary>
        public string UITodayPointsEditText = "1";
        
        /// <summary>
        /// Type '{Enter}' in 'notes-list-page' pane
        /// </summary>
        public string UINoteslistpagePaneSendKeys = "{Enter}";
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class UIToDoInternetExplorerWindow : BrowserWindow
    {
        
        public UIToDoInternetExplorerWindow()
        {
            #region Search Criteria
            this.SearchProperties[UITestControl.PropertyNames.Name] = "To Do";
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "IEFrame";
            this.WindowTitles.Add("To Do");
            this.WindowTitles.Add("Jasmine Test Runner of Jassplan");
            this.WindowTitles.Add("Edit Note");
            #endregion
        }
        
        public void LaunchUrl(System.Uri url)
        {
            this.CopyFrom(BrowserWindow.Launch(url));
        }
        
        #region Properties
        public UIFavoritesBarToolBar UIFavoritesBarToolBar
        {
            get
            {
                if ((this.mUIFavoritesBarToolBar == null))
                {
                    this.mUIFavoritesBarToolBar = new UIFavoritesBarToolBar(this);
                }
                return this.mUIFavoritesBarToolBar;
            }
        }
        
        public UIToDoDocument UIToDoDocument
        {
            get
            {
                if ((this.mUIToDoDocument == null))
                {
                    this.mUIToDoDocument = new UIToDoDocument(this);
                }
                return this.mUIToDoDocument;
            }
        }
        #endregion
        
        #region Fields
        private UIFavoritesBarToolBar mUIFavoritesBarToolBar;
        
        private UIToDoDocument mUIToDoDocument;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class UIFavoritesBarToolBar : WinToolBar
    {
        
        public UIFavoritesBarToolBar(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinToolBar.PropertyNames.Name] = "Favorites Bar";
            this.WindowTitles.Add("To Do");
            this.WindowTitles.Add("Jasmine Test Runner of Jassplan");
            #endregion
        }
        
        #region Properties
        public WinButton UIJassTestsDataContextButton
        {
            get
            {
                if ((this.mUIJassTestsDataContextButton == null))
                {
                    this.mUIJassTestsDataContextButton = new WinButton(this);
                    #region Search Criteria
                    this.mUIJassTestsDataContextButton.SearchProperties[WinButton.PropertyNames.Name] = "JassTests DataContext";
                    this.mUIJassTestsDataContextButton.WindowTitles.Add("To Do");
                    #endregion
                }
                return this.mUIJassTestsDataContextButton;
            }
        }
        
        public WinButton UIJassplanButton
        {
            get
            {
                if ((this.mUIJassplanButton == null))
                {
                    this.mUIJassplanButton = new WinButton(this);
                    #region Search Criteria
                    this.mUIJassplanButton.SearchProperties[WinButton.PropertyNames.Name] = "Jassplan";
                    this.mUIJassplanButton.WindowTitles.Add("Jasmine Test Runner of Jassplan");
                    #endregion
                }
                return this.mUIJassplanButton;
            }
        }
        #endregion
        
        #region Fields
        private WinButton mUIJassTestsDataContextButton;
        
        private WinButton mUIJassplanButton;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class UIToDoDocument : HtmlDocument
    {
        
        public UIToDoDocument(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[HtmlDocument.PropertyNames.Id] = null;
            this.SearchProperties[HtmlDocument.PropertyNames.RedirectingPage] = "False";
            this.SearchProperties[HtmlDocument.PropertyNames.FrameDocument] = "False";
            this.FilterProperties[HtmlDocument.PropertyNames.Title] = "To Do";
            this.FilterProperties[HtmlDocument.PropertyNames.AbsolutePath] = "/JassClientJS/index.html";
            this.FilterProperties[HtmlDocument.PropertyNames.PageUrl] = "http://localhost:51727/JassClientJS/index.html";
            this.WindowTitles.Add("To Do");
            this.WindowTitles.Add("Edit Note");
            #endregion
        }
        
        #region Properties
        public UINoteslistpagePane UINoteslistpagePane
        {
            get
            {
                if ((this.mUINoteslistpagePane == null))
                {
                    this.mUINoteslistpagePane = new UINoteslistpagePane(this);
                }
                return this.mUINoteslistpagePane;
            }
        }
        
        public HtmlHyperlink UISaveHyperlink
        {
            get
            {
                if ((this.mUISaveHyperlink == null))
                {
                    this.mUISaveHyperlink = new HtmlHyperlink(this);
                    #region Search Criteria
                    this.mUISaveHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Id] = "save-note-button";
                    this.mUISaveHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Name] = null;
                    this.mUISaveHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Target] = null;
                    this.mUISaveHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.InnerText] = "Save ";
                    this.mUISaveHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.AbsolutePath] = "/JassClientJS/index.html";
                    this.mUISaveHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Title] = null;
                    this.mUISaveHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Href] = "http://localhost:51727/JassClientJS/index.html#notes-list-page";
                    this.mUISaveHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Class] = "ui-btn-right ui-btn ui-btn-up-b ui-shadow ui-btn-corner-all ui-btn-icon-left";
                    this.mUISaveHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.ControlDefinition] = "class=\"ui-btn-right ui-btn ui-btn-up-b u";
                    this.mUISaveHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.TagInstance] = "9";
                    this.mUISaveHyperlink.WindowTitles.Add("Edit Note");
                    #endregion
                }
                return this.mUISaveHyperlink;
            }
        }
        
        public UINoteeditorpagePane UINoteeditorpagePane
        {
            get
            {
                if ((this.mUINoteeditorpagePane == null))
                {
                    this.mUINoteeditorpagePane = new UINoteeditorpagePane(this);
                }
                return this.mUINoteeditorpagePane;
            }
        }
        
        public HtmlCustom UINoteeditorformCustom
        {
            get
            {
                if ((this.mUINoteeditorformCustom == null))
                {
                    this.mUINoteeditorformCustom = new HtmlCustom(this);
                    #region Search Criteria
                    this.mUINoteeditorformCustom.SearchProperties["TagName"] = "FORM";
                    this.mUINoteeditorformCustom.SearchProperties["Id"] = "note-editor-form";
                    this.mUINoteeditorformCustom.SearchProperties[UITestControl.PropertyNames.Name] = null;
                    this.mUINoteeditorformCustom.FilterProperties["Class"] = null;
                    this.mUINoteeditorformCustom.FilterProperties["ControlDefinition"] = "id=\"note-editor-form\" action=\"\" method=\"";
                    this.mUINoteeditorformCustom.FilterProperties["TagInstance"] = "1";
                    this.mUINoteeditorformCustom.WindowTitles.Add("Edit Note");
                    #endregion
                }
                return this.mUINoteeditorformCustom;
            }
        }
        
        public HtmlEdit UITitleEdit
        {
            get
            {
                if ((this.mUITitleEdit == null))
                {
                    this.mUITitleEdit = new HtmlEdit(this);
                    #region Search Criteria
                    this.mUITitleEdit.SearchProperties[HtmlEdit.PropertyNames.Id] = "note-title-editor";
                    this.mUITitleEdit.SearchProperties[HtmlEdit.PropertyNames.Name] = "note-title-editor";
                    this.mUITitleEdit.FilterProperties[HtmlEdit.PropertyNames.LabeledBy] = "Title:";
                    this.mUITitleEdit.FilterProperties[HtmlEdit.PropertyNames.Type] = "SINGLELINE";
                    this.mUITitleEdit.FilterProperties[HtmlEdit.PropertyNames.Title] = null;
                    this.mUITitleEdit.FilterProperties[HtmlEdit.PropertyNames.Class] = "ui-input-text ui-body-c";
                    this.mUITitleEdit.FilterProperties[HtmlEdit.PropertyNames.ControlDefinition] = "name=\"note-title-editor\" class=\"ui-input";
                    this.mUITitleEdit.FilterProperties[HtmlEdit.PropertyNames.TagInstance] = "1";
                    this.mUITitleEdit.WindowTitles.Add("Edit Note");
                    #endregion
                }
                return this.mUITitleEdit;
            }
        }
        
        public HtmlEdit UIPointsEdit
        {
            get
            {
                if ((this.mUIPointsEdit == null))
                {
                    this.mUIPointsEdit = new HtmlEdit(this);
                    #region Search Criteria
                    this.mUIPointsEdit.SearchProperties[HtmlEdit.PropertyNames.Id] = "note-estimatedDuration-editor";
                    this.mUIPointsEdit.SearchProperties[HtmlEdit.PropertyNames.Name] = "note-estimatedDuration-editor";
                    this.mUIPointsEdit.FilterProperties[HtmlEdit.PropertyNames.LabeledBy] = "Points: ";
                    this.mUIPointsEdit.FilterProperties[HtmlEdit.PropertyNames.Type] = "SINGLELINE";
                    this.mUIPointsEdit.FilterProperties[HtmlEdit.PropertyNames.Title] = null;
                    this.mUIPointsEdit.FilterProperties[HtmlEdit.PropertyNames.Class] = "ui-input-text ui-body-c";
                    this.mUIPointsEdit.FilterProperties[HtmlEdit.PropertyNames.ControlDefinition] = "name=\"note-estimatedDuration-editor\" cla";
                    this.mUIPointsEdit.FilterProperties[HtmlEdit.PropertyNames.TagInstance] = "3";
                    this.mUIPointsEdit.WindowTitles.Add("Edit Note");
                    #endregion
                }
                return this.mUIPointsEdit;
            }
        }
        
        public HtmlEdit UITodayPointsEdit
        {
            get
            {
                if ((this.mUITodayPointsEdit == null))
                {
                    this.mUITodayPointsEdit = new HtmlEdit(this);
                    #region Search Criteria
                    this.mUITodayPointsEdit.SearchProperties[HtmlEdit.PropertyNames.Id] = "note-actualDuration-editor";
                    this.mUITodayPointsEdit.SearchProperties[HtmlEdit.PropertyNames.Name] = "note-actualDuration-editor";
                    this.mUITodayPointsEdit.FilterProperties[HtmlEdit.PropertyNames.LabeledBy] = "TodayPoints: ";
                    this.mUITodayPointsEdit.FilterProperties[HtmlEdit.PropertyNames.Type] = "SINGLELINE";
                    this.mUITodayPointsEdit.FilterProperties[HtmlEdit.PropertyNames.Title] = null;
                    this.mUITodayPointsEdit.FilterProperties[HtmlEdit.PropertyNames.Class] = "ui-input-text ui-body-c";
                    this.mUITodayPointsEdit.FilterProperties[HtmlEdit.PropertyNames.ControlDefinition] = "name=\"note-actualDuration-editor\" class=";
                    this.mUITodayPointsEdit.FilterProperties[HtmlEdit.PropertyNames.TagInstance] = "4";
                    this.mUITodayPointsEdit.WindowTitles.Add("Edit Note");
                    #endregion
                }
                return this.mUITodayPointsEdit;
            }
        }
        #endregion
        
        #region Fields
        private UINoteslistpagePane mUINoteslistpagePane;
        
        private HtmlHyperlink mUISaveHyperlink;
        
        private UINoteeditorpagePane mUINoteeditorpagePane;
        
        private HtmlCustom mUINoteeditorformCustom;
        
        private HtmlEdit mUITitleEdit;
        
        private HtmlEdit mUIPointsEdit;
        
        private HtmlEdit mUITodayPointsEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class UINoteslistpagePane : HtmlDiv
    {
        
        public UINoteslistpagePane(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[HtmlDiv.PropertyNames.Id] = "notes-list-page";
            this.SearchProperties[HtmlDiv.PropertyNames.Name] = null;
            this.FilterProperties[HtmlDiv.PropertyNames.InnerText] = "Plan\r\n\r\n\r\n \r\n0/0/0\r\n\r\n\r\nNew \r\n \r\nFri Nov";
            this.FilterProperties[HtmlDiv.PropertyNames.Title] = null;
            this.FilterProperties[HtmlDiv.PropertyNames.Class] = "ui-page ui-body-c ui-page-header-fixed ui-page-footer-fixed ui-page-active";
            this.FilterProperties[HtmlDiv.PropertyNames.ControlDefinition] = "tabindex=\"0\" class=\"ui-page ui-body-c ui-page-header-fixed ui-page-footer-fixed u" +
                "i-page-active\" id=\"notes-list-page\" style=\"padding-top: 67px; padding-bottom: 43" +
                "px; min-height: 502px;\" data-role=\"page\" data-title=\"To Do\" data-url=\"notes-list" +
                "-page\"";
            this.FilterProperties[HtmlDiv.PropertyNames.TagInstance] = "1";
            this.WindowTitles.Add("To Do");
            #endregion
        }
        
        #region Properties
        public HtmlHyperlink UINewHyperlink
        {
            get
            {
                if ((this.mUINewHyperlink == null))
                {
                    this.mUINewHyperlink = new HtmlHyperlink(this);
                    #region Search Criteria
                    this.mUINewHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Id] = null;
                    this.mUINewHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Name] = null;
                    this.mUINewHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Target] = null;
                    this.mUINewHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.InnerText] = "New ";
                    this.mUINewHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.AbsolutePath] = "/JassClientJS/index.html";
                    this.mUINewHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Title] = null;
                    this.mUINewHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Href] = "http://localhost:51727/JassClientJS/index.html#note-editor-page";
                    this.mUINewHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Class] = "ui-btn-right ui-btn ui-btn-up-b ui-shadow ui-btn-corner-all ui-btn-icon-left";
                    this.mUINewHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.ControlDefinition] = "class=\"ui-btn-right ui-btn ui-btn-up-b u";
                    this.mUINewHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.TagInstance] = "1";
                    this.mUINewHyperlink.WindowTitles.Add("To Do");
                    #endregion
                }
                return this.mUINewHyperlink;
            }
        }
        #endregion
        
        #region Fields
        private HtmlHyperlink mUINewHyperlink;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class UINoteeditorpagePane : HtmlDiv
    {
        
        public UINoteeditorpagePane(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[HtmlDiv.PropertyNames.Id] = "note-editor-page";
            this.SearchProperties[HtmlDiv.PropertyNames.Name] = null;
            this.FilterProperties[HtmlDiv.PropertyNames.InnerText] = "Cancel \r\n \r\nEdit\r\n\r\nSave \r\n \r\n\r\n\r\nTitle:";
            this.FilterProperties[HtmlDiv.PropertyNames.Title] = null;
            this.FilterProperties[HtmlDiv.PropertyNames.Class] = "ui-page ui-body-c ui-page-header-fixed ui-page-footer-fixed ui-page-active fade i" +
                "n";
            this.FilterProperties[HtmlDiv.PropertyNames.ControlDefinition] = @"tabindex=""0"" class=""ui-page ui-body-c ui-page-header-fixed ui-page-footer-fixed ui-page-active fade in"" id=""note-editor-page"" style=""height: 697.76px; padding-top: 43px; padding-bottom: 43px;"" data-role=""page"" data-title=""Edit Note"" data-url=""note-editor-page""";
            this.FilterProperties[HtmlDiv.PropertyNames.TagInstance] = "14";
            this.WindowTitles.Add("Edit Note");
            #endregion
        }
        
        #region Properties
        public HtmlDiv UITitleToDoToDoTodaySPane
        {
            get
            {
                if ((this.mUITitleToDoToDoTodaySPane == null))
                {
                    this.mUITitleToDoToDoTodaySPane = new HtmlDiv(this);
                    #region Search Criteria
                    this.mUITitleToDoToDoTodaySPane.SearchProperties[HtmlDiv.PropertyNames.Id] = null;
                    this.mUITitleToDoToDoTodaySPane.SearchProperties[HtmlDiv.PropertyNames.Name] = null;
                    this.mUITitleToDoToDoTodaySPane.FilterProperties[HtmlDiv.PropertyNames.InnerText] = "Title:\r\n\r\n\r\nTo-Do:\r\n\r\nTo-Do-Today: \r\n\r\nS";
                    this.mUITitleToDoToDoTodaySPane.FilterProperties[HtmlDiv.PropertyNames.Title] = null;
                    this.mUITitleToDoToDoTodaySPane.FilterProperties[HtmlDiv.PropertyNames.Class] = "ui-content";
                    this.mUITitleToDoToDoTodaySPane.FilterProperties[HtmlDiv.PropertyNames.ControlDefinition] = "class=\"ui-content\" role=\"main\" data-role=\"content\"";
                    this.mUITitleToDoToDoTodaySPane.FilterProperties[HtmlDiv.PropertyNames.TagInstance] = "16";
                    this.mUITitleToDoToDoTodaySPane.WindowTitles.Add("Edit Note");
                    #endregion
                }
                return this.mUITitleToDoToDoTodaySPane;
            }
        }
        #endregion
        
        #region Fields
        private HtmlDiv mUITitleToDoToDoTodaySPane;
        #endregion
    }
}
