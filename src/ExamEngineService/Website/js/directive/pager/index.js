"use strict";
define(["app", "filters"], function (app) {

    app.directive("pager", ["$timeout", "$parse", function ($timeout, $parse) {
        return {
            restrict: "E"
            , templateUrl: "js/directive/pager/template.html"
            //, require: "?ngModel"
            , scope: {
                pageIndex: "=",
                pageSize: "=",
                total: "=",
                onpagechange:"&"
            }
            , link: function (scope, ele, attrs, ctrl) {
                scope.pageCount = 0;
                //var onPageChange = $parse(attrs.onpagechange);
                //计算页数
                function caluPageCount() {
                    scope.pageCount = Math.ceil(scope.total / scope.pageSize);
                }

                scope.prev = function () {
                    scope.pageIndex--;
                };
                scope.next = function () {
                    scope.pageIndex++;
                };
                scope.goto = function (index) {
                    scope.pageIndex = index;
                };

                //page change
                scope.$watch("pageIndex", function (newValue, oldValue) {
                    if (newValue != oldValue) {
                        scope.onpagechange(scope.pageIndex,scope.pageSize);
                    }
                }, true);
                scope.$watch("total", function (newValue, oldValue) {
                    caluPageCount();
                }, true);
            }
        };
    }]);

});