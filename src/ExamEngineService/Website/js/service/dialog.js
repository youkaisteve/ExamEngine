/**
 * Created by hai.ma on 2015/9/23 0023.
 */
define(["app"], function (app) {
    app.factory("Dialog", ["$rootScope", "$http", "$templateCache", "$q", "$timeout", "$compile",
        function ($rootScope, $http, $templateCache, $q, $timeout, $compile) {
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
            methods.open = function ($scope, context) {
                var scope = $scope.$new(true, $scope);
                angular.extend(scope, context);
                scope.close = function (event) {
                    $(event.target).parents(".modal").remove();
                };
                return methods.getTemplate().then(function (tmp) {
                    var ele = $compile(tmp)(scope);
                    console.log(ele);
                    $(document.body).append(ele);
                    return ele;
                });
            };

            //load dialog template
            $http.get("partials/template_dialog.html").then(function (res) {
                $templateCache.put(methods.KEY_TMP, res.data);
            });

            return methods;
        }]);
});