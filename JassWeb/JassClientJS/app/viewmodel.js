var Jassplan = Jassplan || {};

Jassplan.viewmodel = (function (dataContext) {

    var appStorageKey = "Notes.NotesList";
    var state = "Do";

    var getState = function (){
        return state;
    }

    var getLogged = function () {
        return dataContext.getLogged();
    }

    var setStatePlan = function (){
        state = "Plan"; 
    }
    var setStateDo = function () {
        state = "Do"; 
    }
    var setStateReview = function () {
        state = "Review";
    }

    var getNotesList = function () {

        notesList = dataContext.getNotesList();

        return notesList;
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
        setStateReview: setStateReview,
    };

})(Jassplan.dataContext);


