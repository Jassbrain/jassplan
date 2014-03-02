var Jassplan = Jassplan || {};

Jassplan.dataContext = (function (serverProxy) {
    var userLogged = false;
    var notesList = [];
    var notesListStorageKey;

    var getLogged = function () {
        if (userLogged) return "Online"
        else return "Offline";
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

    var getNotesList = function () {
        return notesList;
    };

    var createBlankNote = function () {
        var dateCreated = new Date();
        var id = dateCreated.getTime().toString();
        var config = {};
        config.id = id;
        config.title = "";
        config.narrative = "";
        config.dateCreated = dateCreated;
        var noteModel = new Jassplan.NoteModel(config);
        return noteModel; };

    var init = function (storageKey) {
        userLogged = serverProxy.checkUserLogged();
        notesListStorageKey = storageKey;
        loadNotesFromLocalStorage();
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

        saveNotesToLocalStorage(); };

    var saveNotesToLocalStorage = function () { $.jStorage.set(notesListStorageKey, notesList); };

    var public = {
        init: init,
        getNotesList: getNotesList,
        createBlankNote: createBlankNote,
        saveNote: saveNote,
        getLogged: getLogged
    };
    return public;
})(Jassplan.serverProxy);


