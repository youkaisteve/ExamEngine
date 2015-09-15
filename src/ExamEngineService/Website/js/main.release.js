/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
"use strict";
require.config({
    baseUrl: "js/"
    , paths: {
        "angular": "lib/angular/angular.min"
        , "jquery": "lib/jquery/dist/jquery.min"
        , "bootstrap": "lib/bootstrap/dist/js/bootstrap.min"
        , "angularAMD": "lib/angularAMD/angularAMD.min"
        , "app": "app"
        , "app.config": "app.config"
        , "angular-route": "lib/angular-route/angular-route.min"
        , "angular-cookie": "lib/angular-cookies/angular-cookies.min"
        , "angular-storage": "lib/ngstorage/ngStorage.min"
        , "root_scope": "root_scope"

        //directive
        , "table-form": "directive/table-form/index"
        , "team-select": "directive/team-select/index"
        , "custom-select": "directive/custom-select/index"
        , "file-uploader": "directive/file-uploader/index"
        , "disabled-when-click": "directive/disabled_when_click"
    }
    , shim: {
        "angular": ["jquery"]
        , "angular-route": ["angular"]
        , "angular-cookie": ["angular"]
        , "angular-storage": ["angular"]
        , "angularAMD": ["angular-route"]
        , "bootstrap": ["jquery"]
        , "app": ["angular-storage", /*"angular-cookie",*/"bootstrap"]
    }
    , deps: ["app"]
    , waitSeconds: 60
});