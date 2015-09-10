/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app"], function (app) {

    app.controller("default", ["$scope", "$window", function ($scope, $window) {

        //location target page by role
        var user = $scope._auth();
        if (user) {
            $scope.path = user.AuthName;
        }
        if ($scope.path.indexOf("teacher") >= 0) {
            $scope.fixedFooter = true;
        }
        else {
            $scope.tasks = [];

            $scope.getTasks = function () {
                $scope._request("Tasks", {
                    UserId: "007"
                }).success(function (res) {
                    if (res.Code === 0) {
                        $scope.tasks = res.Data.Tasks;
                    }
                    else {
                        $window.alert(res.ErrorMessage);
                    }
                });
            };

            $scope.getTasks();
        }

    }]);

});