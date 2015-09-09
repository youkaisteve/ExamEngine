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
    , api: "http://172.16.70.11:8083/api/Home/Handler"
    //, api: "http://localhost:1567//api/Home/Handler"
    , start: "/default"
    , route: [{
        url: "/default"
        , templateUrl: "default.html"
    }, {
        url: "/task/:id"
        , templateUrl: "task.html"
    }, {
        url: "/workflow"
        , templateUrl: "request_workflow.html"
    }, {
        url: "/answer"
        , templateUrl: "setting_answer.html"
    }, {
        url: "/scores"
        , templateUrl: "query_score.html"
    }, {
        url: "/import"
        , templateUrl: "import_students.html"
    }, {
        url: "/team"
        , templateUrl: "setting_team.html"
    }, {
        url: "/login"
        , templateUrl: "login.html"
    }]
    , formDependence: {}
});