var Jassplan = Jassplan || {};

Jassplan.dataContext = (function (serverProxy) {
    var userLogged = false;
    var notesList = [];
    var notesListStorageKey;
    var reviewsList = [];
    var reviewsListStorageKey;

    var getLogged = function () {
        if (userLogged) return ""
        else return "x";
    }

    var loadNotesFromLocalStorage = function () {

        var storedNotes;
        if (userLogged) {
            storedNotes = serverProxy.getTodoLists();
        }
        else {
            storedNotes = $.jStorage.get(notesListStorageKey);
        }

        if (storedNotes !== null) {
            notesList = storedNotes;
        }
    };

    var loadReviewsFromLocalStorage = function () {

        var storedReviews;
        if (userLogged) {
            storedReviews = serverProxy.getReviewLists();
        }
        else {
            storedReviews = $.jStorage.get(reviewsListStorageKey);
        }

        if (storedReviews !== null) {
            reviewsList = storedReviews;
        }
    };

    var archiveAndReloadNotes = function () {
            storedNotes = serverProxy.archiveTodoLists();
            notesList = storedNotes;
    };

    var getNotesList = function () {
        return notesList;
    };

    var getReviewsList = function () {
        return reviewsList;
    };

    var createBlankNote = function () {
        var dateCreated = new Date();
        var id = dateCreated.getTime().toString();
        var config = {};
        config.id = id;
        config.title = "";
        config.description = "";
        config.narrative = "";
        config.dateCreated = dateCreated;
        var noteModel = new Jassplan.NoteModel(config);
        return noteModel; };

    var init = function (storageKey) {
        userLogged = serverProxy.checkUserLogged();
        notesListStorageKey = storageKey;
        loadNotesFromLocalStorage();
        reviewsListStorageKey = storageKey+"review";
        loadReviewsFromLocalStorage();
    };

    var saveNote = function (noteModel) {
        var found = false;
        var i;
        for (i = 0; i < notesList.length; i += 1) {
          if (notesList[i].id === noteModel.id) {
              notesList[i] = noteModel;
              found = true;
              i = notesList.length; } }
        if (!found) {
            Jassplan.serverProxy.createTodoList(noteModel);
            notesList.splice(0, 0, noteModel);
        } else {
            Jassplan.serverProxy.saveTodoList(noteModel);
        }

        saveNotesToLocalStorage();
    };

    var deleteNote = function (noteModel) {
        var found = false;
        var i;
        var ifound;
        for (i = 0; i < notesList.length; i += 1) {
            if (notesList[i].id === noteModel.id) {
                notesList[i] = noteModel;
                found = true;
                ifound = i;
                i = notesList.length;
            }
        }
        if (!found) {
            alert("Note cannot be deleted beucase we could not find it");
        } else {
            Jassplan.serverProxy.deleteTodoList(noteModel);
            notesList.splice(ifound, 1);
        }

        saveNotesToLocalStorage();
    };

    var saveNotesToLocalStorage = function () { $.jStorage.set(notesListStorageKey, notesList); };

    var public = {
        init: init,
        getNotesList: getNotesList,
        getReviewsList: getReviewsList,
        createBlankNote: createBlankNote,
        saveNote: saveNote,
        deleteNote: deleteNote,
        getLogged: getLogged,
        archiveAndReloadNotes: archiveAndReloadNotes
    };

    return public;
})(Jassplan.serverProxy);


