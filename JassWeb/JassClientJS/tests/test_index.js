/**
 * Jassplan UI Integration Test
 * This is a large and comprehesive integration / regression test
 * to go through the most important uses cases
 */

var notesListStorageKey = "Jassplan.NotesListTest";
var failedTests = "";

afterEach(function () {
    if (this.results_.failedCount > 0) {
        alert("Test Failed: " + this.description);
        failedTests += this.description + "\n";
        jasmine.Queue.prototype.next_ = function () {
            // to skip to the end and stop processing
            this.onComplete();
        }
    }

    if (this.description == "LastTest") {
        if (failedTests == "") {
            alert("All Test Passed!");
        } else {
            alert("Test Failed: " + this.description);
        }
    }
});


describe("Test Index Page", function () {

    it("Check Test User Logged / Cleanup All Notes", function () {
        //Clen Up Everything
        //First, make sure this is the "test" user
        var user = Jassplan.viewmodel.getUserName();
        expect(user).toBe("test");
        $.jStorage.deleteKey(notesListStorageKey);
        Jassplan.dataContext.deleteAllNotes();
        expect(true).toBe(true);
    });

    it("Open task edit screen from New Task button", function () {
        var urlBefore = location.toString();
        $("#new-button").trigger("click");
        var urlAfter = location.toString();
        expect(urlBefore).not.toBe(urlAfter);
    });

   
    it("Type a new name for a new task", function () {
       var urlBefore = location.toString();
       // $("#new-button").trigger("click");
       // var urlAfter = location.toString();
       // expect(urlBefore).not.toBe(urlAfter);
    });

    it("Test a==b on index page ", function () {

        expect(true).toBe(true);
    });

    it("LastTest", function () {
        expect(true).toBe(true);
    });

});
