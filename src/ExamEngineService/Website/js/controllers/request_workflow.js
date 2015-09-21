/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "team-select", "custom-select","disabled-when-click","process"], function (app) {

    app.controller("request_workflow", ["$scope", "$window","Process",
        function ($scope, $window,Process) {
            $scope.teamSource = [];

            $scope.workflows = [{
                text: "--请选择--"
                , value: -1
            }];

            $scope.selectedWorkflow = "";

            $scope.getAllWorkflows = function (loading) {
                return Process.getAllProcess(loading).then(function (res) {
                    $scope.workflows=Process.convertToKV(res.Data.AllProcess);
                    angular.forEach(res.Data.Teams, function (ele) {
                        $scope.teamSource.push({
                            text: ele
                            , value: ele
                        });
                    });
                });
            };

            $scope.changeWorkflow = function ($event) {
                if ($event.newValue != -1) {
                    $scope.selectedWorkflow = $event.newValue;
                }
            };

            function buildPostData(model) {
                var data = {};
                data.ProcessName = model.ProcessName.ProcessName;
                data.NodeTeams = [];
                for (var name in model.NodeTeams) {
                    data.NodeTeams.push({
                        NodeName: name
                        , TeamName: model.NodeTeams[name]
                    });
                }
                return data;
            }

            $scope.initExam = function (model) {
                return $scope._request("InitExam", buildPostData(model)).then(function (res) {
                    $window.alert("发起流程成功");
                    $scope._goto("/default");
                });
            };

            $scope.getAllWorkflows(true);


        }]);

});