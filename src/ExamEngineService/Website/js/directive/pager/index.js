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
                onpagechange: "&"
            }
            , link: function (scope, ele, attrs, ctrl) {
                scope.pageCount = 0;
                scope.pageNumbers = [];
                scope.displayCount = 5;
                //var onPageChange = $parse(attrs.onpagechange);
                //计算页数
                function caluPageCount() {
                    scope.pageCount = Math.ceil(scope.total / scope.pageSize);
                }

                function buildPageNumbers() {
                    var arr = [];
                    var begin = scope.pageIndex - (Math.floor(scope.displayCount / 2));
                    if (begin < 0) {
                        begin = 0;
                    }
                    var num;
                    for (var i = 0; i < scope.displayCount; i++) {
                        num = begin + i;
                        if (num < scope.pageCount) {
                            arr.push(num);
                        }
                    }
                    scope.pageNumbers=arr;
                }

                scope.prev = function () {
                    if (scope.pageIndex > 1) {
                        scope.pageIndex--;
                    }
                };
                scope.next = function () {
                    if (scope.pageIndex < scope.pageCount) {
                        scope.pageIndex++;
                    }
                };
                scope.goto = function (index) {
                    if (index < 1) {
                        index = 1;
                    }
                    if (index > scope.pageCount) {
                        index = scope.pageCount;
                    }
                    scope.pageIndex = index;
                };

                //page change
                scope.$watch("pageIndex", function (newValue, oldValue) {
                    if (newValue != oldValue) {
                        scope.onpagechange({
                            $pageIndex: scope.pageIndex,
                            $pageSize: scope.pageSize
                        });
                        buildPageNumbers();
                    }
                }, true);
                scope.$watch("pageSize", function (newValue, oldValue) {
                    if (!angular.equals(newValue, oldValue)) {
                        scope.onpagechange({
                            $pageIndex: scope.pageIndex,
                            $pageSize: scope.pageSize
                        });
                        buildPageNumbers();
                    }
                }, true);
                scope.$watch("total", function (newValue, oldValue) {
                    if (!angular.equals(newValue, oldValue)) {
                        caluPageCount();
                        buildPageNumbers();
                    }
                }, true);
            }
        };
    }]);

});