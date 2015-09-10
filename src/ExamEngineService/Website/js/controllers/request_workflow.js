/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "team-select"], function (app) {

    app.controller("request_workflow", ["$scope", function ($scope) {
        $scope.teamSource = [{
            text: "Team1"
            , value: 1
        }];

        $scope.workflows = [];
    }]);

});