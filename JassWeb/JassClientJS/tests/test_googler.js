/**
 * Test for googler
 //https://developers.google.com/google-apps/calendar/
 */

describe("Jassplan.Googler", function() {

describe("Googler Asyn Testing Environment", function() {
        var value=0;

        beforeEach(function(done) {
            setTimeout(function() {
                value = 1;
                done();
            }, 4000);// wait some reasonable time for you async process to finish
        });

        //when you execute this 
        it("supports async testing using done", function (done) {
            expect(value).toBeGreaterThan(0);
            done();
        });

});

  describe("Googler interface", function() {

            //when you execute this 
        it("Can create objects", function () {
            var googler = new Jassplan.Googler();
            expect(googler).toBeDefined();
        });
        it("has method getAuthorization", function () {
            var googler = new Jassplan.Googler();
            expect(googler.getAuthorization).toBeDefined();
        });
        it("has method isAuthorized", function () {
            var googler = new Jassplan.Googler();
            expect(googler.isAuthorized).toBeDefined();
        });
  });

  describe("Googler behaviour", function() {
            var googler = Jassplan.Googler();

            beforeEach(function (done) {
             //  alert("going to ask authorization");
                googler.getAuthorization();
                setTimeout(function() {
                    done();
                }, 2000);// wait some reasonable time for you async process to finish
            });
      //when you execute this 

            it("can get authorization token", function (done) {
              //   alert("going to verify authorization");
                var isAuthorized = googler.isAuthorized();
                expect(isAuthorized).toBeTruthy();
                done();
            });

    });

});