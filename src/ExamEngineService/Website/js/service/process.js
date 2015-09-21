define(["app"], function (app) {
    app.factory("Process", ["$rootScope", function ($rootScope) {
        var methods = {};

        methods.getAllProcess = function (loading) {
            return $rootScope._request("AllProcess", null, loading);
        };

        methods.getProcessImage = function (definedName, loading) {
            return $rootScope._request("ProcessImage", {
                DefineName: definedName
            }, loading);
        };

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

        return methods;
    }]);
});