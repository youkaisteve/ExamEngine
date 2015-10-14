define(["app"], function (app) {
    app.factory("Process", ["$rootScope", function ($rootScope) {
        //var methods = {};

        //for(var conf in config){
        //    methods[conf]=$rootScope._generateR(config[conf])
        //}

        //methods.getAllProcess = function (loading) {
        //    return $rootScope._request("AllProcess", null, loading);
        //};
        //
        //methods.getProcessImage = function (definedName, loading) {
        //    return $rootScope._request("ProcessImage", {
        //        DefineName: definedName
        //    }, loading);
        //};
        //
        //methods.getTokenImage = function (model, loading) {
        //    return $rootScope._request("TokenImage", model, loading);
        //};
        //
        //methods.convertToKV = function (allProcess) {
        //    var result = [];
        //    angular.forEach(allProcess, function (ele) {
        //        result.push({
        //            text: ele.ProcessName
        //            , value: ele
        //        })
        //    });
        //    return result;
        //};
        //
        //methods.getUnfinshedTask = function (filter, loading) {
        //    return $rootScope._request("UnFinishProcess", filter, loading);
        //};
        //
        //methods.doneAllProcess = function (loading) {
        //    return $rootScope._request("TerminateAllUnFinishProcess", null, loading);
        //};

        return $rootScope._generateRest({
            getAllProcess: {
                url: "AllProcess"
                , method: "POST"
            }
            , getProcessImage: {
                url: "ProcessImage"
                , method: "POST"
            }
            , getTokenImage: {
                url: "TokenImage"
                , method: "POST"
            }
            , getUnfinshedTask: {
                url: "UnFinishProcess"
                , method: "POST"
            }
            , doneAllProcess: {
                url: "TerminateAllUnFinishProcess"
                , method: "POST"
            }
        });
    }]);
});