/**
 * Test for the JPScheduler Adapter.. This is a very simple piece of code use to 
 interface between the object we have in Jassplan with some funny names.. and the objets
 that the scheduler is expecting.

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

    //SAMPLE

      \"jassActivityID\":2,
      \"id\":2,
      \"parentID\":null,
      \"userName\":\"pablo\",
      \"name\":null,
      \"description\":\"calm\",
      \"title\":\"A1 Mind\",
      \"narrative\":\"Plan Tasks,
      \"status\":\"done\",
      \"flag\":\"yellow\",
      \"dateCreated\":      \"2014-11-10T22:24:52.517      \",
      \"estimatedDuration\":1,
      \"estimatedStartHour\":null,
      \"actualDuration\":1,
      \"todoToday\":false,
      \"doneToday\":false,
      \"lastUpdated\":      \"2015-2-4T13:50:10.7      \",
      \"created\":      \"2014-11-10T22:24:52.517      \",
      \"doneDate\":\"Wed Feb 04 2015\"
   }

 */

describe("Test for JPScheduler Jassplan Adapter", function () {

    it("Can make an original notes list schedulable and acees properties", function () {

        var jsonNotesList = "[{\"jassActivityID\":2,\"id\":2,\"parentID\":null,\"userName\":\"pablo\",\"name\":null,\"description\":\"calm\",\"title\":\"A1 Mind\",\"narrative\":\"Plan Tasks, Calendar, Email, Review.\\nFinish all categories/tasks seriously. \\nMake sure google docs and calendars and bookmarks  are in sync.\",\"status\":\"done\",\"flag\":\"yellow\",\"dateCreated\":\"2014-11-10T22:24:52.517\",\"estimatedDuration\":1,\"estimatedStartHour\":null,\"actualDuration\":1,\"todoToday\":false,\"doneToday\":false,\"lastUpdated\":\"2015-2-4T13:50:10.7\",\"created\":\"2014-11-10T22:24:52.517\",\"doneDate\":\"Wed Feb 04 2015\"},{\"jassActivityID\":3,\"id\":3,\"parentID\":null,\"userName\":\"pablo\",\"name\":null,\"description\":\"Run and walk\",\"title\":\"A2 Body\",\"narrative\":\"Gym, Diet, Dr, Look\",\"status\":\"stared\",\"flag\":\"yellow\",\"dateCreated\":\"2014-11-10T22:26:13.57\",\"estimatedDuration\":2,\"estimatedStartHour\":null,\"actualDuration\":1,\"todoToday\":false,\"doneToday\":false,\"lastUpdated\":\"2015-2-4T13:49:59.31\",\"created\":\"2014-11-10T22:26:13.57\",\"doneDate\":null},{\"jassActivityID\":4,\"id\":4,\"parentID\":null,\"userName\":\"pablo\",\"name\":null,\"description\":\"Rodrigo phone, Lelia talk\",\"title\":\"A3 Flia\",\"narrative\":\"Lelia, Rodri, Santi, Bahia\",\"status\":\"stared\",\"flag\":\"red\",\"dateCreated\":\"2014-11-10T22:29:35.44\",\"estimatedDuration\":2,\"estimatedStartHour\":null,\"actualDuration\":1,\"todoToday\":false,\"doneToday\":false,\"lastUpdated\":\"2015-2-4T13:49:54.200\",\"created\":\"2014-11-10T22:29:35.44\",\"doneDate\":null},{\"jassActivityID\":10,\"id\":10,\"parentID\":null,\"userName\":\"pablo\",\"name\":null,\"description\":\"web api course + organize folders\\nddd\",\"title\":\"C2 BePro\",\"narrative\":\"web api, ef, nunit, autofac, angular, CSS3,  HTML5,  ReSharper,  visual para dime,  azure, visual studio. Unit testing dependency injection.\",\"status\":\"stared\",\"flag\":\"yellow\",\"dateCreated\":\"2014-11-10T22:53:11.807\",\"estimatedDuration\":1,\"estimatedStartHour\":null,\"actualDuration\":4,\"todoToday\":false,\"doneToday\":false,\"lastUpdated\":\"2015-2-4T13:49:25.335\",\"created\":\"2014-11-10T22:53:11.807\",\"doneDate\":null},{\"jassActivityID\":11,\"id\":11,\"parentID\":null,\"userName\":\"pablo\",\"name\":null,\"description\":\"prepare meetings greg / larry\",\"title\":\"C3 ProNetCareer\",\"narrative\":\"Review super summary.\\nlatest communications.\\n- more specific technology \\n- people leadership / management\\n- software architecture\\n- agile project management\",\"status\":\"stared\",\"flag\":\"green\",\"dateCreated\":\"2014-11-10T22:59:20.927\",\"estimatedDuration\":1,\"estimatedStartHour\":null,\"actualDuration\":1,\"todoToday\":false,\"doneToday\":false,\"lastUpdated\":\"2015-2-4T13:49:30.151\",\"created\":\"2014-11-10T22:59:20.927\",\"doneDate\":null},{\"jassActivityID\":95,\"id\":95,\"parentID\":null,\"userName\":\"pablo\",\"name\":null,\"description\":\"Complete integration test framework to perfection creating good integration teats. \",\"title\":\"C4 Jassbrain\",\"narrative\":\"Jassplan: Complete Modeling and Jasming use case based  testing\",\"status\":\"stared\",\"flag\":\"green\",\"dateCreated\":\"2014-11-16T17:40:42.8\",\"estimatedDuration\":1,\"estimatedStartHour\":null,\"actualDuration\":6,\"todoToday\":false,\"doneToday\":false,\"lastUpdated\":\"2015-2-4T13:49:32.2\",\"created\":\"2014-11-16T17:40:42.8\",\"doneDate\":null}]";
        var notesList = JSON.parse(jsonNotesList);

        //first let's make sure all interesting properties have some values
      
        notesList[0].id = 123;
        notesList[0].title = "title";
        notesList[0].estimatedDuration = 10;
        notesList[0].estimatedStartHour = 1;
        notesList[0].actualDuration = 2;
        notesList[0].status = "status";
        notesList[0].flag = "flag";

        var schedulerJassplanAdapter = new Jassplan.SchedulerJassplanAdapter();
        schedulerJassplanAdapter.makeSchedulable(notesList);

        expect(notesList[0].id).toBe(123);
        expect(notesList[0].title).toBe("title");
        expect(notesList[0].order).toBe(10);
        expect(notesList[0].snoozeUntil).toBe(1);
        expect(notesList[0].points).toBe(2);
        expect(notesList[0].status).toBe("status");
        expect(notesList[0].flag).toBe("flag");
    });


});
