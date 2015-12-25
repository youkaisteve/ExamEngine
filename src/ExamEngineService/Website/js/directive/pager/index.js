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
                //var onPageChange = $parse(attrs.onpagechange);
                //计算页数
                function caluPageCount() {
                    scope.pageCount = Math.ceil(scope.total / scope.pageSize);
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

                scope.isShow = function (page, pageIndex, total) {
                    var min = pageIndex - 2;
                    var max = pageIndex + 2;
                    if (min < 1) {
                        min = 1;
                        max += 2;
                    }
                    if (max > total) {
                        max = total;
                        min -= 2;
                    }
                    return page >= min && page <= max;
                };

                //page change
                scope.$watch("pageIndex", function (newValue, oldValue) {
                    if (newValue != oldValue) {
                        scope.onpagechange({
                            $pageIndex: scope.pageIndex,
                            $pageSize: scope.pageSize
                        });
                    }
                }, true);
                scope.$watch("pageSize", function (newValue, oldValue) {
                    if (!angular.equals(newValue, oldValue)) {
                        scope.onpagechange({
                            $pageIndex: scope.pageIndex,
                            $pageSize: scope.pageSize
                        });
                    }
                });
                scope.$watch("total", function (newValue, oldValue) {
                    if (!angular.equals(newValue, oldValue)) {
                        caluPageCount();
                    }
                }, true);
            }
        };
    }]);

});