/*
This test is mostly about new features on viewmodell.
I need now to build and test a function to calculate the points of child task.
 */

describe("Test calculate point", function () {

    var viewModel;

    beforeEach(function(){
        //We create a fake datacontext taht will get injected into the view model.
        //this data context has a setNoteList that can be used to setup values for each test

        Jassplan.dataContext = (function() {
            _notesList = [];
            var init = function() { }
            var   getNotesList = function(){ return _notesList;}
            var   setNotesList = function(noteList){ _notesList = noteList;}
            return {
                init: init,
                getNotesList: getNotesList,
                setNotesList: setNotesList,
                getReviewsList: function () { return [];}
            }
        })();

        //Jassplan.view;
        Jassplan.viewmodel = new Jassplan.ViewmodelConstructor(Jassplan.dataContext);

    
    });

    it("Can calculates points for empty task list", function () {

        //Arrange
        Jassplan.dataContext.setNotesList([]);

        //Act

        //Initialize the view model and calculate points
        Jassplan.viewmodel.init();

        //Assert

        expect(Jassplan.viewmodel.getTotalPoints()).toBe(0);
        expect(Jassplan.viewmodel.getTotalPointsScheduled()).toBe(0);
        expect(Jassplan.viewmodel.getTotalPointsDone()).toBe(0); 
        expect(Jassplan.viewmodel.getTotalPointsDonePlus()).toBe(0);


    });

    it("Can calculates points for list of two stared root level tasks", function () {

        //Arrange
        /*
         *  var taskPoints = parseInt(notesList[i].actualDuration);
            var taskStatus = notesList[i].status;
            var taskParentId = notesList[i].parentID;
         * 
         */

        var task1 = { title: "task1", status: "stared", actualDuration: 1, parentID: null };
        var task2 = { title: "task2", status: "stared", actualDuration: 1, parentID: null };

        Jassplan.dataContext.setNotesList([task1,task2]);

        //Act

        //Initialize the view model and calculate points
        Jassplan.viewmodel.init();

        //Assert

        expect(Jassplan.viewmodel.getTotalPoints()).toBe(2);
        expect(Jassplan.viewmodel.getTotalPointsScheduled()).toBe(2);
        expect(Jassplan.viewmodel.getTotalPointsDone()).toBe(0);
        expect(Jassplan.viewmodel.getTotalPointsDonePlus()).toBe(0);


    });


    it("Can calculates points for list of two stared root level tasks", function () {

        //Arrange
        /*
         *  var taskPoints = parseInt(notesList[i].actualDuration);
            var taskStatus = notesList[i].status;
            var taskParentId = notesList[i].parentID;
         * 
         */

        var task1 = { title: "task1", status: "stared", actualDuration: 1, parentID: null };
        var task2 = { title: "task2", status: "stared", actualDuration: 1, parentID: null };

        Jassplan.dataContext.setNotesList([task1, task2]);

        //Act

        //Initialize the view model and calculate points
        Jassplan.viewmodel.init();

        //Assert

        expect(Jassplan.viewmodel.getTotalPoints()).toBe(2);
        expect(Jassplan.viewmodel.getTotalPointsScheduled()).toBe(2);
        expect(Jassplan.viewmodel.getTotalPointsDone()).toBe(0);
        expect(Jassplan.viewmodel.getTotalPointsDonePlus()).toBe(0);


    });

    it("Can calculates points for two stared root level tasks and 1 child task (In state plan)", function () {

        //Arrange
        /*
         *  var taskPoints = parseInt(notesList[i].actualDuration);
            var taskStatus = notesList[i].status;
            var taskParentId = notesList[i].parentID;
         * 
         */

        var task1 = { id:1, title: "task1", status: "stared", actualDuration: 1, parentID: null };
        var task2 = { id:2, title: "task2", status: "stared", actualDuration: 1, parentID: null };
        var task3 = { id:3, title: "task3", status: "stared", actualDuration: 1, parentID: 2 };
        var task4 = { id:4, title: "task4", status: "done", actualDuration: 1, parentID: 2 };

        Jassplan.dataContext.setNotesList([task1, task2, task3, task4]);

        //Act

        //Initialize the view model and calculate points
        Jassplan.viewmodel.setStatePlan();
        Jassplan.viewmodel.init();

        var notesList = Jassplan.viewmodel.getNotesList();

        //Assert

        expect(Jassplan.viewmodel.getTotalPoints()).toBe(2);
        expect(Jassplan.viewmodel.getTotalPointsScheduled()).toBe(2);
        expect(Jassplan.viewmodel.getTotalPointsDone()).toBe(0);
        expect(Jassplan.viewmodel.getTotalPointsDonePlus()).toBe(0);

        expect(task1.totalPoints).toBe(0);
        expect(task1.totalPointsScheduled).toBe(0);
        expect(task1.totalPointsDone).toBe(0);
        expect(task1.totalPointsDonePlus).toBe(0);

        expect(task2.totalPoints).toBe(2);
        expect(task2.totalPointsScheduled).toBe(2);
        expect(task2.totalPointsDone).toBe(1);
        expect(task2.totalPointsDonePlus).toBe(0);

        expect(task3.totalPoints).toBe(0);
        expect(task3.totalPointsScheduled).toBe(0);
        expect(task3.totalPointsDone).toBe(0);
        expect(task3.totalPointsDonePlus).toBe(0);

    });


});
