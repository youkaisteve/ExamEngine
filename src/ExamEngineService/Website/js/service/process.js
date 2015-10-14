define(["app"], function (app) {
    app.factory("Process", ["$rootScope", function ($rootScope) {
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
        },{
            convertToKV:function(allProcess){
                var result = [];
                angular.forEach(allProcess, function (ele) {
                    result.push({
                        text: ele.ProcessName
                        , value: ele
                    })
                });
                return result;
            }
        });
    }]);
});