var Jassplan = Jassplan || {};

//https://developers.google.com/google-apps/calendar/

Jassplan.Googleable = function(config) {
    this.original = config;
    this.id = config.id;
}

Jassplan.Googler = function (params) {
    //receives a configuration to call google 

    var _isAuthorized = false;
    var _authResult = null;

    //Jassplan API Key for Browser Applications
    var jassApiKey = "AIzaSyDF_Ra3V3yIzYrvlnVCS9OwKDUt1fK-GQI";

    //Jassplan Client ID
    var jassClientIO = "473177441662-mrh2rekb57m3414005na2m97qc480539.apps.googleusercontent.com";

    //Client secret
    var jassSecret = "0WiVaptWICy8wjusv7sww96Z";

    var scopes = 'https://www.googleapis.com/auth/calendar';

    var handleAuthResult = function (authResult) {
        var json = JSON.stringify(authResult);
        _authResult = authResult;
      //  alert(json);
    }

    var isAuthorized = function () {
        if (_authResult === null) return false;
        if (_authResult.status.signed_in === true) return true;
        return false;
    }

    var checkAuth = function () {
     //   alert("calling gapi auth authorize");
        gapi.auth.authorize({
            client_id: jassClientIO,
            scope: scopes,
            immediate: true
        },
            handleAuthResult);
    }

    var getAuthorization = function() {
        gapi.client.setApiKey(jassApiKey);
        window.setTimeout(checkAuth, 1000);
    };



    var pingGoogleCalendarCallBack = function (json, raw) {
       // alert("Hi");
    };

    var pingGoogleCalendar = function() {
        var calendarEntries;

        var request = gapi.client.request({
            "path": "/calendar/v3/users/me/calendarList",
            "method": "POST",
            "body": JSON.stringify({
                "id":"pablo.elustondo@gmail.com",
                "selected":true
            })
        });
        request.execute(pingGoogleCalendarCallBack);

        return calendarEntries;
    };

    var public = {
        pingGoogleCalendar: pingGoogleCalendar,
        getAuthorization: getAuthorization,
        isAuthorized: isAuthorized
    };

    return public;
};

//Jassplan.NoteModel.prototype.isValid = function () {
