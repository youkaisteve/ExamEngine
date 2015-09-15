/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app"], function (app) {

    app.controller("query_score", ["$scope", "$window",
        function ($scope, $window) {

            function compareObject(source, target) {
                var result = true;

                function getTargetValue(data, path) {
                    if (!data) data = target;
                    if (path.length > 0) {
                        var name = path.shift();
                        if (data[name] instanceof Object) {
                            return getTargetValue(data[name], path);
                        }
                        else {
                            return data[name];
                        }
                    }
                    else {
                        return null;
                    }
                }

                function compare(obj, path) {
                    for (var name in obj) {
                        path.push(name);
                        if (obj[name] instanceof Object) {
                            compare(obj[name], path);
                        }
                        else {
                            if (obj[name] != getTargetValue(target, angular.copy(path))) {
                                result = false;
                            }
                        }
                        path.pop();
                    }
                }

                compare(source, []);
                return result;
            }

            //test
            //var source = {
            //    test: {
            //        a: 1,
            //        b:3
            //    }
            //};
            //var target = {
            //    test: {
            //        a: 2
            //    }
            //}
            //
            //console.log(compareObject(source, target));
        }]);
});