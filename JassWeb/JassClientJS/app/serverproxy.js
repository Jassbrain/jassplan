var Jassplan = Jassplan || {};

Jassplan.serverProxy = (function () {
    //This object is in charge of communicating back and forth with the server side
    //
    var checkUserLogged = function (errorHandler) {

        var userLogged = "";

        $.ajax({
            type: "GET",
            dataType: "json",
            async:false,
            url: "/api/todolist/getuserlogged",
            success: function (data) {
                userLogged = data;
            },
            error: function (data) {
                userLogged = "" ;
            }
        });

        return userLogged;
    }

    var pingUserLogged = function (errorHandler) {

        var userLogged = "";

        $.ajax({
            type: "GET",
            dataType: "json",
            async: true,
            url: "/api/todolist/getuserlogged",
            success: function (data) {
                userLogged = data;
                errorHandler(data, "all good");
            },
            error: function (data) {
                userLogged = "";
                errorHandler("", "not logged");
            }
        });

        return userLogged;
    }

    var getReviewLists = function (errorHandler) {

        var todoLists;

        $.ajax({
            type: "GET",
            dataType: "json",
            async: false,
            url: "/api/todolist/GetReviewsList",
            success: function (data) {
                todoLists = data;
            },
            error: function (data) {
                errorHandler(data.status, data.responseText);
            }
        });

        for (var x = 0; x < todoLists.length; x++) {
            todoLists[x].id = todoLists[x].jassActivityReviewID;
        }
        return todoLists;
    }

    var getTodoLists = function (errorHandler) {

        var todoLists = [];

        $.ajax({
            type: "GET",
            dataType: "json",
            async: false,
            url: "/api/todolist/GetTodosList",
            success: function (data) {
                todoLists = data;
            },
            error: function (data) {
                errorHandler(data.status, data.responseText);
            }
        });

        return todoLists;
    }

    var archiveTodoLists = function errorHandler() {

        var todoLists = [];

        $.ajax({
            type: "GET",
            dataType: "json",
            async: false,
            url: "/api/todolist/GetArchiveTodoList",
            success: function (data) {
                todoLists = data;
            },
            error: function (data) {
                alert("Error while getting todolist");
            }
        });

        return todoLists;
    }

    var createTodoList = function (todoListIn, errorHandler) {
        var todoListOut;
        $.ajax({
            type: "POST",
            dataType: "json",
            data: todoListIn,
            async: true,
            url: "/api/todolist/PostTodoList",
            success: function (data) {
                todoListOut = data;
            },
            error: function (data) {
                errorHandler(data.status, data.responseText);
            }
        });
        return;
    }

    var saveTodoList = function (todoListIn, errorHandler) {
        var todoListOut;
        $.ajax({
            type: "PUT",
            dataType: "json",
            data: todoListIn,
            async: true,
            url: "/api/todolist/PutTodoList",
            success: function (data) {
                todoListOut = data;
            },
            error: function (data) {
                errorHandler(data.status, data.responseText);
            }
        });

    }

    var deleteTodoList = function (todoListIn, errorHandler) {
        var todoListOut;
        $.ajax({
            type: "PUT",
            dataType: "json",
            data: todoListIn,
            async: true,
            url: "/api/todolist/PutDeleteTodoList",
            success: function (data) {
                todoListOut = data;
            },
            error: function (data) {
                errorHandler(data.status, data.responseText);
            }
        });
        return todoListOut;
    }

    var deleteAllTodoLists = function (errorHandler) {
        var todoListOut;
        $.ajax({
            type: "PUT",
            dataType: "json",
            async: false,
            data: {},
            url: "/api/todolist/PutDeleteAllTodoLists",
            success: function (data) {
                todoListOut = data;
            },
            error: function (data) {
                errorHandler(data.status, data.responseText);
            }
        });

        return todoListOut;
    }

    var saveAllTodoLists = function (allTodos, errorHandler) {
        var result = [];
        $.ajax({
            type: "PUT",
            dataType: "json",
            async: false,
            data: { "ActivityListJson": allTodos },
            url: "/api/todolist/PutSaveAllTodoLists",
            success: function (data) {
                result = data;
            },
            error: function (data) {
                errorHandler(data.status, data.responseText);
            }
        });

        return result;
    }


    var public = {
        checkUserLogged: checkUserLogged,
        pingUserLogged: pingUserLogged,
        getTodoLists: getTodoLists,
        getReviewLists: getReviewLists,
        createTodoList: createTodoList,
        saveTodoList: saveTodoList,
        deleteTodoList: deleteTodoList,
        deleteAllTodoLists: deleteAllTodoLists,
        saveAllTodoLists: saveAllTodoLists,
        archiveTodoLists: archiveTodoLists
    };
 
    return public;
})();


