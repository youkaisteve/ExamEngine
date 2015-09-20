/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "custom-select", "disabled-when-click"], function (app) {

    app.controller("setting_answer", ["$scope", "$window",
        function ($scope, $window) {

            $scope.formPath = "";
            $scope.forms = [];

            $scope.getFormList = function (loading) {
                return $scope._request("FormList", null, loading).then(function (res) {
                    angular.forEach(res.Data, function (ele) {
                        $scope.forms.push({
                            text: ele
                            , value: ele
                        });
                    });
                });
            };

            $scope.loadForm = function ($event, loading) {
                $scope.formPath = "forms/" + $scope.formName;
                return $scope._request("FormData", {
                    TemplateName: $scope.formPath
                }, loading).then(function (res) {
                    $scope.Model = res.Data;
                });
            };

            $scope.save = function () {
                return $scope._request("SaveAnswer", {
                    TemplateName: $scope.formName
                    , TemplateDesc: $scope.templateDesc
                    , TemplateData: JSON.stringify($scope.Model)
                }).then(function (res) {
                    $window.alert("保存成功");
                });
            };

            $scope.getFormList(true);
        }]);

});