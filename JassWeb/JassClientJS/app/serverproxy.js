var JassplanIsOnline = true;
var Jassplan = Jassplan || {};

Jassplan.serverProxy = (function () {
 
    var checkUserLogged = function () {

        var serverLogged;

        $.ajax({
            type: "GET",
            dataType: "json",
            async:false,
            url: "/account/getuserlogged",
            success: function (data) {
                serverLogged = data;
            },
            error: function (data) {
                serverLogged = false;
            }
        });

        return serverLogged;
    }

    var getTodoLists = function () {

        var todoLists;

        $.ajax({
            type: "GET",
            dataType: "json",
            async: false,
            url: "/api/todolist/",
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

    var createTodoList = function (todoListIn) {
        var todoListOut;
        $.ajax({
            type: "POST",
            dataType: "json",
            data: todoListIn,
            async: false,
            url: "/api/todolist/",
            success: function (data) {
                todoListOut = data;
            },
            error: function (data) {
                alert("Error while creating todolist");
            }
        });
        todoListOut.id = todoListOut.jassActivityID;
        return todoListOut;
    }

    var saveTodoList = function (todoListIn) {
        var todoListOut;
        $.ajax({
            type: "PUT",
            dataType: "json",
            data: todoListIn,
            async: false,
            url: "/api/todolist/",
            success: function (data) {
                todoListOut = data;
            },
            error: function (data) {
                alert("Error while creating todolist");
            }
        });
        todoListOut.id = todoListOut.jassActivityID;
        return todoListOut;
    }

  
    var public = {
        checkUserLogged: checkUserLogged,

        getTodoLists: getTodoLists,
        createTodoList: createTodoList,
        saveTodoList: saveTodoList
    };
    return public;
})();


