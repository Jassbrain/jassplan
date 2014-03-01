var Jassplan = Jassplan || {};

Jassplan.helper = (function () {

    var queryStringToObject = function (queryString){ var queryStringObj = {};
      var e;
      var a = /\+/g; // Replace + symbol with a space
      var r = /([^&;=]+)=?([^&;]*)/g;
      var d = function (s) { return decodeURIComponent(s.replace(a, " ")); };
      e = r.exec(queryString);

        while (e) {
            queryStringObj[d(e[1])] = d(e[2]);
            e = r.exec(queryString);
        }
       return queryStringObj;
    };
    return {
        queryStringToObject: queryStringToObject
    };

})();

