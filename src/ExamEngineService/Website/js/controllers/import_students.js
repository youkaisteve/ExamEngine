/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "file-uploader"], function (app) {

    app.controller("import_students", ["$scope", "$window",
        function ($scope, $window) {
            $scope.fixedFooter = true;
            $scope.students=[];
            $scope.uploadComplete = function (res) {
                if (res.Code === 0) {
                    $scope.students = res.Data;
                    $scope.$apply();
                }
                else {
                    $window.alert(res.ErrorMessage);
                }
            };
        }]);

});