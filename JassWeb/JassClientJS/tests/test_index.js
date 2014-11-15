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

    //Clen Up Everything
    $.jStorage.deleteKey(notesListStorageKey);
    Jassplan.dataContext.deleteAllNotes();

    it("Test a==a on index page", function () {

        expect("a").toBe("a");
    });

    it("Test a==b on index page ", function () {

        expect("a").toBe("b");
    });

    it("LastTest", function () {
        expect(true).toBe(true);
    });

});
