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

});