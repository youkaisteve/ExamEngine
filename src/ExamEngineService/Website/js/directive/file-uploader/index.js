/**
 * Created by hai.ma on 2015/9/9 0009.
 */
"use strict";
define(["app", "app.config"], function (app, config) {

    app.directive("fileUploader", ["$timeout", "$window", "$parse",
        function ($timeout, $window, $parse) {
            return {
                restrict: "E"
                , templateUrl: "js/directive/file-uploader/template.html"
                //, scope: {
                //    complete: "&oncomplete"
                //}
                , link: function (scope, ele, attrs, ctrl) {
                    scope.running = false;
                    scope.process = "上传中...";
                    scope.selectedFile = null;

                    var eleFile = ele.find("#file").get(0);

                    //scope.selectFile = function (event) {
                    //    console.log(event);
                    //};

                    var xhr = new XMLHttpRequest();
                    //xhr.addEventListener("progress", function (e) {
                    //    console.log("process");
                    //    //if (e.lengthComputable) {
                    //    //    scope.process = Math.round((e.loaded * 100) / e.total);
                    //    //}
                    //}, false);
                    //xhr.upload.addEventListener("load", function (e) {
                    //    scope.process = 100;
                    //    scope.running = false;
                    //}, false);

                    xhr.addEventListener("readystatechange",function(){
                        if (this.readyState == 4 && this.status == 200) {
                            scope.running = false;
                            scope.$apply();
                            var fn = $parse(attrs.oncomplete);
                            fn(scope, {
                                $result: JSON.parse(this.responseText)
                            });
                        }
                    },false);

                    scope.sendFile = function () {

                        if (eleFile.files.length <= 0) {
                            $window.alert("请选择文件");
                            return;
                        }
                        scope.running = true;
                        var fd = new FormData();

                        xhr.open("POST", config.importStudentUri, true);

                        fd.append('file', eleFile.files[0]);
                        // Initiate a multipart/form-data upload
                        xhr.send(fd);
                    }


                }
            };
        }]);

});