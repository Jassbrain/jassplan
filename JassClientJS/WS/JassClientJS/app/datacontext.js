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

    var init = function (storageKey) {
        notesListStorageKey = storageKey;
        loadNotesFromLocalStorage();
    };

    var public = {
        init: init,
        getNotesList: getNotesList
    };
    return public;
})();


