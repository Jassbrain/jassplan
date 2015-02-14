/**
 * Test for googler
 //https://developers.google.com/google-apps/calendar/
 */

describe("Test for JPScheduler", function () {

    it("Call Open Authorization", function () {

        var googler = new Jassplan.Googler({});
        var result = googler.apiAuthorization();

        expect(result).not.toBeNull();
    });

    //            Jassplan.Googler.apiAuthorization();



});
