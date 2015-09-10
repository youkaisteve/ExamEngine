/**
 * Created by hai.ma on 2015/9/9 0009.
 */
"use strict";
define(["app", "app.config"], function (app, config) {

    app.directive("fileUploader", ["$timeout","$window", function ($timeout,$window) {
        return {
            restrict: "E"
            , templateUrl: "js/directive/file-uploader/template.html"
            , link: function (scope, ele, attrs, ctrl) {
                scope.running = false;
                scope.process = 0;
                scope.selectedFile = null;

                var eleFile = ele.find("#file").get(0);

                scope.selectFile = function (event) {
                    console.log(event);
                };

                var xhr = new XMLHttpRequest();
                xhr.upload.addEventListener("progress", function (e) {
                    if (e.lengthComputable) {
                        scope.process = Math.round((e.loaded * 100) / e.total);
                    }
                }, false);
                xhr.upload.addEventListener("load", function (e) {
                    scope.process = 100;
                    scope.running = false;
                }, false);

                scope.sendFile = function () {

                    if (eleFile.files.length <= 0) {
                        $window.alert("请选择文件");
                        return;
                    }
                    scope.running=true;
                    var fd = new FormData();

                    xhr.open("POST", config.importStudentUri, true);
                    xhr.onreadystatechange = function () {
                        if (xhr.readyState == 4 && xhr.status == 200) {
                            // Handle response.
                            alert(xhr.responseText); // handle response.
                        }
                    };
                    fd.append('file', eleFile.files[0]);
                    // Initiate a multipart/form-data upload
                    xhr.send(fd);
                }


            }
        };
    }]);

});