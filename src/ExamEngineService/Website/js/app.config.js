/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define({
    appName: "app"
    , environment: "develop"
    , appDeps: ["ngRoute", /*"ngCookies",*/"ngStorage","ngMessages"]
    , controllerPath: "controllers/"
    , templatePath: "views/"
    , api: "http://222.196.244.45:9210/api/Home/Handler"
    , importStudentUri: "http://222.196.244.45:9210/api/User/ImportUser"
    , uploadFormUri: "http://222.196.244.45:9210/api/User/UploadForm"
    , importQuestion: "http://222.196.244.45:9210/api/TiKu/ImportTiKu"
    , exportQuestion: "http://222.196.244.45:9210/api/Tiku/ExportProcessInfo"
    //, api: "http://127.0.0.1:8083/api/Home/Handler"
    //, importStudentUri: "http://127.0.0.1:8083/api/User/ImportUser"
    //, uploadFormUri: "http://127.0.0.1:8083/api/User/UploadForm"
    //, importQuestion: "http://127.0.0.1:8083/api/TiKu/ImportTiKu"
    //, exportQuestion: "http://127.0.0.1:8083/api/Tiku/ExportProcessInfo"
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
            , ssl: "false"
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
        , "/question": {
            templateUrl: "question.html"
            , ssl: true
        }
    }
    , formDependence: {}
    , roleMappding: {
        "0": "partials/default_student.html"
        , "1": "partials/default_teacher.html"
    }
});