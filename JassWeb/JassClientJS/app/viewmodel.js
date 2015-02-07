var Jassplan = Jassplan || {};

Jassplan.viewmodel = (function (dataContext) {

    var appStorageKey = "Notes.NotesList";
    var state = "Do";
    var parent = null;
    var parentName = null;
    var stateStorageKey = "Notes.State";
    var parentStorageKey = "Notes.Parent";
    var notesList;
    var reviewsList;

    var totalPoints;
    var totalPointsScheduled;
    var totalPointsDone;
    var totalPointsDonePlus;

    var getState = function (){
        state = $.jStorage.get(stateStorageKey);
        return state;
    }

    var getTotalPoints = function () { return totalPoints; };
    var getTotalPointsScheduled = function () { return totalPointsScheduled; };
    var getTotalPointsDone = function () { return totalPointsDone; };
    var getTotalPointsDonePlus = function () { return totalPointsDonePlus; };

    var getParent = function () {
        parent = $.jStorage.get(parentStorageKey);
        return parent;
    }

    var getParentName = function () {
        return parentName;
    }


    var getLogged = function () {
        var logged = dataContext.getLogged();
        return logged;
    }

    var getUserName = function () {
        var logged = dataContext.getUserName();
        return logged;
    }

    var setParent = function (parentin) {
        parent = parentin;
        $.jStorage.set(parentStorageKey, parent);
    }

    var setStatePlan = function (){
        state = "Plan";
        $.jStorage.set(stateStorageKey, state);
    }
    var setStateDo = function () {
        state = "Do";
        $.jStorage.set(stateStorageKey, state);
    }
    var setStateReview = function () {
        state = "Review";
        $.jStorage.set(stateStorageKey, state);
    }

    var handleArchiveAction = function () {
        dataContext.archiveAndReloadNotes();
    }

    var handleDeleteAction = function () {
        dataContext.archiveAndReloadNotes();
    }

    var deleteNote = function (currentNote) {
        var result = dataContext.deleteNote(currentNote);
        return result;
    };

    var noteForId = function(id){

        for(var i=0; i<notesList.length; i++){

            if (notesList[i].id==id) return i;
        };

        return -1;

    }

    var starparent = function (id) {
        var i = this.noteForId(id);
        if (i == -1) return;
        var note = notesList[i];
        setParent(note.id);
    }

    var unstarparent = function (id) {
        var i = this.noteForId(id);
        if (i == -1) return;
        var note = notesList[i];
        setParent(note.parentID);
    }

    var unstarcurrentparent = function() {
        var i = this.noteForId(parent);
        if (i == -1) return;
        var note = notesList[i];
        setParent(note.parentID);
    }

    var star = function (id) {

        var i = this.noteForId(id);
        if (i == -1) return;
        var note = notesList[i];

        if (state == "Plan") {
            if (note.status == null || note.status == "asleep") note.status = "stared";
        }
        if (state == "Do") {
            if (note.status == "done"){
                note.status = "doneplus";
            }
            if (note.status == "stared") {
                note.status = "done";
                note.doneDate = new Date().toDateString();
            }
        }

        if (state == "Review") {
            alert("this shold not happen");
        }



        this.saveNote(note);

        return note.status;
    }

    var unstar = function (id) {

        var i = this.noteForId(id);
        if (i == -1) return;
        var note = notesList[i];

        if (state == "Plan") {
            if (note.status == "stared") note.status = "asleep";
        }
        if (state == "Do") {
            if (note.status == "done") note.status = "stared";
            if (note.status == "doneplus") note.status = "done";
        }

        if (state == "Review") {
            if (note.status == "done") note.status = "stared";
            if (note.status == "doneplus") note.status = "done";
        }

        this.saveNote(note);

        return note.status;
    }

    var getNoteForId = function(noteId) {
        for (var t=0; t<notesList.length; t++) {
            if (notesList[t].id == noteId) return notesList[t];
        }
        return null;
    }

    var getNotesListDoneDate = function ()
    {
        //if wew have done date, show done date, otherwise show today    
        var notesListDoneDate = new Date();
        for (i = 0; i < notesList.length; i += 1) {
            var mindNote = getNoteForId(i); //the idea here is the get the firt task.. usually A1-Mind
            if (mindNote != null && mindNote.doneDate != null) {
                notesListDoneDate = new Date(mindNote.doneDate);
                return notesListDoneDate;
            }
        }
        return notesListDoneDate;
    }


    var calculatePoints = function()
    {
        totalPoints = 0;
        totalPointsScheduled = 0;
        totalPointsDone = 0;
        totalPointsDonePlus = 0;

        for (var i = 0; i < notesList.length; i++) {
            totalPoints += notesList[i].actualDuration;
            if (notesList[i].status == "stared" || notesList[i].status == "done" || notesList[i].status == "doneplus") {
                totalPointsScheduled += notesList[i].actualDuration;
            }
            if (notesList[i].status == "done" || notesList[i].status == "doneplus") {
                totalPointsDone += notesList[i].actualDuration;
            }
            if (notesList[i].status == "doneplus") {
                totalPointsDonePlus += notesList[i].actualDuration;
            }
        }

    }
    var getNotesList = function () {
        var filteredNotesList = [];
        for (var i = 0; i < notesList.length; i++) {
            if (notesList[i].parentID === parent || notesList[i].jassActivityID === parent) {
            if (state === "Do" && notesList[i].status != null && notesList[i].status !== "asleep") filteredNotesList.push(notesList[i]);
            if (state === "Plan") filteredNotesList.push(notesList[i]);
            if (state === "Review" && notesList[i].status != null && notesList[i].status !== "asleep") filteredNotesList.push(notesList[i]);
            }
        }
        var returnList = [filteredNotesList];
        if (state === "Do") {
            var schedulerJassplanAdapter = new Jassplan.SchedulerJassplanAdapter();
            schedulerJassplanAdapter.makeSchedulable(notesList);
            var scheduler = new Jassplan.Scheduler(
                 {
                     activeTasks: filteredNotesList,
                     currentTime: new Date(),
                     shortTermTimeWindow: 2,
                     shortTermMaxNumberOfTasks: 3
                 });
            scheduler.CreateSchedule();
            returnList = [  scheduler.GetShortTermTasks(),
                            scheduler.GetNextTasks(),
                            scheduler.GetDoneTasks()];
        }
        return returnList;
    };

    var getReviewsList = function () {

        var filteredReviewsList = [];

        for (var r = 0; r < reviewsList.length; r++) {
            var review = reviewsList[r];
            var notesListR = reviewsList[r].activityHistories;
            var filteredNotesList = [];

            for (var i = 0; i < notesListR.length; i++) {
                if (notesListR[i].parentID == parent || notesListR[i].jassActivityID == parent) {
                   filteredNotesList.push(notesListR[i]);
                }
            }
            review.activityHistories = filteredNotesList;
            filteredReviewsList.push(review);
        }

        return filteredReviewsList;
    };

    var createBlankNote = function () {

        var blankNote = dataContext.createBlankNote();

        if (getState() === "Do") {
                blankNote.status = "stared";
            }
        if (getState() === "Plan") {
                blankNote.status = "asleep";
        }



        return blankNote;
    };

    var saveNote = function (currentNote) {

        if (currentNote.doneDate === null)  {
            if (currentNote.status === "done" && currentNote.status === "donebad") {
                currentNote.doneDate = new Date();
            }
        } else {
            if (currentNote.status === "asleep") {
                currentNote.doneDate = null;
            }
        }

        if (!(parseInt(currentNote.actualDuration) > 0)) { currentNote.actualDuration = 1; };

        var result = dataContext.saveNote(currentNote);

        return result;
    };

    var refresh = function(){   
        dataContext.refresh();
    }

    var init = function () {
        dataContext.init(appStorageKey);
        parent = getParent();
        notesList = dataContext.getNotesList();
        reviewsList = dataContext.getReviewsList();

        for (var i = 0; i < notesList.length; i++) {
            if (notesList[i].id === parent) {
                parentName = notesList[i].title;
            }
        }

        if (parentName == null) { parentName = ""; };
        if (getState() == null) { setStateDo(); };

        calculatePoints();
    };

    var viewStatus = function() {
        dataContext.viewStatus();
    }

    var public = {
        init: init,
        getTotalPoints: getTotalPoints,
        getTotalPointsScheduled:getTotalPointsScheduled,
        getTotalPointsDone: getTotalPointsDone, 
        getTotalPointsDonePlus: getTotalPointsDonePlus,
        getNotesListDoneDate: getNotesListDoneDate,
        refresh: refresh,
        getNoteForId: getNoteForId,
        getNotesList: getNotesList,
        getReviewsList: getReviewsList,
        createBlankNote: createBlankNote,
        saveNote: saveNote,
        deleteNote: deleteNote,
        getState: getState,
        getParentName: getParentName,
        getParent: getParent,
        getLogged: getLogged,
        getUserName: getUserName,
        setStatePlan: setStatePlan,
        setStateDo: setStateDo,
        setStateReview: setStateReview,
        viewStatus:viewStatus,
        star: star,
        unstar: unstar,
        starparent: starparent,
        unstarparent: unstarparent,
        unstarcurrentparent: unstarcurrentparent,
        noteForId: noteForId,
        handleArchiveAction: handleArchiveAction
   }

    return public;

})(Jassplan.dataContext);


