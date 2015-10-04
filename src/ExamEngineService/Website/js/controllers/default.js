/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "disabled-when-click", "process", "dialog"]
    , function (app) {

        app.controller("default", ["$scope", "$window", "Process", "Dialog"
            , function ($scope, $window, Process, Dialog) {

                var user = $scope._auth();
                if (user) {
                    $scope.path = user.AuthName;
                }
                if ($scope.path.indexOf("teacher") >= 0) {
                    $scope.fixedFooter = true;
                    $scope.doneAll = function () {
                        return Dialog.confirm($scope, "是否将所有业务关闭?", function () {
                            return Process.doneAllProcess(true);
                        });
                    };
                    $scope.clearData = function () {
                        return Dialog.confirm($scope, "是否要清除所有业务数据?", function () {
                            //TODO
                        });
                    };
                }
                else {
                    $scope.tasks = [];

                    $scope.getTasks = function (loading) {
                        return $scope._request("Tasks", {
                            UserId: user.UserID
                        }, loading).then(function (res) {
                            $scope.tasks = res.Data.Tasks;
                        });
                    };

                    $scope.gotoDetail = function (task) {
                        $scope.sessionStorage.currentTask = task;
                        $scope._goto("/task/" + task.InstanceId + "/" + task.TokenID);
                    };

                    $scope.getTasks(true);


                }

            }]);

    });