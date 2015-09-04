/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app"], function (app) {

    app.controller("login", ["$scope", function ($scope) {

        $scope.login = function ($event, model) {
            //return $scope._request("Login", model).success(function (res) {
            //});
            //location to default page when login success
            window.location.hash = "/default";
        }


    }]);

});