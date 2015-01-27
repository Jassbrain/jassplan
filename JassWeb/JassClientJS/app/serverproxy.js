var Jassplan = Jassplan || {};

Jassplan.serverProxy = (function () {
    //This object is in charge of communicating back and forth with the server side
    //
    var checkUserLogged = function () {

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

    var getReviewLists = function () {

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

    var getTodoLists = function () {

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

    var archiveTodoLists = function () {

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

    var createTodoList = function (todoListIn) {
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

    var saveTodoList = function (todoListIn) {
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
                alert("We failed to saved the task. Are you logged in?");
            }
        });

    }

    var deleteTodoList = function (todoListIn) {
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

    var deleteAllTodoLists = function () {
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


    var public = {
        checkUserLogged: checkUserLogged,
        getTodoLists: getTodoLists,
        getReviewLists: getReviewLists,
        createTodoList: createTodoList,
        saveTodoList: saveTodoList,
        deleteTodoList: deleteTodoList,
        deleteAllTodoLists: deleteAllTodoLists,
        archiveTodoLists: archiveTodoLists,
    };
 
    return public;
})();


