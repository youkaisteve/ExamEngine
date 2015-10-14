/**
 * Created by hai.ma on 2015/9/9 0009.
 */
"use strict";
define(["app", "app.config"], function (app, config) {

    app.directive("fileUploader", ["$timeout", "$window", "$parse", "$sessionStorage", "$q",
        function ($timeout, $window, $parse, $sessionStorage, $q) {
            return {
                restrict: "E"
                , templateUrl: "js/directive/file-uploader/template.html"
                , link: function (scope, ele, attrs, ctrl) {
                    scope.running = false;
                    scope.process = "上传中...";
                    scope.multiple = attrs.multiple;
                    scope.fileIndex = 0;
                    scope.fileCount = 0;

                    var eleFile = $(ele).find("[sid=file]").get(0);
                    //var xhr = new XMLHttpRequest();

                    var onComplete = $parse(attrs.oncomplete);
                    var url = $parse(attrs.url);

                    var fileTexts = [];

                    scope.sendFile = function () {
                        if (!eleFile) {
                            eleFile = ele.find("[sid=file]").get(0);
                        }

                        if (eleFile.files.length <= 0) {
                            $window.alert("请选择文件");
                            return;
                        }
                        scope.running = true;
                        scope.fileCount = eleFile.files.length;

                        //console.log("start upload : ", scope.fileIndex);
                        //send file one by one
                        if (scope.fileIndex < eleFile.files.length) {
                            return scope.send(eleFile.files[scope.fileIndex]).then(function (data) {
                                //console.log("上传完成");
                                scope.fileIndex++;
                                //scope.$apply();
                                fileTexts.push(data);
                                return scope.sendFile();
                            }).catch(function () {
                                scope.running = false;
                                scope.fileIndex = 0;
                                //console.log("upload error");
                                //scope.$apply();
                            });
                        }
                        else {
                            scope.running = false;
                            scope.fileIndex = 0;
                            //scope.$apply();
                            onComplete(scope, {
                                $result: fileTexts
                            });
                        }
                    };

                    //var xhr;
                    scope.send = function (file) {
                        var deferred = $q.defer();
                        var xhr = new XMLHttpRequest();
                        xhr.addEventListener("readystatechange", function () {
                            if (this.readyState == 4 && this.status == 200) {
                                deferred.resolve(this.responseText);
                            }
                        }, false);
                        xhr.addEventListener("error", function () {
                            deferred.reject(this, file);
                        }, false);
                        var fd = new FormData();
                        xhr.open("POST", url(scope), true);
                        xhr.setRequestHeader("user-authorize", $sessionStorage.token);
                        fd.append('file', file);

                        xhr.send(fd);
                        return deferred.promise;
                    };


                }
            };
        }]);

});