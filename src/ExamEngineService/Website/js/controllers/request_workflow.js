/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "team-select", "custom-select"], function (app) {

    app.controller("request_workflow", ["$scope", function ($scope) {
        $scope.teamSource = [{
            text: "Team1"
            , value: 1
        }];

        $scope.getAllWorkflows=function(){
            return $scope._request("AllProcess").then(function(res){

            });
        };

        $scope.getAllWorkflows();

        $scope.workflows = [{
            text: "sdfdf"
            , value: 1
        }];

    }]);

});