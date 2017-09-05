/// <reference path="../angular.js" />
var sturesources = angular.module("sturesources", ['ngResource']);

sturesources.factory("Studs", function Studs($resource) {
    return $resource('/api/students', {},
        {
            query: { method: 'GET', params:{}, isArray: true}
        });
});