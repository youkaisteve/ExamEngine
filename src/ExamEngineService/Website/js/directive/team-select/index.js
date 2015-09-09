/**
 * Created by hai.ma on 2015/9/9 0009.
 */
"use strict";
define(["app"], function (app) {

    app.directive("teamSelect", ["$timeout", function ($timeout) {
        return {
            restrict: "E"
            , templateUrl: "js/directive/team-select/template.html"
            , require: ["?ngModel"]
            , scope: {
                selectedValue: "=ngModel"
                , source: "="
            }
            , link: function (scope, ele, attrs, ctrl) {
                scope.selectedText = "请选择组";
                function findItem(value) {
                    var i = 0, len = scope.source.length;
                    for (; i < len; i++) {
                        if (scope.source[i].value === value) {
                            return Object.create(scope.source[i]);
                        }
                    }
                    return null;
                }

                scope.hasNoSelected = function () {
                    var i = 0, len = scope.source.length;
                    for (; i < len; i++) {
                        if (!scope.source[i].selected) {
                            return true;
                        }
                    }
                    return false;
                };

                if (scope.selectedValue) {
                    var selected = findItem(scope.selectedValue);
                    if (selected) {
                        scope.selectedText = selected.text;
                    }
                }

                if (scope.source) {
                    angular.forEach(scope.source, function (ele) {
                        ele.selected = false;
                    });
                }

                scope.$watch(function () {
                    return scope.selectedValue;
                }, function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        var item = findItem(newValue);
                        if (item) {
                            scope.selectedText = item.text;
                        }
                    }
                });
            }
        };
    }]);

});