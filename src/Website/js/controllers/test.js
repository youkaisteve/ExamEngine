/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "dialog"], function (app) {

    app.controller("test", ["$scope", "$window", "Dialog",
        function ($scope, $window, Dialog) {

            $scope.Dialog=Dialog;

            $scope.confirm=function(){
                Dialog.confirm($scope,"test",function(){
                    $window.alert("OK");
                },function(){
                    $window.alert("Cancel");
                });
            };

            $scope.alert = function () {
                Dialog.open($scope, {
                    title: "test"
                    , body: "<p style=\"color:red;\">message</p>"
                    , buttons: [{
                        text: "close"
                        , click: function () {
                            alert("click");
                        }
                    }]
                });
            };

            $scope.showForm= function () {
                Dialog.open($scope, {
                    title: "test"
                    , body: "<p style=\"color:red;\">message<input type='text' ng-model='abc'/>{{abc}}</p>"
                    , buttons: [{
                        text: "close"
                        , click: function () {
                            alert("click");
                        }
                    }]
                });
            };

        }]);

});