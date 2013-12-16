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

});
