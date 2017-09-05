/// <reference path="../angular.js" />
var stucontrollers = angular.module("stucontrollers", ["ServiceModule"]);

// Before service: "ServiceModule > myservice" was created
// -------------------------------------------------------
//stucontrollers.controller("GetStudentsList",
//    function GetStudentsList($scope, $http) {
//        $http.get("/api/students").success(function (data) {
//            $scope.students = data;
//        });
//    });

// After service: "ServiceModule > myservice" was created
// ------------------------------------------------------
stucontrollers.controller("GetStudentsList", function GetStudentsList($scope, myservice) {

    loadRecords();

    function loadRecords() {

        var promiseGet = myservice.getStudents();
        promiseGet.then(function (response) { $scope.students = response.data },
            function (errorResponse) {
                $log.error("Failure loading Students", errorResponse);
            });
    }

});


//// AngularJS 1.6.4
//// ---------------
//var stucontrollers = angular.module("stucontrollers", []);

//stucontrollers.controller("GetStudentsList",
//    function GetStudentsList($scope, $http) {
//        $http.get("/api/students")
//            .then(function(response) {
//                $scope.students = response.data;

//            });
//    });

stucontrollers.controller("AddStudent", function ($scope, $http, $location) {

    $scope.student = {
        "FirstName": $scope.FirstName,
        "LastName": $scope.LastName,
        "Age": $scope.Age,
        "Gender": "Male"
    };
    $scope.genders = ['Male', 'Female', 'Other'];
    $scope.pattern = /^\s*\d+\s*$/;


    $scope.Add = function () {
        $http({ method: "POST", data: $scope.student, url: "/api/students" }).success(
            function (data, status, headers, config) {

                alert("Student Added Successfully !!! ");

                $scope.AddStu.$setPristine(); // AddStu is a form name

                $scope.student = {
                    "Gender": "Male"
                };

                $location.path('/AllStu');
            }).error(function () {

                alert("Something went wrong!");

            });
    }
});

//stucontrollers.controller("xxx",
//    function ($scope, $http, $location, $timeout, getStuToEdit, $route) {


//        get();
//        function get () {
//            $scope.studentToEdit = getStuToEdit.getModel();

//        }




//    });

// AllStudents Controller
stucontrollers.controller("AllStudents",
    function ($scope, $http, $location, $timeout, getStuToEdit, $route) {

        // Get All Students
        alert("Hi");
        $scope.getAll = function () {

            $http({ method: "GET", url: "/api/students" }).success(function (data, status, headers, config) {
                $scope.students = data;
                $scope.count = data.length;

            }).error(function () {

                alert("Something went wrong!");
            });
        }

        $scope.getAll();


        //---start

        //$scope.set = function() {
        //    getStuToEdit.setModel("halo");
        //}
        //$scope.set();

        //$scope.get = function () {
        //    $scope.alooo = getStuToEdit.getModel();
        //}
        //$scope.get();


        //$scope.get = function () {
        //    $scope.studentToEdit = getStuToEdit.getModel();
        //    //$route.reload();

        //}

        //$scope.get();

        //$scope.Edito = function (id) {

        //    //$scope.studentToEdit = getStuToEdit.setModel(id);
        //    getStuToEdit.setModel(id);
        //    // $scope.get();

        //    //var promiseGet = getStuToEdit.setModel(id);
        //    //promiseGet.then(function (response) {
        //    //    $scope.studentToEdit = response.data;

        //    //},
        //    //    function (errorResponse) {
        //    //        $log.error("Failure loading Students", errorResponse);
        //    //    });

        //    //$route.reload();

        //    // $location.path('/EditStu');
        //}


        //----end


        // Remove the Student
        $scope.Remove = function (Id) {

            if (confirm("Do you really want to delete this student?") == true) {

                $http({ method: "DELETE", url: "/api/students/" + Id }).success(function () {


                    $scope.status = "Deleted seccessfully!";
                    $scope.statuscolor = "#28b62c";
                    $scope.showStatus = true;

                    $scope.getAll();

                }).error(function () {

                    $scope.status = "Something went wrong!";
                    $scope.statuscolor = '#f70404';
                    $scope.showStatus = true;
                }
                );

                $timeout(function () {
                    $scope.showStatus = false;
                },
                    3000);

            } else {
                return false;
            }
        };


        //$scope.studentToEdit = {
        //    "Id": $scope.Id,
        //    "FirstName": $scope.FirstName,
        //    "LastName": $scope.LastName,
        //    "Age": $scope.Age,
        //    "Gender": $scope.Gender
        //};

        //$scope.genders = ['Male', 'Female', 'Other'];
        //$scope.pattern = /^\s*\d+\s*$/;

        //--------------
        //Edit student (GET to update)
        $scope.Edit = function (Id) {

            var jumbotron = document.getElementById("jumbotron");
            var editForm = document.getElementById("editForm");

            //jumbotron.style.visibility = 'hidden';
            jumbotron.style.display = 'none';
            editForm.style.display = 'block';

            //$scope.studentToEdit = {
            //    "Id": $scope.Id,
            //    "FirstName": $scope.FirstName,
            //    "LastName": $scope.LastName,
            //    "Age": $scope.Age,
            //    "Gender": $scope.Gender
            //};

            $scope.genders = ['Male', 'Female', 'Other'];
            $scope.pattern = /^\s*\d+\s*$/;

            $http({ method: "GET", url: "/api/students/" + Id }).success(function (data) {
                //alert(data.FirstName);
                $scope.studentToEdit = data;
                //$scope.$apply();
                $location.path('/EditStu');

            });

        };


        //-------Test start-------

        $scope.goto = function (path) {

            var jumbotron = document.getElementById("jumbotron");
            var editForm = document.getElementById("editForm");

            editForm.style.display = 'none';
            jumbotron.style.display = 'block';


            //$location.path(path);
            //$route.reload();
            //$window.history.back();
        };


        //-------Test end-------

        //Update student 
        $scope.Update = function () {
            $http({ method: "PUT", data: $scope.studentToEdit, url: "/api/students/" + $scope.studentToEdit.Id })
                .success(
                    function (data, status, headers, config) {

                        $scope.status = "Updated seccessfully!";
                        $scope.statuscolor = "#28b62c";
                        $scope.showStatus = true;

                        $scope.studentToEdit = {};
                        $scope.EditStu.$setPristine(); // EditStu is a form name

                        var jumbotron = document.getElementById("jumbotron");
                        var editForm = document.getElementById("editForm");

                        editForm.style.display = 'none';
                        jumbotron.style.display = 'block';

                        $scope.getAll();


                    }).error(function () {

                        $scope.status = "Something went wrong!";
                        $scope.statuscolor = '#f70404';
                        $scope.showStatus = true;
                    });


            $timeout(function () {
                $scope.showStatus = false;
            },
                3000);

            $location.path('/AllStu');

        }

        $scope.ExactMatchNotification = function (state) {
            if (state == true) {
                alert("Exact Match is on");
            } else {
                alert("Exact Match is off");
            }
        }

        //$scope.getInfo = function (item) {

        //    if ($scope.stu == undefined) {
        //        return true;
        //    } else {
        //        if (item.FirstName.toLowerCase().indexOf($scope.studentToEdit.toLowerCase()) != -1);
        //        {
        //            return true;
        //        }

        //    }

        //    return false;

        //};


    });