/**
 * Created by pablo on 12/15/13.
 */

describe("Test for JPScheduler", function () {

    it("Can Create a scheduler and call its functions", function () {

        var anActiveTask = { Title: "test title" };
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

        var anActiveTask1 = { Title: "title1" };
        var anActiveTask2 = { Title: "title2" };

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
        expect(result2[0].Title).toBe("title1");

        var result3 = scheduler.GetNextTasks();
        expect(result3).toBeDefined();
        expect(result3.length).toBe(1);
        expect(result3[0].Title).toBe("title2");

        var result4 = scheduler.GetDoneTasks();
        expect(result4).toBeDefined();
        expect(result4.length).toBe(0);
    });


});
