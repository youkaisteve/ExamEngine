/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "process", "dialog", "pager","disabled-when-click"], function (app) {

    app.controller("maintain_process", ["$scope", "$window", "Dialog", "Process",
        function ($scope, $window, Dialog, Process) {
            $scope.ProcessService=Process;
            $scope.PageInfo = {
                PageIndex: 1,
                PageSize: 10,
                Total: 0
            };
            $scope.Processes = [];

            $scope.query = function () {
                return Process.queryProcess($scope.PageInfo, true).then(function (res) {
                    res.Data = {
                        "Page": {
                            "PageIndex": 1,
                            "PageSize": 10,
                            "Total": 1
                        },
                        "Result": [
                            {
                                "SysNo": 1,
                                "ProcessName": "Test",
                                "Category": "aaaa1",
                                "DifficultyLevel": "bbbb1",
                                "Description": "ccc1c",
                                "InUser": "001",
                                "InDate": "2015-12-02T14:21:36.617",
                                "LastEditUser": "001",
                                "LastEditDate": "2015-12-02T14:25:46.677",
                                "User": null
                            }
                        ]
                    };

                    if (res.Data) {
                        $scope.Processes = res.Data.Result;
                        $scope.PageInfo = res.Data.Page;
                    }
                });
            };
            $scope.updateProcessInfo=function(model,process){
                model.SysNo=process.SysNo;
                return $scope._request("UpdateProcessInfo",model,false).then(function(res){
                    if(res.Code==0) {
                        process.edit = false;
                        for (var key in model) {
                            process[key] = model[key];
                        }
                    }
                    else{
                        $scope._notify(res.Message);
                    }
                });
            };

            $scope.query();

        }]);

});