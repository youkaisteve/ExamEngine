/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "app.config"], function (app, config) {

    app.controller("task", ["$scope", "$routeParams", function ($scope, $routeParams) {
        var tokenID = $routeParams.tokenID;
        var instanceID=$routeParams.instanceID;
        //TODO 根据任务ID获得流程相关的数据,如:对应的表单,按钮,权限等
        //var path = "forms/医疗保险费单位缴费月报表.html";

        $scope.getDetail=function(tokenID,instanceID){
            return $scope._request("TaskDetail",{
                InstanceId:instanceID
                ,TokenId:tokenID
            }).success(function(res){

                return res;
            });
        };

        $scope.getDetail(tokenID,instanceID);

        tokenID = "forms/verifystaff.html";
        var task = config.formDependence[tokenID];
        if (task) {
            if (task.deps) {
                require(task.deps, function () {
                    $scope.path = task.path;
                    $scope.$apply();
                });
            }
            else {
                $scope.path = task.path;
            }
        }
        else {
            $scope.path = tokenID;
        }
    }]);
});