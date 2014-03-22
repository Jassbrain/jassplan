var Jassplan = Jassplan || {};

Jassplan.viewmodel = (function (dataContext) {

    var appStorageKey = "Notes.NotesList";
    var state = "Do";
    var stateStorageKey = "Notes.State";
    var noteList;

    var getState = function (){
        state = $.jStorage.get(stateStorageKey);
        return state;
    }

    var getLogged = function () {
        var logged = dataContext.getLogged();
        return logged;
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

    var noteForId = function(id){

        for(var i=0; i<notesList.length; i++){

            if (notesList[i].id==id) return i;
        };

        return -1;

    }

    var star = function (id) {

        var i = this.noteForId(id);
        if (i == -1) return;
        var note = notesList[i];

        if (note.status == "done") note.status = "done++";
        if (note.status == "stared") note.status = "done";
        if (note.status == null || note.status == "asleep") note.status = "stared";


        this.saveNote(note);
    }

    var unstar = function (id) {

        var i = this.noteForId(id);
        if (i == -1) return;
        var note = notesList[i];

        if (note.status == "stared") note.status = "asleep";
        if (note.status == "done") note.status = "stared";
        if (note.status == "done++") note.status = "done";

        this.saveNote(note);
    }

    var getNotesList = function () {

        notesList = dataContext.getNotesList();

        var filteredNotesList = [];

        for (var i = 0; i < notesList.length; i++) {

            if (state == "Do" && notesList[i].status != null && notesList[i].status != "asleep") filteredNotesList.push(notesList[i]);
            if (state == "Plan") filteredNotesList.push(notesList[i]);
            if (state == "Review" && notesList[i].status != null && notesList[i].status != "asleep") filteredNotesList.push(notesList[i]);


        }

        return filteredNotesList;
    };

    var createBlankNote = function () {

        var blankNote = dataContext.createBlankNote();

        return blankNote;
    };

    var saveNote = function (currentNote) {

        var result = dataContext.saveNote(currentNote);

        return result;
    };

    var init = function () {
        dataContext.init(appStorageKey);
    };

    var public = {
        init: init,
        getNotesList: getNotesList,
        createBlankNote: createBlankNote,
        saveNote: saveNote,
        getState: getState,
        getLogged: getLogged,
        setStatePlan: setStatePlan,
        setStateDo: setStateDo,
        setStateReview: setStateReview,
        star: star,
        noteForId: noteForId,
        handleArchiveAction: handleArchiveAction
    }

    return public;

})(Jassplan.dataContext);


