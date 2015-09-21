/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
"use strict";

define(["app.config"], function (config) {

    return ["$rootScope", "$http", "$sessionStorage", "$window", "$q",
        function ($rootScope, $http, $sessionStorage, $window, $q) {

            $rootScope.sessionStorage = $sessionStorage;
            $rootScope.fixedFooter = false;
            $rootScope.environment = config.environment;
            $rootScope._AUTH_KEY = "auth";

            function toLogin() {
                $rootScope._goto("#/login");
            }

            function clear(){
                $sessionStorage.$reset();
                $rootScope.userName=null;
            };

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
                    $rootScope.userName = auth.UserName;
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
                if (!$rootScope._auth()) {
                    toLogin();
                }
            });

            $rootScope._request = function (action, data, loading) {
                var deferred = $q.defer();
                var eleLoading;
                if (loading) {
                    eleLoading = $rootScope._notify("加载中...");
                }
                $http.post(config.api, data, {
                    headers: {
                        "action": action
                        , "user-authorize": $sessionStorage.token
                    }
                }).success(function (res, status, headers) {
                    if (res.Code === 0) {
                        if (!$sessionStorage.token) {
                            $sessionStorage.token = headers("user-authorize");
                        }
                        deferred.resolve(res);
                    }
                    else if (res.Code === 3) {
                        clear();
                        $window.alert(res.ErrorMessage);
                        $rootScope._goto("/login");
                    }
                    else {
                        $window.alert(res.ErrorMessage);
                        deferred.reject(res);
                    }
                }).error(function () {
                    $window.alert("系统错误,请联系管理员");
                }).finally(function () {
                    if (eleLoading) {
                        eleLoading.remove();
                    }
                });
                return deferred.promise;
            };

            $rootScope._logout = function () {
                return $rootScope._request("Logout", {}).then(function (res) {
                    clear();
                    toLogin();
                });
            };

            $rootScope._notify = function (message, type) {
                if (!type) {
                    type = "alert-warning";
                }
                var div = $("<div>").addClass("notify alert").addClass(type).text(message);
                div.appendTo(document.body);
                return div;
            };

            $rootScope.getRows = function (model) {
                var arr = [];
                if (model) {
                    for (var p in model) {
                        arr.push(arr.length);
                    }
                }
                else {
                    arr = [0];
                }
                return arr;
            };

        }];

});