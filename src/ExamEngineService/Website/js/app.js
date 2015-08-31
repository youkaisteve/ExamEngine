/**
 * Created by Jean on 4/29/2015.
 */

"use strict";

define(["angularAMD", "app.config", "root_scope"], function (angularAMD, config, initRootScope) {

    var app = angular.module(config.appName, ["ngRoute"]);

    function configRoute(templateUrl, controllerUrl, controller) {

        if (!controllerUrl) {

            if (!config.controllerPath) {
                throw new Error("controllerPath is not defined");
            }

            var lastDot = templateUrl.lastIndexOf(".");
            controllerUrl = config.controllerPath + templateUrl.slice(0, lastDot);
        }

        if (!config.templatePath) {
            throw new Error("templatePath is not defined");
        }
        templateUrl = config.templatePath + templateUrl;

        var cfg = {
            templateUrl: templateUrl
            , controllerUrl: controllerUrl
        };


        if (!controller) {
            var last = controllerUrl.lastIndexOf("/");
            controller = controllerUrl.slice(last + 1);
        }
        cfg.controller = controller;

        return cfg;
    }

    app.config(["$routeProvider", "$httpProvider", function ($routeProvider, $httpProvider) {
        var ele;
        for (var i = 0; i < config.route.length; i++) {
            ele = config.route[i];
            $routeProvider.when(ele.url,
                angularAMD.route(configRoute(ele["templateUrl"], ele["controllerUrl"], ele["controller"])));
        }

        //$httpProvider.interceptors.push(["$q", function ($q) {
        //    return {
        //        request: function (cfg) {
        //            return cfg;
        //        }
        //        , requestError: function (rejection) {
        //            return $q.reject(rejection);
        //        }
        //        , response: function (response) {
        //            return response;
        //        }
        //        , responseError: function (rejection) {
        //            return $q.reject(rejection);
        //        }
        //    };
        //}]);
    }]);

    app.run(function ($rootScope) {
        initRootScope($rootScope);
    });

    return angularAMD.bootstrap(app);

});