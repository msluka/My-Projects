var servicemodule = angular.module("ServiceModule", []);

servicemodule.service('myservice',
    function ($http) {

        //Get all students

        this.getStudents = function () {
            return $http.get("/api/Students");
        };

    });

servicemodule.service('getstudentbyid', function ($http) {
    var model = {};
    var _index = 0;
    this.setModel = function (id, index) {
        model = $http.get("/api/Students/" + id);
        _index = index;
        return $http.get("/api/Students/" + id);

        //    .success(function (response) {
        //    model = response;
        //});
        // return $http.get("/api/Students/" + id);

    };

    this.getModel = function () {
        return model;
    }
    this.getIndex = function () {
        return _index;
    }

    

});






