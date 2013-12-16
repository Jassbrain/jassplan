/**
 * Created by pablo on 12/15/13.
 */

var Jassplan = Jassplan || {};

Jassplan.controller = (function () {

    var notesListPageId = "notes-list-page";
    var notesListSelector = "#notes-list-content";

    var renderNotesList = function () {
        var dummyNotesCount = 10,
            note,
            i;
        var view = $(notesListSelector);
        view.empty();
        var ul = $("<ul id=\"notes-list\" data-role=\"listview\"></ul>").appendTo(view);
        for (i = 0; i < dummyNotesCount; i += 1) {
            $("<li>"
            + "<a href=\"index.html#note-editor-page?noteId=" + i + "\">"
            + "<div>Note title " + i + "</div>"
            + "<div class=\"list-item-narrative\">Note Narrative " + i + "</div>"
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
        $(document).bind("pagechange", onPageChange);
    };

    var public = {
        init: init
    };

    return public;

})();

$(document).bind("mobileinit", function () {
    Jassplan.controller.init();

});


