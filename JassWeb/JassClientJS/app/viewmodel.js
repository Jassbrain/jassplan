var Jassplan = Jassplan || {};

Jassplan.viewmodel = (function (dataContext) {

    var appStorageKey = "Notes.NotesList";
    var state = "Do";
    var stateStorageKey = "Notes.State";

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
  
    return {
        init: init,
        getNotesList: getNotesList,
        createBlankNote: createBlankNote,
        saveNote: saveNote,
        getState: getState,
        getLogged: getLogged,
        setStatePlan: setStatePlan,
        setStateDo: setStateDo,
        setStateReview: setStateReview
    };

})(Jassplan.dataContext);


