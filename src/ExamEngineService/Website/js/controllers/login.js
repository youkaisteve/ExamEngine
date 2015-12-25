/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "disabled-when-click"], function (app) {

    app.controller("login", ["$scope", "$window", "$location"
        , function ($scope, $window, $location) {
            $scope.sso = false;
            var query = $location.search();
            //if url has token then auto login
            if (query.token) {
                $scope.sso = true;
                $scope._request("GetUserInfoByToken", {
                    token: query.token
                }).then(function (res) {
                    $scope._auth(res.Data);
                    //if url has action then location to action
                    if (query.action) {
                        $scope._goto(query.action);
                    }
                    else {
                        $scope._goto("/default");
                    }
                });
            }

            $scope.login = function ($event, model, loading) {
                return $scope._request("Login", model, loading).then(function (res) {
                    $scope._auth(res.Data);
                    $scope._goto("/default");
                });
            };

        }]);

});