﻿/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "app.config", "disabled-when-click"], function (app, config) {

    app.controller("task", ["$scope", "$routeParams", function ($scope, $routeParams) {
        $scope.taskDetail = $scope.sessionStorage.currentTask;
        $scope.Model={};
        if (!$scope.taskDetail) {
            $scope._goto("/default");
        }
        else {
            //delete $scope.sessionStorage.currentTask;
            var tokenID = $routeParams.tokenID;
            var instanceID = $routeParams.instanceID;
            $scope.getDetail = function (tokenID, instanceID,loading) {
                return $scope._request("TaskDetail", {
                    InstanceId: instanceID
                    , TokenId: tokenID
                },loading).then(function (res) {
                    $scope.path = res.Data.Page;
                    angular.extend($scope.taskDetail, res.Data);
                    return res;
                });
            };

            $scope.handle = function (data) {
                return $scope._request("Process", {
                    InstanceId: $scope.taskDetail.InstanceId
                    , TokenId: $scope.taskDetail.TokenID
                    , DefineName: $scope.taskDetail.DefineName
                    , TokenName: $scope.taskDetail.TokenName
                    , TransitionName: $scope.taskDetail.TransitionName
                    , TemplateName: $scope.taskDetail.Page
                    , TemplateData: JSON.stringify($scope.Model)
                    , TransitionName: data.TransitionName
                }).then(function (res) {
                    $scope._goto("/default");
                });
            };

            $scope.getDetail(tokenID, instanceID,true);

        }
    }]);
});