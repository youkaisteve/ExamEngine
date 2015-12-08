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

            $scope.query = function (pageIndex, pageSize) {
                return Process.queryQuestion({
                    PageInfo: {
                        PageIndex: pageIndex || $scope.PageInfo.PageIndex,
                        PageSize: pageSize || $scope.PageInfo.PageSize
                    }
                }, true).then(function (res) {
                    if (res.Data) {
                        $scope.PageInfo = res.Data.Page;
                        $scope.Questions = res.Data.Result;
                    }
                });
            };

            $scope.importComplete = function () {

            };

            $scope.active = function (sysNo) {
                return Process.activeQuestion([sysNo]).then(function(){
                    return $scope.query();
                });
            };

            $scope.del = function (sysNo) {
                return Process.deleteQuestion([sysNo]).then(function(){
                    return $scope.query();
                });
            };


            $scope.query();
        }]);

});