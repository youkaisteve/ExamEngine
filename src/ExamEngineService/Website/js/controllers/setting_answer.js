/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "custom-select", "disabled-when-click"], function (app) {

    app.controller("setting_answer", ["$scope", "$window", "$routeParams","$q",
        function ($scope, $window, $routeParams,$q) {
            $scope.Model = {};
            $scope.formPath = "";
            $scope.formName = "";
            $scope.workflowName = "";

            function getFileName(path) {
                var last = path.lastIndexOf("/");
                return path.substring(last + 1);
            }

            function getWorkflowName(fleName) {
                var index = fleName.indexOf("_");
                if (index >= 0) {
                    return fleName.substring(0, index);
                }
                else {
                    return "";
                }
            }

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

                if (arr.length <= 0) {
                    arr = [0];
                }

                return arr;
            };

            $scope.loadForm = function (loading) {
                var path = decodeURI($routeParams.name);
                var fileName = getFileName(path);
                $scope.formName = fileName;
                var workflowName = getWorkflowName(fileName);
                $scope.workflowName = workflowName;
                return $scope._request("FormData", {
                    TemplateName: fileName
                }, loading).then(function (res) {
                    if (res.Data) {
                        $scope.Model = JSON.parse(res.Data.TemplateData);
                        $scope.templateDesc = res.Data.TemplateDesc;
                    }
                    $scope.formPath = path;

                });
            };

            $scope.save = function (form) {
                //if(form.$invalid){
                //    return $q();
                //}
                return $scope._request("SaveAnswer", {
                    TemplateName: $scope.formPath
                    , TemplateDesc: $scope.templateDesc
                    , TemplateData: JSON.stringify($scope.Model)
                }).then(function (res) {
                    $window.alert("保存成功");
                    $scope._goto("/forms");
                });
            };

            $scope.loadForm(true);

        }]);

});