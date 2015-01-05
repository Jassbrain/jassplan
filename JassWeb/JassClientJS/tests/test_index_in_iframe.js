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

    it("Check User Logged is 'test' and cleanup All Notes", function () {
        var user = Jassplan().viewmodel.getUserName();
        if (user == "test") {
            $.jStorage.deleteKey(notesListStorageKey);
            Jassplan().dataContext.deleteAllNotes();
        }
        expect(user).toBe("test");
    });

    it("Access Plan View", function () {
        Jassplan().triggerclick("#plan-button");
        //expect   <td><h3 id="view-model-state">Do</h3></td>
        var stateShownInPage = Jassplan().gettext("#view-model-state");
        expect(stateShownInPage).toBe("Plan");
        //Expect view model change
        var stateShownInModelView = Jassplan().viewmodel.getState();
        expect(stateShownInModelView).toBe("Plan");
    });


    it("Create New Subject While in Plan View", function () {
        Jassplan().triggerclick("#new-button");
        Jassplan().entertext("#note-title-editor", "something");
        expect(true).toBe(true);
    });


    it("LastTest", function () {
        expect(true).toBe(true);
    });

});
