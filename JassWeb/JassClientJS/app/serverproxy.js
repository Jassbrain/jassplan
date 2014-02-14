/**
 * Created by pablo on 12/15/13.
 */

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
                alert("sucess got todolist");
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
  
    var public = {
        checkUserLogged: checkUserLogged,
        getTodoLists: getTodoLists
    };
    return public;
})();


