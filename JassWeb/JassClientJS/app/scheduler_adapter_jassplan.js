var Jassplan = Jassplan || {};

Jassplan.SchedulerJassplanAdapter = function() {

    var makeSchedulable = function (notesList) {
        for (var t = 0; t < notesList.length; t++) {
            var anote = notesList[t];
            anote.order = anote.estimatedDuration;
            anote.snoozeUntil = anote.estimatedStartHour;
            anote.points = anote.actualDuration;
        }
    }

    var publicInterface = {
        makeSchedulable: makeSchedulable
    }
    return publicInterface;
}
