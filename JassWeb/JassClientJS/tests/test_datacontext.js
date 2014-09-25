/**
 * Created by pablo on 12/15/13.
 */

var notesListStorageKey = "Jassplan.NotesListTest";


describe("Data context tests", function () {

    //General Tet Setup
    // Clean Test Local Storage
    $.jStorage.deleteKey(notesListStorageKey);
    //Delete All Notes for Test User
    Jassplan.dataContext.deleteAllNotes();

    it("Create first - Jassplan.dataContext.saveNote - New Task", function () {

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

    it("Create first - Jassplan.dataContext.saveNote - Update Task (after refresh)", function () {

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
});