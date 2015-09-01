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
    , route: [{
        url: "/"
        , templateUrl: "default.html"
    }, {
        url: "/forms"
        , templateUrl: "partial_container.html"
    }, {
        url: "/test"
        , templateUrl: "test.html"
    }]
    , formDependence: {
        "forms/test-frm1.html": ["table-form"]
    }
});