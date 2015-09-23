/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "dialog"], function (app) {

    app.controller("test", ["$scope", "$window", "Dialog",
        function ($scope, $window, Dialog) {

            Dialog.open({
                title: "test"
            }).then(function (ele) {
                $(document.body).append(ele);
            });

        }]);

});