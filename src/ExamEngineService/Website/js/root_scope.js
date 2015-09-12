/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
"use strict";

define(["app.config"], function (config) {

    var extendRootScope = ["$rootScope", "$http", "$sessionStorage", "$window","$q",
        function ($rootScope, $http, $sessionStorage, $window,$q) {

            $rootScope.sessionStorage = $sessionStorage;
            $rootScope.fixedFooter = false;
            $rootScope._AUTH_KEY = "auth";

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

            $rootScope._auth = function (auth) {
                if (auth) {
                    $sessionStorage[$rootScope._AUTH_KEY] = auth;
                }
                else {
                    return $sessionStorage[$rootScope._AUTH_KEY];
                }
            };

            var user = $rootScope._auth();
            if (user) {
                $rootScope.userName = user.UserName;
            }

            $rootScope.$on("$routeChangeStart", function (event, next, current) {
                //if not authentication then location to login
                if (!$rootScope._auth()) {
                    toLogin();
                }
            });

            //API method
            $rootScope._request = function (action, data) {
                var deferred=$q.defer();
                $http.post(config.api, data, {
                    headers: {
                        "action": action
                    }
                }).success(function(res){
                    if(res.Code===0){
                        deferred.resolve(res);
                    }
                    else{
                        $window.alert(res.ErrorMessage);
                        deferred.reject(res);
                    }
                }).error(function () {
                    $window.alert("系统错误,请联系管理员");
                });
                return deferred.promise;
            };

            $rootScope._logout = function () {
                return $rootScope._request("Logout", {}).then(function (res) {
                    //if success remove authentication then location to login
                    $sessionStorage.$reset();
                    toLogin();
                });
            };

            $rootScope._notify=function(message){

            };

        }];


    return extendRootScope;

});