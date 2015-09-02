/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
"use strict";

define(["app.config"],function (config) {

    function extendRootScope($rootScope, $http) {
        $rootScope._request = function (action, data) {
            return $http.post(config.api, {
                Action: action,
                Params: data
            });
        };
    }

    return extendRootScope;

});