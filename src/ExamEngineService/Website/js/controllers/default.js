/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app"], function (app) {

    app.controller("default", ["$scope", "$window", function ($scope, $window) {

        //location target page by role
        var user=$scope._auth();
        $scope.path = "partials/default_student.html";
        if($scope.path.indexOf("teacher")>=0){
            $scope.fixedFooter = true;
        }

        //$scope.path = "partials/default_student.html";
        //$scope.path = "partials/default_teacher.html";
        $scope.tasks = [];

        $scope.getTasks = function () {
            $scope._request("Tasks", {
                UserId: "007"
                //, PageInfo: {
                //    PageIndex: 0
                //    , PageSize: 100
                //}
            }).success(function (res) {
                if (res.Code === 0) {
                    $scope.tasks = res.Data.Tasks;
                }
                else {
                    $window.alert(res.ErrorMessage);
                }
            });
        };

        $scope.getTasks();

    }]);

});