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
            $.jStorage.set(loggedStorageKey, false);
            $("#status-button-label").text("!");
            alert("You do not seem to be logged in, you will continue working offline");
        } else {
            alert("Error: " + errorCode + " : " + errorMessage);
        }
    }

    var handleProxyLogged = function (userName, errorMessage) {
        if (userName === "") {
            $.jStorage.set(loggedStorageKey, false);
            $("#status-button-label").text("!");
        } else {
            $.jStorage.set(loggedStorageKey, true);
            $("#status-button-label").text("k");
        }
    }

    var saveNotesToLocalStorage = function () { $.jStorage.set(notesListStorageKey, notesList); };

    var getLogged = function () {
        var loggedToken = $.jStorage.get(loggedStorageKey);
        return loggedToken;
    }

    var pingLogged = function () {
        serverProxy.pingUserLogged(handleProxyLogged);
    }

    var getUserName = function () {
        return userName;
    }

    var loadNotesFromLocalStorage = function () {
        notesList = $.jStorage.get(notesListStorageKey);

        if (notesList == null) {
            userName = serverProxy.checkUserLogged();
            if (userName !== "") { 
                notesList = serverProxy.getTodoLists(handleProxyError);
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
                reviewsList = serverProxy.getReviewLists(handleProxyError);
                reviewsList = $.jStorage.set(reviewsListStorageKey, reviewsList);
            };
        }
        return reviewsList;
    };

    var archiveAndReloadNotes = function () {
        storedNotes = serverProxy.archiveTodoLists(handleProxyError);
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
        //get all the storage keys

        pingLogged();

        notesListStorageKey = storageKey;
        reviewsListStorageKey = storageKey + "review";
        refreshStorageKey = storageKey + "refresh";
        loggedStorageKey = storageKey + "logged";

        //get the refresh and logged token from local storage
        var refreshToken = $.jStorage.get(refreshStorageKey);
        var weAreInRefresh = (refreshToken !== null);

        notesList = $.jStorage.get(notesListStorageKey);
        reviewsList = $.jStorage.get(reviewsListStorageKey);


        if (weAreInRefresh) {
            notesList = serverProxy.saveAllTodoLists(notesList, handleProxyError);
            $.jStorage.set(notesListStorageKey, notesList);
            loadReviewsFromLocalStorage();
            $.jStorage.set(refreshStorageKey, null);
        }

    };


    var viewStatus = function() {
        var refreshToken = $.jStorage.get(refreshStorageKey);
        var loggedToken = $.jStorage.get(loggedStorageKey);

        var weAreInRefresh = (refreshToken !== null);
        var weAreLogged = (loggedToken === true);

        alert("Jassplan Status: \n  logged: " + loggedToken + "\nrefresh: " + refreshToken + "\n this is also a logoff");

        window.localStorage.clear();
        $.jStorage.set(loggedStorageKey, null);
        $("#status-button-label").text("!");

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
            noteModel.id = noteModel.jassActivityID;
            noteModel.Created = noteModel.lastUpdated;
            noteModel.dateCreated = noteModel.lastUpdated;
            noteModel.estimatedStartHour = null;
            notesList.splice(0, 0, noteModel);
            Jassplan.serverProxy.createTodoList(noteModel, handleProxyError);
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
            Jassplan.serverProxy.deleteTodoList(noteModel, handleProxyError);
            notesList.splice(ifound, 1);
        }

        saveNotesToLocalStorage();
    };
    var deleteAllNotes = function(){
    
        Jassplan.serverProxy.deleteAllTodoLists(handleProxyError);
    }
    var refresh = function () {
        $.jStorage.set(loggedStorageKey, null);
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
        Logged: pingLogged,
        getUserName: getUserName,
        archiveAndReloadNotes: archiveAndReloadNotes
    };

    return public;
})(Jassplan.serverProxy);


