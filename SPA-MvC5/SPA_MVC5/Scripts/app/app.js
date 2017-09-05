/// <reference path="../angular.js" />
var module = angular.module("StudentApp", ["ngRoute", "stucontrollers", "sturesources"]);

module.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Home',
        {
            templateUrl: 'templates/Home.html',
            controller: 'GetStudentsList'
        }).
        when('/AddStu',
            {
                templateUrl: 'templates/AddStudent.html',
                controller: 'AddStudent'
                
            }).
        when('/EditStu',
            {
                templateUrl: 'templates/EditStudent.html',
                controller: 'EditStudent'
            }).
        when('/AllStu',
            {
                templateUrl: 'templates/AllStudents.html',
                controller: 'AllStudents'
            }).
        otherwise({
            redirectTo:
                '/Home'
        });
}]);



