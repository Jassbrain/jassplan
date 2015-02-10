var Jassplan = Jassplan || {};

//https://developers.google.com/google-apps/calendar/

Jassplan.Googleable = function(config) {
    this.original = config;
    this.id = config.id;
}

Jassplan.Googler = function (params) {
    //receives a configuration to call google 


    var apiAuthorization = { "auth_uri": "https://accounts.google.com/o/oauth2/auth", "client_secret": "n_NWUmm1Mnkc4UiRNGjo3iE3", "token_uri": "https://accounts.google.com/o/oauth2/token", "client_email": "473177441662-8aljqe6fne1s5umrromjg3u0jc6slsh3@developer.gserviceaccount.com", "client_x509_cert_url": "https://www.googleapis.com/robot/v1/metadata/x509/473177441662-8aljqe6fne1s5umrromjg3u0jc6slsh3@developer.gserviceaccount.com", "client_id": "473177441662-8aljqe6fne1s5umrromjg3u0jc6slsh3.apps.googleusercontent.com", "auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs" };
    var pingGoogleCalendar = function() {

        return "ok";
    };

    var public = {
        pingGoogleCalendar: pingGoogleCalendar,
        apiAuthorization: apiAuthorization
    };

    return public;
};

//Jassplan.NoteModel.prototype.isValid = function () {
