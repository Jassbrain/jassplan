var Jassplan = Jassplan || {};

Jassplan.dataContext = (function (serverProxy) {
    //This object acts as the data context. The controller and view model do not know anything
    //about a server. This layer provides an abstraction to the data. data is stored in the server
    //and also in local memory. The idea is great but for the moment the implementatio is pretty
    //inneficient. we are going to the server everytime is this is posible. Only go to local storage 
    //if we are offline. We need to change this.

    var userLogged = false;
    var notesList = [];
    var notesListStorageKey;
    var reviewsList = [];
    var reviewsListStorageKey;

    var getLogged = function () {
        if (userLogged) return ""
        else return "x";
    }


    var loadNotesFromLocalStorage = function () {
        //Ok, this is not efficient... but works.. if the user is logged we are just calling 
        //the server
        var storedNotes;
        if (userLogged) {
            storedNotes = serverProxy.getTodoLists();
        }
        else {
            storedNotes = $.jStorage.get(notesListStorageKey);
        }

        if (storedNotes !== null) {
            notesList = storedNotes;
        }

        return notesList;
    };

    var loadReviewsFromLocalStorage = function () {

        var storedReviews;
        if (userLogged) {
            storedReviews = serverProxy.getReviewLists();
        }
        else {
            storedReviews = $.jStorage.get(reviewsListStorageKey);
        }

        if (storedReviews !== null) {
            reviewsList = storedReviews;
        }

        return reviewsList;
    };

    var archiveAndReloadNotes = function () {
            storedNotes = serverProxy.archiveTodoLists();
            notesList = storedNotes;
    };

    var getNotesList = function () {
        return notesList;
    };

    var getReviewsList = function () {
        return reviewsList;
    };

    var createBlankNote = function () {
        var dateCreated = new Date();
        var id = dateCreated.getTime().toString();
        var config = {};
        config.id = id;
        config.title = "";
        config.description = "";
        config.narrative = "";
        config.dateCreated = dateCreated;
        var noteModel = new Jassplan.NoteModel(config);
        return noteModel; };

    var init = function (storageKey) {
        userLogged = serverProxy.checkUserLogged();
        notesListStorageKey = storageKey;
        loadNotesFromLocalStorage();
        reviewsListStorageKey = storageKey+"review";
        loadReviewsFromLocalStorage();
    };

    var saveNote = function (noteModel) {
        var noteIndex = noteIndexInNotesList(noteModel);
        if (noteIndex == null) {
            notesList.splice(0, 0, noteModel);  //add the note to note list
            Jassplan.serverProxy.createTodoList(noteModel);
        } else {
            notesList[noteIndex] = noteModel; // save the note on notes list
            Jassplan.serverProxy.saveTodoList(noteModel);
        }
        saveNotesToLocalStorage();
    };

    var noteIndexInNotesList = function (noteModel){
        var index = null;
        var i;
        for (i = 0; i < notesList.length; i += 1) {
            if (notesList[i].id === noteModel.id) {
                index = i;
                i = notesList.length;
            }
        }
        return index;
    }

    var deleteNote = function (noteModel) {
        var found = false;
        var i;
        var ifound;
        for (i = 0; i < notesList.length; i += 1) {
            if (notesList[i].id === noteModel.id) {
                notesList[i] = noteModel;
                found = true;
                ifound = i;
                i = notesList.length;
            }
        }
        if (!found) {
            alert("Note cannot be deleted because we could not find it");
        } else {
            Jassplan.serverProxy.deleteTodoList(noteModel);
            notesList.splice(ifound, 1);
        }

        saveNotesToLocalStorage();
    };

    var saveNotesToLocalStorage = function () { $.jStorage.set(notesListStorageKey, notesList); };

    var deleteAllNotes = function(){
    
        Jassplan.serverProxy.deleteAllTodoLists();   
    }

    var public = {
        init: init,
        getNotesList: getNotesList,
        getReviewsList: getReviewsList,
        createBlankNote: createBlankNote,
        saveNote: saveNote,
        deleteNote: deleteNote,
        deleteAllNotes: deleteAllNotes,
        getLogged: getLogged,
        archiveAndReloadNotes: archiveAndReloadNotes
    };

    return public;
})(Jassplan.serverProxy);


