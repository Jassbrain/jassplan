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
        var scheduler = new Jassplan.Scheduler(activeTasks);
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

    it("If number of tasks is 1, it will put second task in next", function () {

        var anActiveTask1 = { title: "title1" };
        var anActiveTask2 = { title: "title2" };

        var activeTasks = [anActiveTask1,anActiveTask2];
        var scheduler = new Jassplan.Scheduler(activeTasks, new Date(), 1, 1);
        expect(scheduler).toBeDefined();
        scheduler.CreateSchedule();

        var result = scheduler.GetAllActiveTasks();
        expect(result).toBeDefined();
        expect(result.length).toBe(2);

        var result2 = scheduler.GetShortTermTasks();
        expect(result2).toBeDefined();
        expect(result2.length).toBe(1);
        expect(result2[0].title).toBe("title1");

        var result3 = scheduler.GetNextTasks();
        expect(result3).toBeDefined();
        expect(result3.length).toBe(1);
        expect(result3[0].title).toBe("title2");

        var result4 = scheduler.GetDoneTasks();
        expect(result4).toBeDefined();
        expect(result4.length).toBe(0);
    });

    it("If number of tasks is 1, it will put second task in next", function () {

        var anActiveTask1 = { title: "title1", snoozeUntil:1 };
        var anActiveTask2 = { title: "title2", snoozeUntil:2 };

        var activeTasks = [anActiveTask1, anActiveTask2];
        var scheduler = new Jassplan.Scheduler(activeTasks, new Date(), 1, 1);
        expect(scheduler).toBeDefined();
        scheduler.CreateSchedule();

        var result = scheduler.GetAllActiveTasks();
        expect(result).toBeDefined();
        expect(result.length).toBe(2);

        var result2 = scheduler.GetShortTermTasks();
        expect(result2).toBeDefined();
        expect(result2.length).toBe(1);
        expect(result2[0].title).toBe("title1");

        var result3 = scheduler.GetNextTasks();
        expect(result3).toBeDefined();
        expect(result3.length).toBe(1);
        expect(result3[0].title).toBe("title2");

        var result4 = scheduler.GetDoneTasks();
        expect(result4).toBeDefined();
        expect(result4.length).toBe(0);
    });

});