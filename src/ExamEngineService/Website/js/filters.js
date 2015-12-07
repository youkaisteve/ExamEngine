/**
 * Created by Jean on 9/25/2015.
 */
define(["app"], function (app) {

    app.filter("ENCODE_URI", [function () {
        return encodeURI;
    }]);

    app.filter("DECODE_URI", [function () {
        return decodeURI;
    }]);

    app.filter("TIMES", [function () {
        return function (input, times) {
            for (var i = 0; i <= times; i++) {
                input.push(i);
            }
            return input;
        };
    }]);

    app.filter("TIMES1", [function () {
        return function (input, times) {
            for (var i = 1; i <= times; i++) {
                input.push(i);
            }
            return input;
        };
    }]);

});