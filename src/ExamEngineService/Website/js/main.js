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
        , "angularAMD": "lib/angularAMD/angularAMD"
        , "app": "app"
        , "app.config": "app.config"
        , "angular-route": "lib/angular-route/angular-route"
        , "root_scope": "root_scope"
    }
    , shim: {
        "angular": ["jquery"]
        , "angular-route": ["angular"]
        , "angularAMD": ["angular-route"]
    }
    , deps: ["app"]
});