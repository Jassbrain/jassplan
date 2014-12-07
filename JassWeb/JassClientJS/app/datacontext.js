var Jassplan = Jassplan || {};

Jassplan.dataContext = (function (serverProxy) {
    //This object acts as data layer and handles local storage or communimcation with server via the server proxy. 
    //The idea is great but for the moment the implementatio is pretty
    //inneficient as we are going to the server everytime and in sync mode...
    var userName = "";     //knows if the user did log in
    var userLogged = false;     //knows if the user did log in
    var notesList = [];         //keeps the list of notes
    var notesListStorageKey;    //keeps the local storage key for the note list
    var reviewsList = [];       //keeps the review list
    var reviewsListStorageKey;  //keeps the review list local storage key

    var getLogged = function () {
        return userLogged;
    }

    var getUserName = function () {
        return userName;
    }

    var loadNotesFromLocalStorage = function () {
        //Ok, this is not efficient..if the user is logged we are just calling the server
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
        //oportunity for reuse... we have to siilar algorithms
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
        userName = serverProxy.checkUserLogged();
        if (userName != "") { userLogged = true; };
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
        getUserName: getUserName,
        archiveAndReloadNotes: archiveAndReloadNotes
    };

    return public;
})(Jassplan.serverProxy);


