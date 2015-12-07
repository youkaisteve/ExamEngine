define(["app", "dialog"], function (app) {
    app.factory("Process", ["$rootScope", "Dialog"
        , function ($rootScope, Dialog) {
            var methods = {};

            //获得所有的流程
            methods.getAllProcess = function (loading) {
                return $rootScope._request("AllProcess", null, loading);
            };

            //获得流程的流程图
            methods.getProcessImage = function (definedName, loading) {
                return $rootScope._request("ProcessImage", {
                    DefineName: definedName
                }, loading);
            };

            //获得流程当前状态的流程图
            methods.getTokenImage = function (model, loading) {
                return $rootScope._request("TokenImage", model, loading);
            };

            methods.convertToKV = function (allProcess) {
                var result = [];
                angular.forEach(allProcess, function (ele) {
                    result.push({
                        text: ele.ProcessName
                        , value: ele
                    })
                });
                return result;
            };

            //获得未完成的任务
            methods.getUnfinshedTask = function (filter, loading) {
                return $rootScope._request("UnFinishProcess", filter, loading);
            };

            //完成所有流程
            methods.doneAllProcess = function (loading) {
                return $rootScope._request("TerminateAllUnFinishProcess", null, loading);
            };

            //获得所有的流程,分页
            //var filter = {
            //    "PageInfo": {
            //        "PageIndex": 1,
            //        "PageSize": 10
            //    }
            //}
            methods.queryProcess = function (filter, loading) {
                return $rootScope._request("QueryProcess", filter, loading);
            }

            //显示流程图片
            methods.showProcessImage = function (name) {
                return methods.getProcessImage(name, true).then(function (res) {
                    var base64 = res.Data.Image;
                    var html = '<div style="text-align: center"><img style="max-width:100%;" src="data:image/png;base64,' + base64 + '"/></div>';
                    return Dialog.open($rootScope, {
                        title: name
                        , body: html
                        , heightPercent: 0.7
                        , style: {
                            width: "800px"
                        }
                    });
                });
            };

            //获得题库
            methods.queryQuestion = function (filter, loading) {
                return $rootScope._request("GetTiKuByCondition", filter, loading);
            };

            //激活题库
            methods.activeQuestion = function (arr) {
                var formData = {
                    List: []
                };
                for (var i = 0; i < arr.length; i++) {
                    formData.List.push({SysNo: arr[i]});
                }
                return $rootScope._request("ActiveTiKu",formData,true);
            };
            //删除题库
            methods.deleteQuestion=function(arr){
                var formData = {
                    List: []
                };
                for (var i = 0; i < arr.length; i++) {
                    formData.List.push({SysNo: arr[i]});
                }
                return $rootScope._request("DeleteTiKu",formData,true);
            };

            return methods;
        }]);
});