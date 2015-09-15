/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
define(["app"], function (app) {

    app.controller("query_score", ["$scope", "$window",
        function ($scope, $window) {

            $scope.scores=[];

            function compareObject(source, target) {
                //var result = true;
                var count = 0;
                var errorCount = 0;

                function getTargetValue(data, path) {
                    if (!data) data = target;
                    if (path.length > 0) {
                        var name = path.shift();
                        if (data[name] instanceof Object) {
                            return getTargetValue(data[name], path);
                        }
                        else {
                            return data[name];
                        }
                    }
                    else {
                        return null;
                    }
                }

                function compare(obj, path) {
                    for (var name in obj) {
                        path.push(name);
                        if (obj[name] instanceof Object) {
                            compare(obj[name], path);
                        }
                        else {
                            count++;
                            if (obj[name] != getTargetValue(target, angular.copy(path))) {
                                errorCount++;
                            }
                        }
                        path.pop();
                    }
                }

                compare(source, []);
                return {
                    count: count
                    , errorCount: errorCount
                };
            }

            $scope._request("Score").then(function (res) {
                if(res.Data) {
                    var standarAnswers = res.Data.StandarAnswers;
                    var students = res.Data.UserAnswers;

                    function getStandarAnswer(formName) {
                        for (var i = 0; i < standarAnswers.length; i++) {
                            if (formName == standarAnswers[i].TemplateName) {
                                return standarAnswers[i].TemplateData;
                            }
                        }
                        return null;
                    }

                    angular.forEach(students, function (ele) {
                        ele.Count = 0;
                        ele.ErrorCount = 0;
                        angular.forEach(ele.Answer, function (el) {
                            var standar = getStandarAnswer(el.TemplateName);
                            if (standar) {
                                var result = compareObject(standar, el.TemplateData);
                                ele.Count += result.count;
                                ele.ErrorCount += result.errorCount;
                            }
                        });
                    });

                    $scope.scores = students;
                }

            });
        }]);
});