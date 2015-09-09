/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app"], function (app) {

    app.controller("default", ["$scope","$window", function ($scope,$window) {

        //TODO location target page by role


        //$scope.path = "partials/default_student.html";
        $scope.path = "partials/default_teacher.html";
        $scope.fixedFooter = true;

        $scope.tasks = [];

        $scope._request("Tasks", {
            UserId: "007"
            , PageInfo: {
                PageIndex: 0
                , PageSize: 100
            }
        }).success(function (res) {
            if(res.Code===0) {
                $scope.tasks =res.Data.Tasks;
            }
            else{
                $window.alert(res.ErrorMessage);
            }
        });

    }]);

});