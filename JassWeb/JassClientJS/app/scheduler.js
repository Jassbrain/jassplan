var Jassplan = Jassplan || {};

Jassplan.Schedulable = function(config) {
    this.original = config;

    this.id = config.id;
    this.title = config.title;
    this.order = config.estimatedDuration;
    this.snoozeUntil = config.estimatedStartHour;
    this.points = config.actualDuration;
    this.status = config.status;
    this.flag = config.flag;
}

Jassplan.Scheduler = function (activeTasks, currentTime, maxTimeWindow, maxNumberOfShortTermTasks) {
    //receives a list of tasks to schedule
    var _activeTasks = activeTasks;
    var _maxTimeWindow = maxTimeWindow;
    var _currentTime = currentTime;
    var _maxNumberOfShortTermTasks = maxNumberOfShortTermTasks;
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
        if (_shortTermTasks.length >= _maxNumberOfShortTermTasks) return false;
        _shortTermTasks.splice(0, 0, activeTask);
        return true;
    }

    var tryToAddToNextList = function (activeTask) {
        _nextTasks.splice(0, 0, activeTask);
        return false;
    }

    var tryToAddToDoneList = function (activeTask) {
        return false;
    }

    var CreateSchedule = function (currentTime) {
        //A schedule will be created for a given current time, a max time window and a maximun number of task
        //for example, current time 3pm, max time window 3 hrs (until 6pm), 5 tasks maximun
        _currentTime = currentTime;
        for(var t=0; t<activeTasks.length; t++) {
            var activeTask = activeTasks[t];
            if (tryToAddToShortTermList(activeTask)) continue;
            if (tryToAddToNextList(activeTask)) continue;
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
