/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app","disabled-when-click"], function (app) {

    app.controller("login", ["$scope", "$window", function ($scope, $window) {

        $scope.login = function ($event, model,loading) {
            return $scope._request("Login", model,loading).then(function (res) {
                $scope._auth(res.Data);
                $scope._goto("/default");
            });
        };

    }]);

});