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
                alert("Error while getting todolist");
            }
        });

        for (var x = 0; x < todoLists.length; x++) {
            todoLists[x].id = todoLists[x].jassActivityReviewID;
        }
        return todoLists;
    }

    var getTodoLists = function (errorHandler) {

        var todoLists;

        $.ajax({
            type: "GET",
            dataType: "json",
            async: false,
            url: "/api/todolist/GetTodosList",
            success: function (data) {
                todoLists = data;
            },
            error: function (data) {
                alert("Error while getting todolist");
            }
        });

        for (var x = 0; x < todoLists.length; x++)
        {
            todoLists[x].id=todoLists[x].jassActivityID;
        }
        return todoLists;
    }

    var archiveTodoLists = function errorHandler() {

        var todoLists;

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

        for (var x = 0; x < todoLists.length; x++) {
            todoLists[x].id = todoLists[x].jassActivityID;
        }
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
                alert("Error while creating todolist");
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
                errorHandler(401, "We failed to saved the task becuase your are not logged in");
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
                alert("Error while creating todolist");
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
                if (data.status != 200) {
                    alert("Error while deleting all todolists");
                }
            }
        });

        return todoListOut;
    }

    var saveAllTodoLists = function (allTodos, errorHandler) {
        var result = false;
        $.ajax({
            type: "PUT",
            dataType: "json",
            async: false,
            data: allTodos,
            url: "/api/todolist/PutSaveAllTodoLists",
            success: function (data) {
                result = true;
            },
            error: function (data) {
                if (data.status != 200) {
                    alert("Error while trying to sync. Are you connected? Are you logged in?");
                }
            }
        });

        return result;
    }


    var public = {
        checkUserLogged: checkUserLogged,
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


