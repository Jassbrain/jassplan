/**
 * Created by pablo on 12/15/13.
 */

var Jassplan = Jassplan || {};

Jassplan.controller = (function (dataContext) {

    var appStorageKey = "Notes.NotesList";
    var notesListPageId = "notes-list-page";
    var notesListSelector = "#notes-list-content";

    var renderNotesList = function () {

        var notesList = dataContext.getNotesList();

        var notesCount = notesList.length,
            note,
            i;
        var view = $(notesListSelector);
        view.empty();
        var ul = $("<ul id=\"notes-list\" data-role=\"listview\"></ul>").appendTo(view);
        for (i = 0; i < notesCount; i += 1) {
            $("<li>"
            + "<a href=\"index.html#note-editor-page?noteId=" + notesList[i].id + "\">"
            + "<div>Note title " + notesList[i].title + "</div>"
            + "<div class=\"list-item-narrative\">Note Narrative " + notesList[i].narrative + "</div>"
            + "</a>"
            + "</li>").appendTo(ul);
        }
        ul.listview();
    };

    var onPageChange = function (event, data) {
        var toPageId = data.toPage.attr("id");
        switch (toPageId) {
            case notesListPageId:
                renderNotesList();
                break;
        }
    };

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


