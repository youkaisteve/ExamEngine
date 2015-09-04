/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
"use strict";

define(["app.config"], function (config) {

    var extendRootScope = ["$rootScope", "$http", "$cookies",
        function ($rootScope, $http, $cookies) {
            $rootScope.$on("$routeChangeStart", function (next, current) {
                //TODO check has authentication


            });

            //API method
            $rootScope._request = function (action, data) {
                return $http.post(config.api, {
                    Action: action,
                    Params: data
                }).success(function (res) {
                    //TODO if dont authentication then location to login
                    return res;
                });
            };
        }];


    return extendRootScope;

});