/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "custom-select", "disabled-when-click","filters"], function (app) {

    app.controller("setting_forms", ["$scope", "$window",
        function ($scope, $window) {

            $scope.Model = {};
            $scope.formPath = "";
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

            $scope.getRows = function (model) {
                var arr = [];
                if (model) {
                    for (var p in model) {
                        arr.push(arr.length);
                    }
                }
                else {
                    arr = [0];
                }
                return arr;
            };

            $scope.loadForm = function ($event, loading) {

                return $scope._request("FormData", {
                    TemplateName: $scope.formName
                }, loading).then(function (res) {
                    if (res.Data) {
                        $scope.Model = JSON.parse(res.Data.TemplateData);
                        $scope.templateDesc = res.Data.TemplateDesc;
                    }
                    $scope.formPath = $scope.formName;
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