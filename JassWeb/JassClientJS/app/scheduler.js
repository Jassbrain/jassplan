var Jassplan = Jassplan || {};

Jassplan.Scheduler = function (activeTasks) {
    //receives a list of tasks to schedule
    var _activeTasks = activeTasks;
    var _shortTermTasks = [];
    var _nextTasks = [];
    var _doneTasks = [];

    var GetShortTermTasks = function() {
        return _shortTermTasks;
    }

    var GetNextTasks = function () {
        return _nextTasks;
    }

    var GetDoneTasks = function () {
        return _doneTasks;
    }

    var GetAllActiveTasks = function () {
        return _activeTasks;
    }

    var tryToAddToShortTermList = function (activeTask) {
        _shortTermTasks.splice(0,0,activeTask);
        return true;
    }

    var tryToAddToNextList = function (activeTask) {
        return false;
    }

    var tryToAddToDoneList = function (activeTask) {
        return false;
    }

    var CreateSchedule = function() {        
        for(var t=0; t<activeTasks.length; t++) {
            var activeTask = activeTasks[t];
            if (tryToAddToShortTermList(activeTask)) break;
            if (tryToAddToNextList(activeTask)) break;
            tryToAddToDoneList(activeTask);
        }
    }



    var public = {
        CreateSchedule: CreateSchedule,
        GetAllActiveTasks: GetAllActiveTasks,
        GetShortTermTasks: GetShortTermTasks,
        GetNextTasks: GetNextTasks,
        GetDoneTasks: GetDoneTasks
    }

    return public;
};

//Jassplan.NoteModel.prototype.isValid = function () {
