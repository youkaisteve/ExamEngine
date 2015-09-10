/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app"], function (app) {

    app.controller("login", ["$scope","$window", function ($scope,$window) {

        $scope.login = function ($event, model) {
            return $scope._request("Login", model).success(function (res) {
                //location to default page when login success
                if(res.Code==0){
                    //TODO save auth info when login success
                    //auth info contain Role,UserId
                    $scope._auth(res.Data);
                    $scope._goto("/default");
                }
                else{
                    $window.alert(res.ErrorMessage);
                }
            });
        }
    }]);

});