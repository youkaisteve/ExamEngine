/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
"use strice";
require.config({
    baseUrl: "js/"
    , paths: {
        "angular": "lib/angular/angular"
        , "jquery": "lib/jquery/dist/jquery"
        , "bootstrap": "lib/bootstrap/dist/js/bootstrap"
        , "angularAMD": "lib/angularAMD/angularAMD"
        , "app": "app"
        , "app.config": "app.config"
        , "angular-route": "lib/angular-route/angular-route"
        , "angular-cookie": "lib/angular-cookies/angular-cookies"
        , "angular-storage": "lib/ngstorage/ngStorage"
        , "root_scope": "root_scope"

        //directive
        , "table-form": "directive/table-form/index"
        , "team-select": "directive/team-select/index"
        , "custom-select": "directive/custom-select/index"
        , "file-uploader": "directive/file-uploader/index"
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
});