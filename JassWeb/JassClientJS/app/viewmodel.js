var Jassplan = Jassplan || {};

Jassplan.viewmodel = (function (dataContext) {

    var appStorageKey = "Notes.NotesList";
    var state = "Do";
    var parent = null;
    var parentName = null;
    var stateStorageKey = "Notes.State";
    var parentStorageKey = "Notes.Parent";
    var notesList;
    var reviewsList;

    var getState = function (){
        state = $.jStorage.get(stateStorageKey);
        return state;
    }

    var getParent = function () {
        parent = $.jStorage.get(parentStorageKey);
        return parent;
    }

    var getParentName = function () {
        return parentName;
    }


    var getLogged = function () {
        var logged = dataContext.getLogged();
        return logged;
    }

    var setParent = function (parentin) {
        parent = parentin;
        $.jStorage.set(parentStorageKey, parent);
    }

    var setStatePlan = function (){
        state = "Plan";
        $.jStorage.set(stateStorageKey, state);
    }
    var setStateDo = function () {
        state = "Do";
        $.jStorage.set(stateStorageKey, state);
    }
    var setStateReview = function () {
        state = "Review";
        $.jStorage.set(stateStorageKey, state);
    }

    var handleArchiveAction = function () {
        dataContext.archiveAndReloadNotes();
    }

    var handleDeleteAction = function () {
        dataContext.archiveAndReloadNotes();
    }

    var deleteNote = function (currentNote) {
        var result = dataContext.deleteNote(currentNote);
        return result;
    };

    var noteForId = function(id){

        for(var i=0; i<notesList.length; i++){

            if (notesList[i].id==id) return i;
        };

        return -1;

    }

    var starparent = function (id) {
        var i = this.noteForId(id);
        if (i == -1) return;
        var note = notesList[i];
        setParent(note.id);
    }

    var unstarparent = function (id) {
        var i = this.noteForId(id);
        if (i == -1) return;
        var note = notesList[i];
        setParent(note.parentID);
    }

    var unstarcurrentparent = function () {
        var i = this.noteForId(parent);
        if (i == -1) return;
        var note = notesList[i];
        setParent(note.parentID);
    }

    var star = function (id) {

        var i = this.noteForId(id);
        if (i == -1) return;
        var note = notesList[i];

        if (state == "Plan") {
            if (note.status == null || note.status == "asleep") note.status = "stared";
        }
        if (state == "Do") {
            if (note.status == "done"){
                note.status = "doneplus";
            }
            if (note.status == "stared") {
                note.status = "done";
                note.doneDate = new Date().toDateString();
            }
        }

        if (state == "Review") {
            alert("this shold not happen");
        }



        this.saveNote(note);

        return note.status;
    }

    var unstar = function (id) {

        var i = this.noteForId(id);
        if (i == -1) return;
        var note = notesList[i];

        if (state == "Plan") {
            if (note.status == "stared") note.status = "asleep";
        }
        if (state == "Do") {
            if (note.status == "done") note.status = "stared";
            if (note.status == "doneplus") note.status = "done";
        }

        if (state == "Review") {
            if (note.status == "done") note.status = "stared";
            if (note.status == "doneplus") note.status = "done";
        }

        this.saveNote(note);

        return note.status;
    }

    var getNotesList = function () {

        var filteredNotesList = [];

        for (var i = 0; i < notesList.length; i++) {
            if (notesList[i].parentID == parent || notesList[i].id == parent) {
            if (state == "Do" && notesList[i].status != null && notesList[i].status != "asleep") filteredNotesList.push(notesList[i]);
            if (state == "Plan") filteredNotesList.push(notesList[i]);
            if (state == "Review" && notesList[i].status != null && notesList[i].status != "asleep") filteredNotesList.push(notesList[i]);
            }
        }

        return filteredNotesList;
    };

    var getReviewsList = function () {

        var filteredReviewsList = [];

        for (var r = 0; r < reviewsList.length; r++) {
            var review = reviewsList[r];
            var notesListR = reviewsList[r].activityHistories;
            var filteredNotesList = [];

            for (var i = 0; i < notesListR.length; i++) {
                if (notesListR[i].parentID == parent || notesListR[i].jassActivityID == parent) {
                   filteredNotesList.push(notesListR[i]);
                }
            }
            review.activityHistories = filteredNotesList;
            filteredReviewsList.push(review);
        }

        return filteredReviewsList;
    };

    var createBlankNote = function () {

        var blankNote = dataContext.createBlankNote();

        return blankNote;
    };

    var saveNote = function (currentNote) {

        if (currentNote.doneDate == null) {
            if (currentNote.status == "done" && currentNote.status == "donebad") {
                currentNote.doneDate = new Date();
            }
        } else {
            if (currentNote.status == "asleep") {
                currentNote.doneDate = null;
            }
        }
        var result = dataContext.saveNote(currentNote);

        return result;
    };

    var init = function () {
        dataContext.init(appStorageKey);
        parent = getParent();
        notesList = dataContext.getNotesList();
        reviewsList = dataContext.getReviewsList();
        for (var i = 0; i < notesList.length; i++) {
            if (notesList[i].id == parent) {
                parentName = notesList[i].title;
            }
        }
        if (parentName == null) { parentName = "";}
    };

    var public = {
        init: init,
        getNotesList: getNotesList,
        getReviewsList: getReviewsList,
        createBlankNote: createBlankNote,
        saveNote: saveNote,
        deleteNote: deleteNote,
        getState: getState,
        getParentName: getParentName,
        getParent: getParent,
        getLogged: getLogged,
        setStatePlan: setStatePlan,
        setStateDo: setStateDo,
        setStateReview: setStateReview,
        star: star,
        unstar: unstar,
        starparent: starparent,
        unstarparent: unstarparent,
        unstarcurrentparent: unstarcurrentparent,
        noteForId: noteForId,
        handleArchiveAction: handleArchiveAction
   }

    return public;

})(Jassplan.dataContext);


