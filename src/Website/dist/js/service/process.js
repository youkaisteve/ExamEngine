define(["app"],function(e){e.factory("Process",["$rootScope",function(e){var n={};return n.getAllProcess=function(n){return e._request("AllProcess",null,n)},n.getProcessImage=function(n,r){return e._request("ProcessImage",{DefineName:n},r)},n.getTokenImage=function(n,r){return e._request("TokenImage",n,r)},n.convertToKV=function(e){var n=[];return angular.forEach(e,function(e){n.push({text:e.ProcessName,value:e})}),n},n}])});