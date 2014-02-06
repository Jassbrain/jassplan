/**
 * Created by pablo on 12/15/13.
 */

var Jassplan = Jassplan || {};

Jassplan.controller = (function (dataContext) {

    var userLogged = false;
    var appStorageKey = "Notes.NotesList";
    var notesListPageId = "notes-list-page";
    var noteEditorPageId = "note-editor-page";
    var notesListSelector = "#notes-list-content";
    var noteTitleEditorSel = "[name=note-title-editor]";
    var noteNarrativeEditorSel = "[name=note-narrative-editor]";
    var currentNote;
    var saveNoteButtonSel = "#save-note-button";

    var renderNotesList = function () {

        var notesList = dataContext.getNotesList();

        var notesCount = notesList.length,
            note,
            dateGroup,
            noteDate,
            i;
        var view = $(notesListSelector);
        view.empty();
        var ul = $("<ul id=\"notes-list\" data-role=\"listview\"></ul>").appendTo(view);
        for (i = 0; i < notesCount; i += 1) {

            noteDate = (new Date(notesList[i].dateCreated)).toDateString();

            if (dateGroup !== noteDate) {
                $("<li data-role=\"list-divider\">" + noteDate + "</li>").appendTo(ul);
                dateGroup = noteDate;
            }

            $("<li>"
            + "<a href=\"index.html#note-editor-page?noteId=" + notesList[i].id + "\">"
            + "<div class=\"list-item-title\">" + notesList[i].title + "</div>"
            + "<div class=\"list-item-narrative\">" + notesList[i].narrative + "</div>"
            + "</a>"
            + "</li>").appendTo(ul);
        }
        ul.listview();
    };
    var onPageChange = function (event, data) {
        var fromPageId;
        if (data.options.fromPage) {
            fromPageId = data.options.fromPage.attr("id");
        }
        var toPageId = data.toPage.attr("id");
        switch (toPageId) {
            case notesListPageId:
                renderNotesList();
                break;
            case noteEditorPageId:
                if (fromPageId === notesListPageId) {
                    renderSelectedNote(data);
                }
                break;
        }
    };
    var renderSelectedNote = function (data){
        var u = $.mobile.path.parseUrl(data.options.fromPage.context.URL);
        var re = "^#" + noteEditorPageId;
        if (u.hash.search(re) !== -1)
        {

        var queryStringObj = queryStringToObject(data.options.queryString);
        var titleEditor = $(noteTitleEditorSel);
        var narrativeEditor = $(noteNarrativeEditorSel);
        var noteId = queryStringObj["noteId"];

        if (typeof noteId !== "undefined")
        {  //here we are supposed to load the values into the fields
            var notesList = dataContext.getNotesList();

            var notesCount = notesList.length;
            var note;
            for (var i = 0; i < notesCount; i++) {
                note = notesList[i];
                if (noteId === note.id) {
                    titleEditor.val(note.title);
                    narrativeEditor.val(note.narrative);
                    currentNote=note;
                    break;
                }
            }
            titleEditor.focus();}
        }
        return data;
    };
    var onPageBeforeChange = function (event, data) {
        var titleEditor = $(noteTitleEditorSel);
        var narrativeEditor = $(noteNarrativeEditorSel);
        titleEditor.val("");
        narrativeEditor.val("");
        currentNote=null;

        if (typeof data.toPage === "string") {
            var url = $.mobile.path.parseUrl(data.toPage);

            if ($.mobile.path.isEmbeddedPage(url)) {
                var parsedHash = url.hash.replace(/^#/, "");
                var parsedQueryString = $.mobile.path.parseUrl(parsedHash);
                data.options.queryString = parsedQueryString.search.replace("?", "");
                var x = 1;
            }
        }
    };
    var onSaveNoteButtonTapped = function () {
        var titleEditor = $(noteTitleEditorSel);
        var narrativeEditor = $(noteNarrativeEditorSel);
        var tempNote = dataContext.createBlankNote();

        tempNote.title = titleEditor.val();
        tempNote.narrative = narrativeEditor.val();

        if (tempNote.isValid()) {
            if (null !== currentNote) {
                currentNote.title = tempNote.title;
                currentNote.narrative = tempNote.narrative; }
            else {
                currentNote = tempNote; }
            dataContext.saveNote(currentNote);
            returnToNotesListPage(); }
        else {
            alert('temp Note is Invalid');
        };
    }

    var checkUserLogged= function (){

        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/account/getuserlogged",
            success: function (data) {
                alert("sucess userLogged=" + data);
            },
            error: function (data) {
                alert("You are offline or not logged in");
            }
        });
    }

    var init = function () {
        checkUserLogged();
        dataContext.init(appStorageKey);
        $(document).bind("pagechange", onPageChange);
        $(document).bind("pagebeforechange", onPageBeforeChange);
        $(document).delegate(saveNoteButtonSel, "tap", onSaveNoteButtonTapped);
    };
    var queryStringToObject = function (queryString){ var queryStringObj = {};
      var e;
      var a = /\+/g; // Replace + symbol with a space
      var r = /([^&;=]+)=?([^&;]*)/g;
      var d = function (s) { return decodeURIComponent(s.replace(a, " ")); };
      e = r.exec(queryString);


        while (e) {
            queryStringObj[d(e[1])] = d(e[2]);
            e = r.exec(queryString);
        }
       return queryStringObj;
    };
    return {
        init: init
    };

})(Jassplan.dataContext);

$(document).bind("mobileinit", function () {
  //  Jassplan.testHelper.createDummyNotes("Notes.NotesList");
    Jassplan.controller.init();
});


