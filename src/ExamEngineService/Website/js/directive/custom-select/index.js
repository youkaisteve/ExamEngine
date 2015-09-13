/**
 * Created by hai.ma on 2015/9/9 0009.
 */
"use strict";
define(["app"], function (app) {

    app.directive("customSelect", ["$timeout", "$parse",
        function ($timeout, $parse) {
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

                    var onChange = $parse(attrs.onchange);

                    scope.$watch(function () {
                        return scope.selectedValue;
                    }, function (newValue, oldValue) {
                        if (newValue !== oldValue) {
                            var newItem = findItem(newValue);
                            if (newItem) {
                                scope.selectedText = newItem.text;
                            }
                            onChange(scope.$parent, {
                                $event: {
                                    newValue: newValue
                                    , oldValue: oldValue
                                }
                            });
                        }
                    });
                }
            };
        }]);

});