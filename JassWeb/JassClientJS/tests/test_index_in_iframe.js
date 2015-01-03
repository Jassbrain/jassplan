/**
 * Jassplan UI Integration Test
 * This is a large and comprehesive integration / regression test
 * to go through the most important uses cases
 */

var notesListStorageKey = "Jassplan.NotesListTest";
var failedTests = "";
var testframe = $('#testFrame');

function Jassplan() {
    return window.frames[0].window.Jassplan;
}

function Location() {
    return window.frames[0].window.location;
}

function FindInFrame(q) {
    return $('#testFrame').contents().find(q);
}

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
        jasmine.Queue.prototype.next_ = function () {
            // to skip to the end and stop processing
            this.onComplete();
        }
    }
});


describe("Test Index Page", function () {

    it("Check Test User Logged / Cleanup All Notes", function () {
        //Clen Up Everything
        //First, make sure this is the "test" user
        var user = Jassplan().viewmodel.getUserName();

        if (user == "test") {
            $.jStorage.deleteKey(notesListStorageKey);
            Jassplan().dataContext.deleteAllNotes();
        }
        expect(user).toBe("test");
    });

    it("Access Plan View", function () {
        Jassplan().triggerclick("#plan-button");
        //expect being in the plan view ??
        expect(true).toBe(true);
    });

/*
    it("Open task edit screen from New Task button", function () {
        Jassplan().triggerclick("#new-button");
        Jassplan().entertext("#note-title-editor","something");
    });
*/

    it("LastTest", function () {
        expect(true).toBe(true);
    });

});
