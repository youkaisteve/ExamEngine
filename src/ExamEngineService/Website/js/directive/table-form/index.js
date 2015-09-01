"use strict";
define(["app"], function (app) {

    app.directive("tableForm", ["$timeout", function ($timeout) {
        return {
            restrict: "EA"
            , templateUrl: "js/directive/table-form/template.html"
            , require: "?ngModel"
            , scope: {
                defined: "="
                , modelName: "@ngModel"
                , ngModel: "="
            }
            , link: function (scope, ele, attrs, ctrl) {
                if (!scope.ngModel) {
                    scope.ngModel = {};
                }
                var index = scope.modelName.lastIndexOf(".");
                scope.modelName = scope.modelName.substring(index + 1);
                scope.rows = [0];
            }
        };
    }]);

});