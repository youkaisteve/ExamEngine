define(["app"],function(n){n.controller("query_score",["$scope","$window",function(n,r){function e(n,r){function e(n,t){if(n||(n=r),t.length>0){var a=t.shift();return n[a]instanceof Object?e(n[a],t):n[a]}return null}function t(n,u){for(var c in n)u.push(c),n[c]instanceof Object?t(n[c],u):(a++,n[c]!=e(r,angular.copy(u))&&o++),u.pop()}var a=0,o=0;return t(n,[]),{count:a,errorCount:o}}n.scores=[],n._request("Score",null,!0).then(function(r){function t(n){for(var r=0;r<a.length;r++)if(n==a[r].TemplateName)return a[r].TemplateData;return null}if(r.Data){var a=r.Data.StandardAnswers,o=r.Data.UserAnswers;angular.forEach(o,function(n){n.Count=0,n.ErrorCount=0,angular.forEach(n.User.Answer,function(r){var a=t(r.TemplateName);if(a){var o=e(JSON.parse(a),JSON.parse(r.TemplateData));n.Count+=o.count,n.ErrorCount+=o.errorCount}})}),n.scores=o}})}])});