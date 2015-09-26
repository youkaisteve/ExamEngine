/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "custom-select", "disabled-when-click", "process", "filters"], function (app) {

    app.controller("view_workflow", ["$scope", "$window", "Process",
        function ($scope, $window, Process) {

            //$scope.imageBase64 = "";
            $scope.allProcess = [];
            $scope.unfinshedTasks = [];
            $scope.totalPage = 0;
            $scope.filter = {
                PageInfo: {
                    PageIndex: 0
                    , PageSize: 10
                }
            };
            $scope.$watch("filter", function (newVal, oldVal) {
                if (!angular.equals(newVal, oldVal)) {
                    $scope.getTasks(newVal, true);
                }
            }, true);
            //$scope.loadProcessImage = function ($event, loading) {
            //    return Process.getProcessImage($scope.definedName.ProcessName, loading).then(function (res) {
            //        $scope.imageBase64 =  res.Data.Image;
            //    });
            //};

            Process.getAllProcess(true).then(function (res) {
                $scope.allProcess = Process.convertToKV(res.Data.AllProcess);
            });


            $scope.getTasks = function (filter, loading) {
                return Process.getUnfinshedTask(filter, loading).then(function (res) {
                    $scope.unfinshedTasks = res.Data.Processes;
                    $scope.totalPage = Math.floor(res.Data.TotalCount / filter.PageInfo.PageSize);
                });
            };

        }]);

});