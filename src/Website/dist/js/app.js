"use strict";define(["angularAMD","app.config","root_scope"],function(r,e,t){function o(r,t,o){if(!t){if(!e.controllerPath)throw new Error("controllerPath is not defined");var l=r.lastIndexOf(".");t=e.controllerPath+r.slice(0,l)}if(!e.templatePath)throw new Error("templatePath is not defined");r=e.templatePath+r;var n={templateUrl:r,controllerUrl:t};if(!o){var a=t.lastIndexOf("/");o=t.slice(a+1)}return n.controller=o,n}var l=angular.module(e.appName,e.appDeps);return l.config(["$routeProvider","$httpProvider","$logProvider",function(t,l,n){for(var a,i=0;i<e.route.length;i++)a=e.route[i],t.when(a.url,r.route(o(a.templateUrl,a.controllerUrl,a.controller)));e.start&&""!==e.start&&t.otherwise({redirectTo:e.start}),n.debugEnabled("develop"===e.environment)}]),l.run(t),r.bootstrap(l)});