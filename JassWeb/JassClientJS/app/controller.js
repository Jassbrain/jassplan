var Jassplan = Jassplan || {};

Jassplan.controller = (function (viewModel, helper) {

    var appStorageKey = "Notes.NotesList";
    var notesListPageId = "notes-list-page";
    var noteEditorPageId = "note-editor-page";
    var notesListSelector = "#notes-list-content";
    var noteTitleEditorSel = "[name=note-title-editor]";
    var noteNarrativeEditorSel = "[name=note-narrative-editor]";
    var currentNote;

    var renderViewModel = function(){
        $("#view-model-state").text(viewModel.getState());
        var logged = viewModel.getLogged()
        $("#view-model-logged").text(logged);
        $("#view-model-logged").css('color', 'red');
    }

    var renderNotesList = function () {

        notesList = viewModel.getNotesList();

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

            var starImg = "star.png";
            var doneImg = "done.png";
            $("<li>"
            + "<a href=\"index.html#note-editor-page?noteId=" + notesList[i].id + "\">"
            + "<div class=\"list-item-title\">"
                + "<table><tr>"
                + "<td style=\"min-width:200px\">" + notesList[i].title + "</td>"
                + "<td style=\"min-width:50px\">" + "<img src=\"images/" + starImg + "\"/>" + "</td>"
                + "<td style=\"min-width:50px\">" + "<img src=\"images/" + doneImg + "\"/>" + "</td>"
                + "</tr></table>"
            + "</div>"
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
                renderViewModel();
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

        var queryStringObj = helper.queryStringToObject(data.options.queryString);
        var titleEditor = $(noteTitleEditorSel);
        var narrativeEditor = $(noteNarrativeEditorSel);
        var noteId = queryStringObj["noteId"];

        if (typeof noteId !== "undefined")
        {  //here we are supposed to load the values into the fields
            var notesList = viewModel.getNotesList();

            var notesCount = notesList.length;
            var note;
            for (var i = 0; i < notesCount; i++) {
                note = notesList[i];
                if (noteId == note.id) {
                    titleEditor.val(note.title);
                    narrativeEditor.val(note.narrative);
                    currentNote=note;
                    break;
                }
            }
            titleEditor.focus();
        }
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
        var tempNote = viewModel.createBlankNote();

        tempNote.title = titleEditor.val();
        tempNote.narrative = narrativeEditor.val();

        if (tempNote.isValid()) {
            if (null !== currentNote) {
                currentNote.title = tempNote.title;
                currentNote.narrative = tempNote.narrative; }
            else {
                currentNote = tempNote; }
            viewModel.saveNote(currentNote);
           // returnToNotesListPage();
        }
        else {
            alert('temp Note is Invalid');
        };
    }

    var onRefreshButtonTapped = function () {        
        refresh();
    }
    var onPlanButtonTapped = function () {
        viewModel.setStatePlan();
        refresh();
    }
    var onDoButtonTapped = function () {
        viewModel.setStateDo();
        refresh();
    }
    var onReviewButtonTapped = function () {
        viewModel.setStateReview();
        refresh();
    }

    var refresh = function(){

        window.location.href = window.location.href;
    }

    var init = function () {
        viewModel.init(appStorageKey);
        $(document).on("pagechange", onPageChange);
        $(document).on("pagebeforechange", onPageBeforeChange);
        $(document).on("tap", "#save-note-button", null, onSaveNoteButtonTapped);
        $(document).on("tap", "#refresh-button", null, onRefreshButtonTapped);
        $(document).on("tap", "#plan-button", null, onPlanButtonTapped);
        $(document).on("tap", "#do-button", null, onDoButtonTapped);
        $(document).on("tap", "#review-button", null, onReviewButtonTapped);
    };
   
    return {
        init: init
    };

})(Jassplan.viewmodel, Jassplan.helper);

$(document).bind("mobileinit", function () {
    Jassplan.controller.init();
});


