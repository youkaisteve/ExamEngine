/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app"], function (app) {

    app.controller("default", ["$scope", function ($scope) {

        //TODO location target page by role



        $scope.path = "partials/default_student.html";

        $scope.tasks = [1];

        $scope._request("Tasks",{
            UserID:"007"
        }).success(function(res){

        });

    }]);

});