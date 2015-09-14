/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app","disabled-when-click"], function (app) {

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
                return $scope._request("Tasks", {
                    UserId: user.UserID
                }).then(function (res) {
                    $scope.tasks = res.Data.Tasks;
                });
            };

            $scope.gotoDetail=function(task){
                $scope.sessionStorage.currentTask=task;
                $scope._goto("/task/"+task.InstanceId+"/"+task.TokenID);
            };

            $scope.getTasks();


        }

    }]);

});