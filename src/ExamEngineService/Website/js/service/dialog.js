/**
 * Created by hai.ma on 2015/9/23 0023.
 */
define(["app"], function (app) {
    app.factory("Dialog", ["$rootScope", "$http", "$templateCache", "$q", "$timeout", "$interpolate",
        function ($rootScope, $http, $templateCache, $q, $timeout, $interpolate) {
            var methods = {
                KEY_TMP: "template_dialog.html"
            };


            methods.getTemplate = function () {
                var deferred = $q.defer();

                function get() {
                    var tmp = $templateCache.get(methods.KEY_TMP);
                    if (tmp) {
                        deferred.resolve(tmp);
                    }
                    $timeout(get, 300);
                }

                get();
                return deferred.promise;
            };
            /*
            * {
            *   title:{string},
            *   body:{html}
            * }
            * */
            methods.open = function (context) {
                return methods.getTemplate().then(function (tmp) {
                    return $($interpolate(tmp)(context));
                });
            };

            //load dialog template
            $http.get("partials/template_dialog.html").then(function (res) {
                $templateCache.put(methods.KEY_TMP, res.data);
            });

            return methods;
        }]);
});