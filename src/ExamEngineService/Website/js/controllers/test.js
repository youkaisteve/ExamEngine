/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "dialog"], function (app) {

    app.controller("test", ["$scope", "$window", "Dialog",
        function ($scope, $window, Dialog) {


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