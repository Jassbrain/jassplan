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

Jassplan.Scheduler = function (params) {
    //receives a list of tasks to be scheduled
    var _activeTasks = params.activeTasks || []; //List of Schedubles 
    var _shortTermTimeWindow = params.shortTermTimeWindow || 4; //Time we see in the short term window in "hours" 1.5 = 1hr 30 mins
    var _currentTime = params.currentTime || new Date(); //Current real time as a DateTime object
    var _currentTimeWindowStart;
    var _currentTimeWindowEnd;
    var _shortTermMaxNumberOfTasks = params.shortTermMaxNumberOfTasks || 5;
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

    var GetNumber = function (param)
    {
        if (isNaN(param)) return 0;
        return parseInt(param);
    }

    var tryToAddToShortTermList = function (task) {

        if (GetNumber(task.snoozeUntil) > _currentTimeWindowEnd) return false;
        if (task.status === "doneplus") return false;

        _shortTermTasks.push(task);
        return true;
    }

    var tryToAddToNextList = function (task) {

        if (task.status === "doneplus") return false;

        _nextTasks.push(task);
        return true;
    }

    var tryToAddToDoneList = function (task) {
        _doneTasks.push(task);
        return true;
    }

    var GetJPHours = function () {
        return _currentTime.getHours() + _currentTime.getMinutes() / 60;
    }

    var CreateSchedule = function () {
        //setup some constraints
        _currentTimeWindowStart = GetJPHours(_currentTime);
        _currentTimeWindowEnd = _currentTimeWindowStart + _shortTermTimeWindow;

        //we look at all tasks and we try to add the lists in order
        for (t in _activeTasks) {
            if (tryToAddToShortTermList(_activeTasks[t])) continue;
            if (tryToAddToNextList(_activeTasks[t])) continue;
            tryToAddToDoneList(_activeTasks[t]);
        }
        //if we got too many tasks in the short term list we move some to the next list
        if (_shortTermTasks.length > _shortTermMaxNumberOfTasks) {
             var shortTermTaskToBeSentToNext = _shortTermTasks.slice(_shortTermMaxNumberOfTasks, _shortTermTasks.length);
            _shortTermTasks= _shortTermTasks.slice(0, _shortTermMaxNumberOfTasks);
            _nextTasks = shortTermTaskToBeSentToNext.concat(_nextTasks);
        }
    }

    var public = {
        CreateSchedule: CreateSchedule,
        GetAllActiveTasks: GetAllActiveTasks,
        GetShortTermTasks: GetShortTermTasks,
        GetNextTasks: GetNextTasks,
        GetDoneTasks: GetDoneTasks,
        GetJPHours : GetJPHours 

    }

    return public;
};

//Jassplan.NoteModel.prototype.isValid = function () {
