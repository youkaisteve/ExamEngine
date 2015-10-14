"use strict";
define(["app"], function (app) {

    app.directive("disabledWhenClick", ["$timeout","$parse",
        function ($timeout,$parse) {
            return function (scope, ele, attrs, ctrl) {
                var fn=$parse(attrs.disabledWhenClick);
                ele.on("click",function(){
                    ele.attr("disabled","disabled");
                    fn(scope).finally(function(){
                        ele.removeAttr("disabled");
                    });
                });
            };
        }]);

});