/**
 * Created by pablo on 12/15/13.
 */

var Jassplan = Jassplan || {};

Jassplan.controller = (function (dataContext) {

    var appStorageKey = "Notes.NotesList";
    var notesListPageId = "notes-list-page";
    var noteEditorPageId = "note-editor-page";
    var notesListSelector = "#notes-list-content";
    var noteTitleEditorSel = "[name=note-title-editor]";
    var noteNarrativeEditorSel = "[name=note-narrative-editor]";


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

        if (typeof noteId !== "undefined") { alert('Ji'); } else { alert('Jo');}
        }

        return data;};

    var init = function () {
        dataContext.init(appStorageKey);
        $(document).bind("pagechange", onPageChange);
    };

    return {
        init: init
    };

})(Jassplan.dataContext);

$(document).bind("mobileinit", function () {
    Jassplan.testHelper.createDummyNotes("Notes.NotesList");
    Jassplan.controller.init();

});


