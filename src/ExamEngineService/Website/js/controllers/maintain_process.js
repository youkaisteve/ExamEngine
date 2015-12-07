/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "process", "dialog", "pager", "disabled-when-click"], function (app) {

    app.controller("maintain_process", ["$scope", "$window", "Dialog", "Process",
        function ($scope, $window, Dialog, Process) {
            $scope.ProcessService = Process;
            $scope.PageInfo = {
                PageIndex: 1,
                PageSize: 10,
                Total: 0
            };
            $scope.Processes = [];

            $scope.query = function () {
                return Process.queryProcess({
                    PageInfo: $scope.PageInfo
                }, true).then(function (res) {
                    if (res.Data) {
                        $scope.Processes = res.Data.Result;
                        $scope.PageInfo = res.Data.Page;
                    }
                });
            };
            $scope.updateProcessInfo = function (model, process) {
                model.SysNo = process.SysNo;
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
                return $scope._request("CreateProcessInfo", model, true).then(function (res) {
                    $scope.PageInfo.PageIndex = 1;
                    return $scope.query().then(function () {
                        $scope.Model2 = {};
                    });
                });
            };
            $scope.query();

        }]);

});