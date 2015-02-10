/**
 * Test for googler
 //https://developers.google.com/google-apps/calendar/
 */

describe("Test for JPScheduler", function () {

    it("Can create a googler and called the ping function", function () {

        var googler = new Jassplan.Googler({});
        var result = googler.pingGoogleCalendar();
    });

    it("a google answers with the authorization info", function () {

        var googler = new Jassplan.Googler({});
        var result = googler.apiAuthorization;

    });


});
