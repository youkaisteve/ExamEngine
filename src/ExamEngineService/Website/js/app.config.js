/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define({
    appName: "app"
    , appDeps: ["ngRoute", /*"ngCookies",*/"ngStorage"]
    , controllerPath: "controllers/"
    , templatePath: "views/"
    , api: "http://127.0.0.1:1567/Handler"
    , start: "/default"
    , route: [{
        url: "/default"
        , templateUrl: "default.html"
    }, {
        url: "/task/:id"
        , templateUrl: "task.html"
    }, {
        url: "/test"
        , templateUrl: "test.html"
    }, {
        url: "/login"
        , templateUrl: "login.html"
    }]
    , formDependence: {
        "task_id": {
            deps: []//angular dependence model
            , path: ""//form path
        }
        , "test_frm1": {
            deps: ["table-form"]
            , path: "forms/test-frm1.html"
        }
        , "医疗保险费单位缴费月报表": {
            path: "forms/医疗保险费单位缴费月报表.html"
        }
    }
});