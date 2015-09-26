/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "custom-select", "disabled-when-click", "filters"], function (app) {

    app.controller("setting_forms", ["$scope", "$window",
        function ($scope, $window) {
            $scope.forms = [];

            function getFileName(path) {
                var last = path.lastIndexOf("/");
                return path.substring(last + 1);
            }

            $scope.getFormList = function (loading) {
                return $scope._request("FormList", null, loading).then(function (res) {
                    angular.forEach(res.Data, function (ele) {
                        $scope.forms.push({
                            text: getFileName(ele)
                            , value: ele
                        });
                    });
                });
            };

            $scope.getFormList(true);
        }]);

});