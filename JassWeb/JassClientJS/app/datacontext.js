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
    var refreshStorageKey;  //keeps the review list local storage key
    var loggedStorageKey;  //keeps the review list local storage key


    var handleProxyError = function (errorCode, errorMessage) {
        if (errorCode === 401) {
            var logged = $.jStorage.get(loggedStorageKey);
            if (logged !== true) {
                alert("You do not seem to be logged in, you will continue working offline");
            }
            $.jStorage.set(loggedStorageKey,false);
        }
    }

    var saveNotesToLocalStorage = function () { $.jStorage.set(notesListStorageKey, notesList); };

    var getLogged = function () {
        return userLogged;
    }

    var getUserName = function () {
        return userName;
    }

    var loadNotesFromLocalStorage = function () {
        notesList = $.jStorage.get(notesListStorageKey);

        if (notesList == null) {
            userName = serverProxy.checkUserLogged();
            if (userName !== "") { 
                notesList = serverProxy.getTodoLists();
                notesList = $.jStorage.set(notesListStorageKey,notesList);
            };
        }
        return notesList;
    };

    var loadReviewsFromLocalStorage = function () {
        reviewsList = $.jStorage.get(reviewsListStorageKey);

        if (reviewsList == null) {
            userName = serverProxy.checkUserLogged();
            if (userName != "") {
                reviewsList = serverProxy.getReviewLists();
                reviewsList = $.jStorage.set(reviewsListStorageKey, reviewsList);
            };
        }
        return reviewsList;
    };

    var archiveAndReloadNotes = function () {
            storedNotes = serverProxy.archiveTodoLists();
            notesList = storedNotes;
    };

    var getNotesList = function () {
        if (notesList != null) return notesList;
        else return [];        
    };

    var getLastId = function () {
        var lastID = 0;
        if (notesList != null) {
            for (var i = 0; i < notesList.length; i++) {
                if (notesList[i].jassActivityID > lastID) {
                    lastID = notesList[i].jassActivityID;
                }
            }
        }
        return lastID;
    };

    var getReviewsList = function () {
        if (reviewsList != null) return reviewsList;
        else return [];
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
        notesListStorageKey = storageKey;
        reviewsListStorageKey = storageKey + "review";
        refreshStorageKey = storageKey + "refresh";
        loggedStorageKey = storageKey + "logged";

    
        var refreshToken = $.jStorage.get(refreshStorageKey);
        var loggedToken = $.jStorage.get(loggedStorageKey);

        if (loggedToken === null) {
            var _userName = serverProxy.checkUserLogged();
            if (_userName === "") {
                loggedToken = $.jStorage.set(loggedStorageKey,false);
            } else {
                loggedToken = $.jStorage.set(loggedStorageKey, true);
            }
        }

        loggedToken = $.jStorage.get(loggedStorageKey);

        var weAreInRefresh = (refreshToken !== null);
        var weAreLogged = (loggedToken === true);


        if (weAreInRefresh) {
            loadNotesFromLocalStorage();
            var result = serverProxy.saveAllTodoLists(notesList, handleProxyError);
            $.jStorage.set(refreshStorageKey, null);
            if (result) {
                $.jStorage.set(notesListStorageKey, null);
                $.jStorage.set(reviewsListStorageKey, null);
            }
        }
        loadNotesFromLocalStorage();
        loadReviewsFromLocalStorage();


    };


    var viewStatus = function() {
        var refreshToken = $.jStorage.get(refreshStorageKey);
        var loggedToken = $.jStorage.get(loggedStorageKey);

        var weAreInRefresh = (refreshToken !== null);
        var weAreLogged = (loggedToken === true);

        alert("Jassplan Status: \n  logged:" + loggedToken + "\n refresh: " + refreshToken);

    }


    var saveNote = function (noteModel) {
        //"2014-11-10T22:24:52.517"

    //    var d = Date();
    //    noteModel.lastUpdated = "2014-11-10T22:24:52.517";
        var d = new Date();
        noteModel.lastUpdated = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate() +
                                "T" + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + "." + d.getMilliseconds();


        var noteIndex = noteIndexInNotesList(noteModel);
        if (noteIndex == null) {
            noteModel.jassActivityID = getLastId();
            notesList.splice(0, 0, noteModel);
             Jassplan.serverProxy.createTodoList(noteModel);
        } else {
            notesList[noteIndex] = noteModel; // save the note on notes list
            Jassplan.serverProxy.saveTodoList(noteModel, handleProxyError);
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



    var deleteAllNotes = function(){
    
        Jassplan.serverProxy.deleteAllTodoLists();   
    }

    var refresh = function () {
        $.jStorage.set(refreshStorageKey, "refresh"); 
    }

    var public = {
        init: init,
        viewStatus: viewStatus,
        refresh: refresh,
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


