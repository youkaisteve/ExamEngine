"use strict";define(["app.config"],function(e){return["$rootScope","$http","$sessionStorage","$window","$q",function(n,o,t,r,i){function u(){n._goto("#/login")}function a(){t.$reset(),n.userName=null}n.sessionStorage=t,n.fixedFooter=!1,n.environment=e.environment,n._AUTH_KEY="auth",n._goto=function(e){0===e.indexOf("#")?window.location.href=e:window.location.hash=e},n._auth=function(e){return e?(n.userName=e.UserName,void(t[n._AUTH_KEY]=e)):t[n._AUTH_KEY]};var s=n._auth();s&&(n.userName=s.UserName),n.$on("$routeChangeStart",function(e,o,t){n._auth()||u()}),n._request=function(u,s,f){var c,d=i.defer();return f&&(c=n._notify("加载中...")),o.post(e.api,s,{headers:{action:u,"user-authorize":t.token}}).success(function(e,o,i){0===e.Code?(t.token||(t.token=i("user-authorize")),d.resolve(e)):3===e.Code?(a(),r.alert(e.ErrorMessage),n._goto("/login")):(r.alert(e.ErrorMessage),d.reject(e))}).error(function(){r.alert("系统错误,请联系管理员")})["finally"](function(){c&&c.remove()}),d.promise},n._logout=function(){return n._request("Logout",{}).then(function(e){a(),u()})},n._notify=function(e,n){n||(n="alert-warning");var o=$("<div>").addClass("notify alert").addClass(n).text(e);return o.appendTo(document.body),o},n.getRows=function(e){var n=[];if(e)for(var o in e)n.push(n.length);else n=[0];return n}}]});