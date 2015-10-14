/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app", "app.config", "custom-select", "disabled-when-click", "filters", "dialog", "file-uploader"]
    , function (app, config) {
        app.controller("setting_forms", ["$scope", "$window", "Dialog",
            function ($scope, $window, Dialog) {
                $scope.config = config;
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

                $scope.showUploadForm = function () {
                    return Dialog.openUri($scope, {
                        title: "上传表单"
                        ,done:function(data){
                            console.log("upload complete");

                        }
                    }, "partials/dialog_upload_form.html");
                };

                $scope.getFormList(true);
            }]);

    });