/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "app.config"], function (app, config) {

    app.controller("partial_container", ["$scope", function ($scope) {
        //TODO 根据每个流程动态读取form
        var path = "forms/test-frm1.html";
        var deps = config.formDependence[path];
        require(deps, function () {
            $scope.path = path;
            $scope.$apply();
        });
    }]);
});