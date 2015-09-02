/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define({
    appName: "app"
    , controllerPath: "controllers/"
    , templatePath: "views/"
    , api: "http://127.0.0.1/api/exam"
    , route: [{
        url: "/"
        , templateUrl: "default.html"
    }, {
        url: "/forms"
        , templateUrl: "partial_container.html"
    }, {
        url: "/test"
        , templateUrl: "test.html"
    }, {
        url: "/login"
        , templateUrl: "login.html"
    }]
    , formDependence: {
        "forms/test-frm1.html": ["table-form"]
    }
});