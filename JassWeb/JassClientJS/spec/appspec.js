/**
 * Created by pablo on 12/15/13.
 */

var notesListStorageKey = "Jassplan.NotesListTest";


describe("Data context tests", function () {
    it("App's namespace exists", function () {
        expect(Jassplan).toBeDefined();
    });
    it("dataContext module exists", function () {
       expect(Jassplan.dataContext).toBeDefined();
    });
    it("Returns notes Array", function () {
        var notesList = Jassplan.dataContext.getNotesList();
        expect(notesList instanceof Array).toBeTruthy();
    });
    it("Has init function", function () {
        expect(Jassplan.dataContext.init).toBeDefined();
    });

    it("Jquestorage defined", function () {
        expect(Jassplan.dataContext.init).toBeDefined();
    });

    it("Returns dummy notes saved in local storage", function () {
        var x=1;
        Jassplan.testHelper.createDummyNotes(notesListStorageKey);
        Jassplan.dataContext.init(notesListStorageKey);
        var notesList = Jassplan.dataContext.getNotesList();
        expect(notesList.length > 0).toBeTruthy();
        for (var i = 0; i < notesList.length; i+=1 ) {
            expect(notesList[i].id).toBeTruthy();
        }
    });

    it("Saves a note to local storage", function () {
        // Make sure LS is empty before the test.
        $.jStorage.deleteKey(notesListStorageKey);
        var notesList = $.jStorage.get(notesListStorageKey);
        expect(notesList).toBeNull();
        // Create a note.
        var dateCreated = new Date();
        var id = dateCreated.getTime().toString();
        var noteModel = new Jassplan.NoteModel({ id: id, dateCreated: dateCreated, title: "", narrative: "" });
        Jassplan.dataContext.init(notesListStorageKey);
        notesList = $.jStorage.get(notesListStorageKey);
        Jassplan.dataContext.saveNote(noteModel);
        // Should contain a note.
        notesList = $.jStorage.get(notesListStorageKey);
        expect(notesList.length).toBe(11);
        // Clean up
        $.jStorage.deleteKey(notesListStorageKey); });
});
