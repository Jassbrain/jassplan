/**
 * Created by pablo on 12/15/13.
 */

var JassplanIsOnline = true;
var Jassplan = Jassplan || {};

Jassplan.serverProxy = (function () {
 
    var myfunction = function () {

        if (JassplanIsOnline) return 'we are online';
        else return "we are offline";
    };

  
    var public = {
        myfunction: myfunction
    };
    return public;
})();


