var Jassplan = Jassplan || {};

Jassplan.controller = (function (view, viewModel, helper) {

    var appStorageKey = "Notes.NotesList";
    var notesListPageId = "notes-list-page";
    var noteEditorPageId = "note-editor-page";
    var notesListSelector = "#notes-list-content";
    var noteTitleEditorSel = "[name=note-title-editor]";
    var noteStatusEditorSel = "#note-status-editor";
    var noteFlagEditorSel = "#note-flag-editor";
    var noteNarrativeEditorSel = "[name=note-narrative-editor]";
    var noteDescriptionEditorSel = "[name=note-description-editor]";
    var noteEstimatedDurationEditorSel = "[name=note-estimatedDuration-editor]";
    var noteEstimatedStartHourEditorSel = "[name=note-estimatedStartHour-editor]";
    var noteDoneDateEditorSel = "[name=note-doneDate-editor]";
    var noteActualDurationEditorSel = "[name=note-actualDuration-editor]";
    var currentNote;

    var totalPoints;
    var totalPointsScheduled;
    var totalPointsDone;

    var _lastEventTimestamp;
    var checkAndPreventDuplicatedEvent = function (e) {
        if (e.timeStamp === _lastEventTimestamp) return true;
        else _lastEventTimestamp = e.timeStamp;
        return false;
    }

    var renderViewModel = function(){
        $("#view-model-state").text(viewModel.getState());
        $("#view-model-parent").text(viewModel.getParentName());
        var logged = viewModel.getLogged();
        $("#view-model-logged").text(logged);
        $("#view-model-logged").css('color', 'red');

        if (logged) {
            $("#status-button-label").text("K");
        } else
        {
            $("#status-button-label").text("!");
        }

        var parentId = viewModel.getParent();

    }
    var renderReviewList = function () {

        var reviewsList = viewModel.getReviewsList();


        var view = $(notesListSelector);
        view.empty();
        var ul = $("<ul id=\"notes-list\" data-role=\"listview\"></ul>").appendTo(view);

        for (r = 0; r < reviewsList.length; r += 1){
            var review = reviewsList[r];
        totalPoints = 0;
        totalPointsScheduled = 0;
        totalPointsDone = 0;
        totalPointsDonePlus = 0;
        var jsDoneDate = new Date(review.reviewYear, review.reviewMonth - 1, review.reviewDay)
        var dayOfWeek = jsDoneDate.getDay();

        var doneDate = review.reviewDay + "-" + review.reviewMonth + "-" + review.reviewYear.toString().substring(2);

        var jsDoneDate = new Date(review.reviewYear, review.reviewMonth-1, review.reviewDay)
        var flagColor = "red";
        var flagBanner = "";
        var statusColor = "red";
        var statusBanner = "";
        for (i = 0; i < review.activityHistories.length; i += 1) {
            var notesList = review.activityHistories;

            flagColor = notesList[i].flag;

            if (notesList[i].status == "asleep") { statusColor = "white" };
            if (notesList[i].status == "stared") { statusColor = "yellow" };
            if (notesList[i].status == "done")   { statusColor = "green" };
            if (notesList[i].status == "doneplus") { statusColor = "blue" };
            if (notesList[i].status == "donebad") { statusColor = "red" };

            var notesCount = notesList.length,
    note,
    dateGroup,
    noteDate,
    i;

            var estimatedDuration = notesList[i].estimatedDuration;
            if (notesList[i].estimatedDuration == null) { estimatedDuration = "?" };

            var estimatedStartHour = notesList[i].estimatedStartHour;
            if (notesList[i].estimatedStartHour == null) { estimatedStartHour = 0 };

            var actualDuration = notesList[i].actualDuration;
            if (notesList[i].actualDuration == null) { actualDuration = "?" };

            totalPoints += notesList[i].actualDuration;
            if (notesList[i].status == "stared" || notesList[i].status == "done" || notesList[i].status == "doneplus") {
                totalPointsScheduled += notesList[i].actualDuration;
            }
            if (notesList[i].status == "done" || notesList[i].status == "doneplus") {
                totalPointsDone += notesList[i].actualDuration;
            }
            if (notesList[i].status == "doneplus") {
                totalPointsDonePlus += notesList[i].actualDuration;
            }
            flagBanner += "<div style=\"margin:1px;position:relative; top:3px;height:15px;width:15px;float:left; background-color:" + flagColor + "\"></div>";
            statusBanner += "<div style=\"margin:1px;position:relative; top:3px;height:15px;width:15px;float:left; background-color:" + statusColor + "\"></div>";

        }//end notes loop


            $("<li style=\"min-height:50px\">"
+ "<div style=\"width:90px;float:left\">" + doneDate + "</div>"
+ "<div style=\"min-width:70px\">" + totalPointsDonePlus + "/" + totalPointsDone + "</div>"
+ statusBanner + "<div style=\"clear:both\"><div>"
+ flagBanner
+ "</li>").appendTo(ul);


        } //end reviews loop

        ul.listview();

        $(document).on("tap click", "[name=starimage]", onTapStar);
        $(document).on("taphold", "[name=starimage]", onTapHoldStar);
        $(document).on("tap click", "[name=parentimage]", onTapParent);
        $(document).on("taphold", "[name=parentimage]", onTapHoldParent);

        // alert(totalPointsDone + "/" + totalPointsScheduled + "/" + totalPoints);

       // $("#view-model-done-status").text(totalPointsDonePlusAvg + "/" + totalPointsDoneAvg + "/" + totalPointsScheduledAvg);

    };
    var renderNotesList = function () {

        var state = viewModel.getState();
        if (state == "Review") {
            renderReviewList();
            return;
        }
        notesList = viewModel.getNotesList();

        var notesCount = notesList.length,
            note,
            dateGroup,
            noteDate,
            i;
        var view = $(notesListSelector);
        view.empty();
        var ul = $("<ul id=\"notes-list\" data-role=\"listview\"></ul>").appendTo(view);
        totalPoints=0;
        totalPointsScheduled=0;
        totalPointsDone = 0;
        totalPointsDonePlus = 0;

        //if wew have done date, show done date, otherwise show today
        var dateForDivider = new Date().toDateString();
        for (i = 0; i < notesCount; i += 1) {
            if (notesList[i].doneDate != null) {
                dateForDivider = notesList[i].doneDate.substring(0,10);
            }
        }
        $("<li data-role=\"list-divider\">" + dateForDivider + "</li>").appendTo(ul);


        for (i = 0; i < notesCount; i += 1) {

            noteDate = (new Date(notesList[i].dateCreated)).toDateString();

            /*
            if (dateGroup !== noteDate) {
                $("<li data-role=\"list-divider\">" + noteDate + "</li>").appendTo(ul);
                dateGroup = noteDate;
            }*/

            var starImg = "star_" + notesList[i].status + ".png";
            var parentImg = "subtasks2.png";
          
            var description = notesList[i].description;
            if (notesList[i].description == null) { description = "" };

            var narrative = notesList[i].narrative;
            if (notesList[i].narrative == null) { narrative = "" };

            var narrativeHTML = "";

            narrativeHTML = "<div style=\"min-width:150px;font-weight:normal; font-size:small; font-style:italic\">" + narrative + "</div>";
            var notesListTemp = notesList[i];

            var estimatedDuration = notesList[i].estimatedDuration;
            if (notesList[i].estimatedDuration == null) { estimatedDuration = "?" };

            var estimatedStartHour = notesList[i].estimatedStartHour;
            if (notesList[i].estimatedStartHour == null) { estimatedStartHour = 0 };

            var actualDuration = notesList[i].actualDuration;
            if (notesList[i].actualDuration == null) { actualDuration = "?" };

            totalPoints += notesList[i].actualDuration;
            if (notesList[i].status == "stared" || notesList[i].status == "done" || notesList[i].status == "doneplus") {
                totalPointsScheduled += notesList[i].actualDuration;
            }
            if (notesList[i].status == "done" || notesList[i].status == "doneplus") {
                totalPointsDone += notesList[i].actualDuration;
            }
            if (notesList[i].status == "doneplus") {
                totalPointsDonePlus += notesList[i].actualDuration;
            }

            var flagColor = notesList[i].flag;

            var starimageid = "itemimage" + notesList[i].id;
            var parentimageid = "itemimage" + notesList[i].id;

            $("<li style=\"min-height:50px\">"
            + "<div style=\"min-width:35px;float:left\">" + "<img height=23px width=23px name=\"starimage\" id=\"" + starimageid + "\" src=\"images/" + starImg + "\"/>" + "</div>"
            + "<div style=\"position:relative; top:2px;min-width:35px;float:left\">" + notesList[i].actualDuration + "</div>"
            + "<div style=\"position:relative; top:1px;height:20px;width:20px;float:left; background-color:"+ flagColor +"\"></div>"
            + "<div style=\"height:15px;width:15px;float:left\"></div>"
            + "<div style=\"min-width:150px;float:left\">"
            + "<a href=\"index.html#note-editor-page?noteId=" + notesList[i].id + "\">"
            + notesList[i].title 
            + "</a>"           
            + "</div>"
            + "<div style=\"min-width:35px\">" + "<img height=20px width=20px name=\"parentimage\" id=\"" + parentimageid + "\" src=\"images/" + parentImg + "\"/>" + "</div>"
            + "<div style=\"min-width:35px\">&nbsp;&nbsp;</div>"

   
            + "<div style=\"min-width:150px;font-size:small\">" + description + "</div>"
            + narrativeHTML
            + "</li>").appendTo(ul);
        }
        ul.listview();

        $(document).on("tap click", "[name=starimage]", onTapStar);
        $(document).on("taphold", "[name=starimage]", onTapHoldStar);
        $(document).on("tap click", "[name=parentimage]", onTapParent);
        $(document).on("taphold", "[name=parentimage]", onTapHoldParent);

       // alert(totalPointsDone + "/" + totalPointsScheduled + "/" + totalPoints);

        $("#view-model-done-status").text(totalPointsDonePlus + "/" + totalPointsDone + "/" + totalPointsScheduled);
 
    };
    var renderSelectedNote = function (data) {
        var u = $.mobile.path.parseUrl(data.options.fromPage.context.URL);
        var re = "^#" + noteEditorPageId;
        if (u.hash.search(re) !== -1) {

            var queryStringObj = helper.queryStringToObject(data.options.queryString);
            var titleEditor = $(noteTitleEditorSel);
            var statusEditor = $(noteStatusEditorSel);
            var flagEditor = $(noteFlagEditorSel);
            var narrativeEditor = $(noteNarrativeEditorSel);
            var descriptionEditor = $(noteDescriptionEditorSel);
            var estimatedDurationEditor = $(noteEstimatedDurationEditorSel);
            var estimatedStartHourEditor = $(noteEstimatedStartHourEditorSel);
            var doneDateEditor = $(noteDoneDateEditorSel);
            var actualDurationEditor = $(noteActualDurationEditorSel);
            var noteId = queryStringObj["noteId"];

            if (typeof noteId !== "undefined") {  //here we are supposed to load the values into the fields
                var notesList = viewModel.getNotesList();

                var notesCount = notesList.length;
                var note;
                for (var i = 0; i < notesCount; i++) {
                    note = notesList[i];
                    if (noteId == note.jassActivityID) {
                        titleEditor.val(note.title);
                        updateStatusEditor(statusEditor, note.status);
                        updateFlagEditor(flagEditor, note.flag);
                        flagEditor.val(note.flag);
                        narrativeEditor.val(note.narrative);
                        descriptionEditor.val(note.description);
                        estimatedDurationEditor.val(note.estimatedDuration);
                        estimatedStartHourEditor.val(note.estimatedStartHour);
                        doneDateEditor.val(note.doneDate);
                        actualDurationEditor.val(note.actualDuration);
                        currentNote = note;
                        break;
                    }
                }
                titleEditor.focus();
            }
        }
        return data;
    };
    var updateStatusEditor = function (statusEditor, status) {
        statusEditor.val(status);
        statusEditor.selectmenu("refresh");
    };
    var updateFlagEditor = function (flagEditor, flag) {
        flagEditor.val(flag);
        flagEditor.selectmenu("refresh");
    };
    var onPageBeforeChange = function (event, data) {
        var titleEditor = $(noteTitleEditorSel);
        var descriptionEditor = $(noteDescriptionEditorSel);
        var estimatedDurationEditor = $(noteEstimatedDurationEditorSel);
        var estimatedStartHourEditor = $(noteEstimatedStartHourEditorSel);
        var doneDateEditor = $(noteDoneDateEditorSel);
        var actualDurationEditor = $(noteActualDurationEditorSel);
        var narrativeEditor = $(noteNarrativeEditorSel);
        titleEditor.val("");
        narrativeEditor.val("");
        descriptionEditor.val("");
        estimatedDurationEditor.val("");
        estimatedStartHourEditor.val("");
        doneDateEditor.val("");
        actualDurationEditor.val("");
        currentNote = null;

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
    var getNoteFromEditor = function () {

        var titleEditor = $(noteTitleEditorSel);
        var narrativeEditor = $(noteNarrativeEditorSel);
        var descriptionEditor = $(noteDescriptionEditorSel);
        var estimatedDurationEditor = $(noteEstimatedDurationEditorSel);
        var estimatedStartHourEditor = $(noteEstimatedStartHourEditorSel);
        var doneDateEditor = $(noteDoneDateEditorSel);
        var actualDurationEditor = $(noteActualDurationEditorSel);
        var note = viewModel.createBlankNote();
        note.title = titleEditor.val();
        note.narrative = narrativeEditor.val();
        note.description = descriptionEditor.val();
        note.status = $(noteStatusEditorSel).val();
        note.flag = $(noteFlagEditorSel).val();
        note.estimatedDuration = $(noteEstimatedDurationEditorSel).val();
        note.estimatedStartHour = $(noteEstimatedStartHourEditorSel).val();
        note.doneDate = $(noteDoneDateEditorSel).val();
        note.actualDuration = $(noteActualDurationEditorSel).val();

        return note;
    };

    var onTapStar = function (e) {
        if (checkAndPreventDuplicatedEvent(e)) return;
        var id = e.currentTarget.id;
        var taskId = id.replace("itemimage","");
        var taskStatus = viewModel.star(taskId);
        $("#" + id).attr("src", "images/star_" + taskStatus + ".png");
    }
    var onTapHoldStar = function (e) {
        if (checkAndPreventDuplicatedEvent(e)) return;
        var id = e.currentTarget.id;
        var taskId = id.replace("itemimage", "");
        var taskStatus = viewModel.unstar(taskId);
        $("#" + id).attr("src", "images/star_" + taskStatus + ".png");
    }
    var onTapParent = function (e) {
        if (checkAndPreventDuplicatedEvent(e)) return;
        var id = e.currentTarget.id;
        var taskId = id.replace("itemimage", "");
        var taskStatus = viewModel.starparent(taskId);
        refresh();
    }
    var onTapHoldParent = function (e) {
        if (checkAndPreventDuplicatedEvent(e)) return;
        var id = e.currentTarget.id;
        var taskId = id.replace("itemimage", "");
        var taskStatus = viewModel.unstarparent(taskId);
        refresh();
    }
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
    var onSaveNoteButtonTapped = function () {
        //This method will both save a note or create it depending on
        //whether we have a current note or not. If we do we just copy
        //the key fields preserving other data like ids, timestamps..
        var newNoteFromEditor = getNoteFromEditor();

        if (newNoteFromEditor.isValid()) {
            if (currentNote !==  null) { // So we save
                currentNote.title = newNoteFromEditor.title;
                currentNote.narrative = newNoteFromEditor.narrative;
                currentNote.description = newNoteFromEditor.description;
                currentNote.estimatedDuration = newNoteFromEditor.estimatedDuration;
                currentNote.estimatedStartHour = newNoteFromEditor.estimatedStartHour;
                currentNote.doneDate = newNoteFromEditor.doneDate;
                currentNote.actualDuration = newNoteFromEditor.actualDuration;
                currentNote.status = newNoteFromEditor.status;
                currentNote.flag = newNoteFromEditor.flag;
            }
            else { //So we save a new note..this is a create
                currentNote = newNoteFromEditor;
                currentNote.parentID = viewModel.getParent();
            }
            viewModel.saveNote(currentNote);
           // returnToNotesListPage();
        }
        else {
            alert('onSaveNoteButtonTapped - We could not get a valid Note from the Editor');
        };
    }
    var onDeleteNoteButtonTapped = function () {
        var tempNote = getNoteFromEditor();

        if (tempNote.isValid()) {
            if (null !== currentNote) {
                currentNote.title = tempNote.title;
                currentNote.narrative = tempNote.narrative;
                currentNote.description = tempNote.description;
                currentNote.estimatedDuration = tempNote.estimatedDuration;
                currentNote.estimatedStartHour = tempNote.estimatedStartHour;
                currentNote.doneDate = tempNote.doneDate;
                currentNote.actualDuration = tempNote.actualDuration;
                currentNote.status = tempNote.status;
                currentNote.flag = tempNote.flag;
            }
            else {
                currentNote = tempNote;
            }
            viewModel.deleteNote(currentNote);
            // returnToNotesListPage();
        }
        else {
            alert('temp Note is Invalid');
        };
       // refresh();
    }
    var onRefreshButtonTapped = function () {
        viewModel.refresh();
        var href = window.location;
        var hrefNew = window.location.protocol + "//" + window.location.host;
        window.location.href = hrefNew;
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
    var onArchiveButtonTapped = function () {
        viewModel.handleArchiveAction();
        refresh();
    }

    var onStatusButtonTapped = function () {
        viewModel.viewStatus();
        var hrefNew = window.location.protocol + "//" + window.location.host;
        window.location.href = hrefNew + "/account/logoff";
    }
    var onDeleteButtonTapped = function () {
        viewModel.handleDeleteAction();
        refresh();
    }
    var refresh = function () {
        window.location.href = window.location.href;
    }
    var refreshAll = function () {
        viewModel.refresh(appStorageKey);
        refresh();
    }
    var onOfflineTapped = function () {
        var href = window.location;
        var hrefNew = window.location.protocol + "//" + window.location.host;
        window.location.href = hrefNew;
        return;
    }
    var onParentNameTapped = function () {
        viewModel.unstarcurrentparent();
        refresh();
    }

    var init = function () {
        viewModel.init(appStorageKey);
        $(document).on("tap", "#view-model-logged", onOfflineTapped);
        $(document).on("pagechange", onPageChange);
        $(document).on("pagebeforechange", onPageBeforeChange);
        $(document).on("tap", "#save-note-button", null, onSaveNoteButtonTapped);
        $(document).on("tap", "#refresh-button", null, onRefreshButtonTapped);
        $(document).on("tap", "#plan-button", null, onPlanButtonTapped);
        $(document).on("tap", "#do-button", null, onDoButtonTapped);
        $(document).on("tap", "#review-button", null, onReviewButtonTapped);
        $(document).on("tap", "#archive-button", null, onArchiveButtonTapped);
        $(document).on("tap", "#status-button", null, onStatusButtonTapped);
        $(document).on("tap", "#delete-button", null, onDeleteNoteButtonTapped);
        $(document).on("tap", "#view-model-parent", null, onParentNameTapped);
    };
   
    return {
        init: init
    };

})(Jassplan.view, Jassplan.viewmodel, Jassplan.helper);

$(document).bind("mobileinit", function () {
    Jassplan.controller.init();
});


