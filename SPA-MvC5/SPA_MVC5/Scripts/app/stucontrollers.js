///<reference path="../angular.js" />
var stucontrollers = angular.module("stucontrollers", ["ServiceModule"]);

// For Home Page
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


// For Add Student Page
stucontrollers.controller("AddStudent", function ($scope, $http, $location) {

    $scope.student = {
        "FirstName": $scope.FirstName,
        "LastName": $scope.LastName,
        "Age": $scope.Age,
        "Gender": "Male",
        "Status": "Unread"
    };
    $scope.genders = ['Male', 'Female', 'Other'];
    $scope.pattern = /^\s*\d+\s*$/;
    $scope.statuses = ['Read', 'Unread'];


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



// For All Students Page
stucontrollers.controller("AllStudents", function ($scope, $http, $timeout, getstudentbyid, $location) {

    // Get All Students
    
    $scope.getAll = function () {

        $http({ method: "GET", url: "/api/students" }).success(function (data, status, headers, config) {
            $scope.students = data;
            $scope.count = data.length;

        }).error(function () {

            alert("Something went wrong!");
        });
    }

    $scope.getAll();

    // Exact Match Function
    $scope.ExactMatchNotification = function (state) {
        if (state == true) {
            alert("Exact Match is on");
        } else {
            alert("Exact Match is off");
        }
    }

    // Remove the Student
    $scope.Remove = function (Id) {

        $scope.id = Id;

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

    // Get Student by ID

    $scope.GetById = function (id, index) {

        getstudentbyid.setModel(id, index).then(function() {
          
            $location.path('/EditStu');

            },
        function (errorResponse) {
            $log.error("Failure loading Students", errorResponse);
        });
        
    }
    

});

// Edit Controller
stucontrollers.controller("EditStudent",
    function ($scope, $http, $timeout, getstudentbyid, $window, $location, $route) {

        $scope.studentToEdit = getstudentbyid.getModel().then(function (response) {
           
            $scope.studentToEdit = response.data;
         
        });


        $scope.index = getstudentbyid.getIndex();
        
       
        $scope.goto = function () {

            $location.path('AllStu');

        };


        $scope.studentToEdit = {
            "FirstName": $scope.FirstName,
            "LastName": $scope.LastName,
            "Age": $scope.Age,
            "Gender": $scope.Gender,
            "Status": $scope.ReadStatus
        };
        $scope.genders = ['Male', 'Female', 'Other'];
        $scope.statuses = ['Read', 'Unread'];
        $scope.pattern = /^\s*\d+\s*$/;
       


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

                        //var jumbotron = document.getElementById("jumbotron");
                        //var editForm = document.getElementById("editForm");

                        //editForm.style.display = 'none';
                        //jumbotron.style.display = 'block';

                        //$scope.getAll();
                        $location.path('/AllStu');


                    }).error(function () {
                        alert("Something went wrong!");
                    $scope.status = "Something went wrong!";
                    $scope.statuscolor = '#f70404';
                    $scope.showStatus = true;
                });


            $timeout(function () {
                    $scope.showStatus = false;
                },
                3000);
            
        }


    });