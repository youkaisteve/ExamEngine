"use strict";
define(["app", "filters"], function (app) {

    app.directive("pager", ["$timeout", function ($timeout) {
        return {
            restrict: "E"
            , templateUrl: "js/directive/pager/template.html"
            //, require: "?ngModel"
            , scope: {
                pageInfo: "=",
                onPageChange: "&"
            }
            , link: function (scope, ele, attrs, ctrl) {
                //计算页数
                function caluPageCount() {
                    scope.pageInfo.PageCount = Math.ceil(scope.pageInfo.Total / scope.pageInfo.PageSize);
                }

                scope.prev = function () {
                    scope.pageInfo.PageIndex--;
                };
                scope.next = function () {
                    scope.pageInfo.PageIndex++;
                };
                scope.goto = function (index) {
                    scope.pageInfo.PageIndex = index;
                };

                //page change
                scope.$watch("pageInfo", function (newValue, oldValue) {
                    if (newValue.PageIndex != oldValue.PageIndex
                        || newValue.PageSize != oldValue.PageSize) {
                        scope.onPageChange(newValue);
                    }
                    if (newValue.PageSize != oldValue.PageSize) {
                        caluPageCount();
                    }
                }, true);

                caluPageCount();
            }
        };
    }]);

});