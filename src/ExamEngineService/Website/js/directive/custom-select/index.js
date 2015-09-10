/**
 * Created by hai.ma on 2015/9/9 0009.
 */
"use strict";
define(["app"], function (app) {

    app.directive("customSelect", ["$timeout", function ($timeout) {
        return {
            restrict: "E"
            , templateUrl: "js/directive/custom-select/template.html"
            , require: ["?ngModel"]
            , scope: {
                selectedValue: "=ngModel"
                , source: "="
            }
            , link: function (scope, ele, attrs, ctrl) {
                scope.selectedText = "";
                function findItem(value) {
                    var i = 0, len = scope.source.length;
                    for (; i < len; i++) {
                        if (scope.source[i].value === value) {
                            return scope.source[i];
                        }
                    }
                    return null;
                }

                //scope.hasNoSelected = function () {
                //    var i = 0, len = scope.source.length;
                //    for (; i < len; i++) {
                //        if (!scope.source[i].selected) {
                //            return true;
                //        }
                //    }
                //    return false;
                //};

                var tip = {
                    text: "--请选择--"
                    , value: -1
                };

                if (scope.source) {
                    if (scope.source.length > 0) {
                        if (scope.source[0].value !== tip.value) {
                            scope.source.splice(0, 0, tip);
                        }
                    }
                    angular.forEach(scope.source, function (ele) {
                        ele.selected = false;
                    });
                }

                if (scope.selectedValue) {
                    var selected = findItem(scope.selectedValue);
                    if (selected) {
                        scope.selectedText = selected.text;
                    }
                }
                else {
                    scope.selectedText = tip.text;
                    scope.selectedValue = tip.value;
                }

                scope.$watch(function () {
                    return scope.selectedValue;
                }, function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        var newItem = findItem(newValue);
                        if (newItem) {
                            scope.selectedText = newItem.text;
                            //if(newItem.value!==tip.value) {
                            //    newItem.selected = true;
                            //}
                        }
                        //var oldItem = findItem(oldValue);
                        //if (oldItem) {
                        //    oldItem.selected = false;
                        //}
                    }
                });
            }
        };
    }]);

});