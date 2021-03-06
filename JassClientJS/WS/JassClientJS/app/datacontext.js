/**
 * Created by pablo on 12/15/13.
 */

var Jassplan = Jassplan || {};

Jassplan.dataContext = (function () {
    var notesList = [];
    var notesListStorageKey;

    var loadNotesFromLocalStorage = function () {
        var storedNotes = $.jStorage.get(notesListStorageKey);
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
        notesListStorageKey = storageKey;
        loadNotesFromLocalStorage();
    };

    var saveNote = function (noteModel) {
        var xxx = 1;
        var found = false;
        var i;
        for (i = 0; i < notesList.length; i += 1) {
          if (notesList[i].id === noteModel.id) {
              notesList[i] = noteModel;
              found = true;
              i = notesList.length; } }
        if (!found) { notesList.splice(0, 0, noteModel); }

        saveNotesToLocalStorage(); };

    var saveNotesToLocalStorage = function () { $.jStorage.set(notesListStorageKey, notesList); };

    var public = {
        init: init,
        getNotesList: getNotesList,
        createBlankNote: createBlankNote,
        saveNote: saveNote
    };
    return public;
})();


