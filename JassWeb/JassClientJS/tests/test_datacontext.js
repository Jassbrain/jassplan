/**
 * Created by pablo on 12/15/13.
 *
 data context functions to test
 
        (*)init: init,
        (*)getNotesList: getNotesList,
        getReviewsList: getReviewsList,
        createBlankNote: createBlankNote,
        (*)saveNote: saveNote,  (we have two cases, create and update)
        deleteNote: deleteNote,
        (*)deleteAllNotes: deleteAllNotes,
        getLogged: getLogged,
        archiveAndReloadNotes: archiveAndReloadNotes
 
 */

var notesListStorageKey = "Jassplan.NotesListTest";


describe("Data context tests", function () {

    //Clen Up Everything
    $.jStorage.deleteKey(notesListStorageKey);
    Jassplan.dataContext.deleteAllNotes();

    it("Create first - Jassplan.dataContext.saveNote/init - New Task", function () {

        // Create a note (at this point local and server storage are empty

        var config = {};
        config.dateCreated = new Date();
        config.id =  config.dateCreated.getTime().toString();
        config.jassActivityID = null;
        config.title = "A1-MindTest";
        config.narrative = "A1-MindTest Narrative";
        config.description = "A1-MindTest Description";
        config.estimatedDuration = 1;
        config.estimatedStartHour = 6;
        config.doneDate = null;
        config.actualDuration = 1;
        var noteModel = new Jassplan.NoteModel(config);

        Jassplan.dataContext.init(notesListStorageKey);
        notesList = $.jStorage.get(notesListStorageKey);

        Jassplan.dataContext.saveNote(noteModel);
        // Should contain a note.
        notesList = $.jStorage.get(notesListStorageKey);
        expect(notesList.length).toBe(1);

    });

    it("Create first - Jassplan.dataContext.saveNote/get notes list - Update Task (after refresh)", function () {

        Jassplan.dataContext.init(notesListStorageKey);
        notesList = Jassplan.dataContext.getNotesList();
        expect(notesList.length).toBe(1); //because we created something before

        noteModel = notesList[0];
        noteModel.title += "2";
        Jassplan.dataContext.saveNote(noteModel);
        // Should contain a note.
        Jassplan.dataContext.init(notesListStorageKey);
        notesList = Jassplan.dataContext.getNotesList();  //this is actually calling server :)
        
        expect(notesList[0].title).toBe("A1-MindTest2");

    });


    it("Creates wierd task", function () {

        // Create a note (at this point local and server storage are empty

        var configJson = "{\"id\":3311,\"jassActivityID\":3311,\"dateCreated\":\"2015-03-06T23:7:22.721\",\"title\":\"tesn\",\"narrative\":\"\",\"description\":\"\",\"estimatedDuration\":\"\",\"estimatedStartHour\":null,\"doneDate\":null,\"actualDuration\":1,\"status\":\"asleep\",\"flag\":\"blue\",\"parentID\":null,\"lastUpdated\":\"2015-03-06T23:7:22.721\",\"Created\":\"2015-03-06T23:7:22.721\"}";
        var noteModel = JSON.parse(configJson);

        Jassplan.dataContext.init(notesListStorageKey);
        notesList = $.jStorage.get(notesListStorageKey);

        Jassplan.dataContext.saveNote(noteModel);
        // Should contain a note.
        notesList = $.jStorage.get(notesListStorageKey);
        expect(notesList.length).toBe(2);

    });

    //

   

});
