/**
 * Test for the JPScheduler. This module represented by the file sheduler.js is used
 to build up a schedule from a set of tasks.
 the output interface is represented by JPScheduler and the input interface \
 is represented by JPTask.
 This module is supposed to be used in future developments by adapting the model interface
 For convenience, we put here the current mapping of the JassPlan model interface

    id => this.id  
           this.jassActivityID
           this.dateCreated
    title => this.title
           this.narrative
           this.description
    order => this.estimatedDuration   I know this is weird.. I am jsut saving a refactoring
    snoozeUntilTime => this.estimatedStartHour
    startTime => ?
    endTime => ?
    points => this.actualDuration
    status => this.status
    flag => this.flag  
    parent => this.parent

 */

describe("Test for JPScheduler", function () {

    it("Can Create a scheduler and call its functions", function () {

        var anActiveTask = { title: "test title" };
        var activeTasks = [anActiveTask];
        var scheduler = new Jassplan.Scheduler({activeTasks:activeTasks});
        expect(scheduler).toBeDefined();
        scheduler.CreateSchedule();
        var result = scheduler.GetAllActiveTasks();
        expect(result).toBeDefined();
        expect(result.length).toBe(1);

        var result2 = scheduler.GetShortTermTasks();
        expect(result2).toBeDefined();
        expect(result2.length).toBe(1);

        var result3 = scheduler.GetNextTasks();
        expect(result3).toBeDefined();
        expect(result3.length).toBe(0);

        var result4 = scheduler.GetDoneTasks();
        expect(result4).toBeDefined();
        expect(result4.length).toBe(0);
    });

    it("Scheduler GetJPHours works fine", function() {

        var currentTime = new Date("2015-02-06T00:02:49.939Z");
        var scheduler = new Jassplan.Scheduler(
        {
            activeTasks: [],
            currentTime: currentTime,
            shortTermTimeWindow: 2,
            shortTermMaxNumberOfTasks: 1
        });

        var currentJPHours = scheduler.GetJPHours();
        expect(currentJPHours).toBe(19.033333333333335);

    });

    it("If number of tasks is 1, it will put second task in next", function () {

        var anActiveTask1 = { title: "title1" };
        var anActiveTask2 = { title: "title2" };

        var currentTime = new Date("2015-02-06T00:00:00.000Z");

        var activeTasks = [anActiveTask1,anActiveTask2];
        var scheduler = new Jassplan.Scheduler(
        {
            activeTasks: activeTasks,
            currentTime: currentTime,
            shortTermTimeWindow: 2,
            shortTermMaxNumberOfTasks: 1
        });

        expect(scheduler).toBeDefined();
        scheduler.CreateSchedule();

        var result = scheduler.GetAllActiveTasks();
        expect(result.length).toBe(2);

        var result2 = scheduler.GetShortTermTasks();
        expect(result2.length).toBe(1);
        expect(result2[0].title).toBe("title1");

        var result3 = scheduler.GetNextTasks();
        expect(result3.length).toBe(1);
        expect(result3[0].title).toBe("title2");

        var result4 = scheduler.GetDoneTasks();
        expect(result4.length).toBe(0); 
    });

    it("If snooze until out of window put in next, in right order, only 3", function () {

        var anActiveTask1 = { title: "title1", snoozeUntil: 18 };
        var anActiveTask2 = { title: "title2", snoozeUntil: 19 };
        var anActiveTask3 = { title: "title3", snoozeUntil: 20 };
        var anActiveTask4 = { title: "title4", snoozeUntil: 21 };
        var anActiveTask5 = { title: "title5", snoozeUntil: 22 };
        var anActiveTask6 = { title: "title6", snoozeUntil: 23 };

        var activeTasks = [anActiveTask1, anActiveTask2, anActiveTask3, anActiveTask4, anActiveTask5, anActiveTask6];
        var currentTime = new Date("2015-02-06T00:00:00.000Z");  //19

        var scheduler = new Jassplan.Scheduler(
         {
             activeTasks: activeTasks,
             currentTime: currentTime,
             shortTermTimeWindow: 2,
             shortTermMaxNumberOfTasks: 3
         });

        expect(scheduler).toBeDefined();
        scheduler.CreateSchedule();

        var result = scheduler.GetAllActiveTasks();
        expect(result.length).toBe(6);

        //since we have a window of 2 hours starting at 19, we should see tasks at 19, 20 and 21.
        //the other tasks should go to the next
        var shortTermTasks = scheduler.GetShortTermTasks();
        expect(shortTermTasks.length).toBe(3);
        expect(shortTermTasks[0].title).toBe("title1");
        expect(shortTermTasks[1].title).toBe("title2");
        expect(shortTermTasks[2].title).toBe("title3");


        var nextTasks = scheduler.GetNextTasks();
        expect(nextTasks.length).toBe(3);
        expect(nextTasks[0].title).toBe("title4");
        expect(nextTasks[1].title).toBe("title5");
        expect(nextTasks[2].title).toBe("title6");

        var result4 = scheduler.GetDoneTasks();
        expect(result4.length).toBe(0);
    });

});
