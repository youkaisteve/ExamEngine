/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "app.config", "file-uploader"], function (app, config) {

    app.controller("import_students", ["$scope", "$window",
        function ($scope, $window) {
            $scope.config = config;
            $scope.fixedFooter = true;
            $scope.students = [];
            $scope.uploadComplete = function (data) {
                if (data.length > 0) {
                    var res = JSON.parse(data[0]);
                    if (res.Code === 0) {
                        $scope.students = res.Data;
                        $scope.$apply();
                    }
                    else {
                        $window.alert(res.ErrorMessage);
                    }
                }
            };
            $scope.getStudents=function(){
                return $scope._request("GetAllStudent",null,true).then(function(res){
                    $scope.students = res.Data;
                });
            };
            $scope.getStudents();
        }]);

});