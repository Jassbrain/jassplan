/**
 * Created by pablo on 12/15/13.
 */

var Jassplan = Jassplan || {};

Jassplan.testHelper = (function () {
    var createDummyNotes = function (notesListStorageKey) {
        var notesCount = 10;
        var notes = [];
        for (var i = 0; i < notesCount; i++) {
            var config = {};
            var dateCreated = new Date();
            config.id = i.toString();
            config.title = "Titlex " + i;
            config.narrative = "Narrativex " + i;
            config.dateCreated = dateCreated;
            var note = new Jassplan.NoteModel(config);
            notes.push(note);
        }
        $.jStorage.set(notesListStorageKey, notes);
    };
    var public = {
        createDummyNotes: createDummyNotes
    };
    return public;
})();
