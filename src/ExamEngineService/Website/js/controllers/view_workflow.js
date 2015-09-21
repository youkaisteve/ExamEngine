/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "custom-select", "disabled-when-click","process"], function (app) {

    app.controller("view_workflow", ["$scope", "$window","Process",
        function ($scope, $window,Process) {

            $scope.imageBase64="";
            $scope.allProcess=[];

            $scope.loadProcessImage=function($event,loading){
                return Process.getProcessImage($scope.definedName.ProcessName,loading).then(function(res){
                    //$scope.imageBase64==res.
                });
            };

            Process.getAllProcess(true).then(function(res){
                $scope.allProcess=Process.convertToKV(res.Data.AllProcess);
            });

        }]);

});