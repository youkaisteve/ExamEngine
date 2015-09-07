/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
"use strict";

define(["app.config"], function (config) {

    var extendRootScope = ["$rootScope", "$http", "$sessionStorage",
        function ($rootScope, $http, $sessionStorage) {

            $rootScope.sessionStorage = $sessionStorage;
            $rootScope._TOKEN_KEY = "token";

            function toLogin() {
                $rootScope._goto("#/login");
            }

            $rootScope._goto = function (url) {
                if (url.indexOf("#") === 0) {
                    window.location.href = url;
                }
                else {
                    window.location.hash = url;
                }
            };

            $rootScope._token = function (token) {

                if (token) {
                    $sessionStorage[$rootScope._TOKEN_KEY] = token;
                }
                else {
                    return $sessionStorage[$rootScope._TOKEN_KEY];
                }
            };

            $rootScope.$on("$routeChangeStart", function (next, current) {
                //if not authentication then location to login
                if (!$rootScope._token()) {
                    toLogin();
                }
            });

            //API method
            $rootScope._request = function (action, data) {
                return $http.post(config.api, {
                    Action: action,
                    Params: data
                }).success(function (res) {
                    //if res.Code==401 then location to login
                    return res;
                });
            };

            $rootScope._logout = function () {
                return $rootScope._request("Logout", {}).success(function (res) {
                    //if success remove authentication then location to login
                    $sessionStorage.$reset();
                    toLogin();
                });
            };


        }];


    return extendRootScope;

});