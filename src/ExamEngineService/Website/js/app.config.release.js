/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define({
    appName: "app"
    , environment: "develop"
    , appDeps: ["ngRoute", /*"ngCookies",*/"ngStorage"]
    , controllerPath: "controllers/"
    , templatePath: "views/"
    , api: "http://222.196.244.45/api/Home/Handler"
    , importStudentUri: "http://222.196.244.45/api/User/ImportUser"
    , uploadFormUri: "http://222.196.244.45/api/User/UploadForm"
    , start: "/default"
    , route: {
        "/default": {
            templateUrl: "default.html"
            , ssl: "true"
        },
        "/task/:instanceID/:tokenID": {
            templateUrl: "task.html"
            , ssl: "true"
        },
        "/workflow": {
            templateUrl: "request_workflow.html"
            , ssl: "true"
        }
        , "/forms": {
            templateUrl: "setting_forms.html"
            , ssl: "true"
        }
        , "/answer": {
            templateUrl: "setting_answer.html"
            , ssl: "true"
        }
        , "/scores": {
            templateUrl: "query_score.html"
            , ssl: "true"
        }
        , "/import": {
            templateUrl: "import_students.html"
            , ssl: "true"
        }
        , "/team": {
            templateUrl: "setting_team.html"
            , ssl: "true"
        }
        , "/login": {
            templateUrl: "login.html"
            , ssl: "true"
        }
        , "/workflow/view": {
            templateUrl: "view_workflow.html"
            , ssl: "true"
        }
        , "/test": {
            templateUrl: "test.html"
        }
        , "/maintain_process": {
            templateUrl: "maintain_process.html"
            , ssl: true
        }
    }
    , formDependence: {}
    , roleMappding: {
        "0": "partials/default_student.html"
        , "1": "partials/default_teacher.html"
    }
});