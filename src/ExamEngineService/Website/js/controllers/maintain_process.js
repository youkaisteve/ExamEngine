/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "process", "dialog", "pager", "disabled-when-click", "custom-select"], function (app) {

    app.controller("maintain_process", ["$scope", "$window", "Dialog", "Process",
        function ($scope, $window, Dialog, Process) {
            $scope.ProcessService = Process;
            $scope.Processes = [];
            $scope.pageSize = 10;
            $scope.pageIndex = 1;
            $scope.total = 0;

            $scope.workflows = [{
                text: "--请选择--"
                , value: -1
            }];

            $scope.getAllWorkflows = function () {
                return Process.getAllProcess(true).then(function (res) {
                    $scope.workflows = Process.convertToKV(res.Data.AllProcess);
                });
            };


            $scope.query = function (pageIndex, pageSize) {
                return Process.queryProcess({
                    PageInfo: {
                        PageSize: pageSize,
                        PageIndex: pageIndex
                    }
                }, true).then(function (res) {
                    if (res.Data) {
                        $scope.Processes = res.Data.Result;
                        //$scope.PageInfo = res.Data.Page;
                        $scope.pageIndex = pageIndex;
                        $scope.pageSize = pageSize;
                        $scope.total = res.Data.Page.Total;
                    }
                });
            };
            $scope.updateProcessInfo = function (model, process) {
                model.SysNo = process.SysNo;
                model.InUser = process.InUser;
                model.InDate = process.InDate;
                return $scope._request("UpdateProcessInfo", model, false).then(function (res) {
                    if (res.Code == 0) {
                        process.edit = false;
                        for (var key in model) {
                            process[key] = model[key];
                        }
                    }
                    else {
                        $scope._notify(res.Message);
                    }
                });
            };
            $scope.add = function (model) {
                model.ProcessName = model.ProcessName.ProcessName;
                return $scope._request("CreateProcessInfo", model, true).then(function (res) {
                    return $scope.query($scope.pageIndex, $scope.pageSize).then(function () {
                        $scope.Model2 = {};
                    });
                });
            };
            $scope.query($scope.pageIndex, $scope.pageSize);
            $scope.getAllWorkflows();
        }]);

});