/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app"], function (app) {

    app.controller("default", ["$scope", function ($scope) {

        //TODO location target page by role



        //$scope.path = "partials/default_student.html";
        $scope.path = "partials/default_teacher.html";
        $scope.fixedFooter=true;

        $scope.tasks = [1];

        $scope._request("Tasks",{
            UserId:"007"
        }).success(function(res){

        });

    }]);

});