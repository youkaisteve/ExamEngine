/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "process", "dialog", "pager", "disabled-when-click", "file-uploader"], function (app) {

    app.controller("question", ["$scope", "$window", "Dialog", "Process",
        function ($scope, $window, Dialog, Process) {
            $scope.ProcessService = Process;

            $scope.PageInfo = {
                PageIndex: 1,
                PageSize: 10,
                Total: 0
            };

            $scope.Questions = [];

            $scope.query = function () {
                return Process.queryQuestion($scope.PageInfo, true).then(function (res) {
                    if (res.Data) {
                        $scope.PageInfo.Total = res.Data.Page.Total;
                        $scope.Questions = res.Data.Result;
                    }
                });
            };

            $scope.importComplete = function () {

            };

            $scope.active = function (sysNo) {
                return Process.activeQuestion([sysNo]).then($scope.query);
            };

            $scope.del = function (sysNo) {
                return Process.deleteQuestion([sysNo]).then($scope.query);
            };


            $scope.query();
        }]);

});