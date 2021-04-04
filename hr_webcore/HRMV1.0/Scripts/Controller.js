app.controller("myCntrl", function ($scope, myService) {

    GetAllEmployees();

    //For sorting according to columns
    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;
        $scope.reverse = !$scope.reverse;
    }

    //To Get All Records  
    function GetAllEmployees() {
        var getData = myService.getEmployees();

        getData.then(function (emp) {
            console.log(emp);
            $scope.employees = emp.data;
        }, function (emp) {
            alert("Records gathering failed!");
        });
    }

    $scope.editEmployee = function (employee) {
        GetDesignations();
        GetDepartments();
        var getData = myService.getEmployee(employee.Id);
        getData.then(function (emp) {
            $scope.employee = emp.data;
            $scope.employeeId = employee.Id;
            $scope.employeeName = employee.name;
            $scope.employeeDOB = new Date(employee.DOB);
            $scope.employeeGender = employee.Gender;
            $scope.employeeEmail = employee.Email;
            $scope.employeeMobile = employee.Mobile;
            $scope.employeeAddress = employee.Address;
            $scope.employeeJoiningDate = employee.JoiningDate;
            $scope.employeeDepartment = employee.DepartmentID;
            $scope.employeeDesignation = employee.DesignationID;
            $scope.Action = "Edit";       
            $("#myModal").modal('show');
        },
        function (msg) {
            $("#alertModal").modal('show');
            $scope.msg = msg.data;
        });
    }

    $scope.AddUpdateEmployee = function () {
        var Employee = {
            Name: $scope.employeeName,
            DOB: $scope.employeeDOB,
            Gender: $scope.employeeGender,
            Email: $scope.employeeEmail,
            Mobile: $scope.employeeMobile,
            Address: $scope.employeeAddress,
            JoiningDate: $scope.employeeJoiningDate,
            DepartmentID: $scope.employeeDepartment,
            DesignationID: $scope.employeeDesignation
        };

        var getAction = $scope.Action;

        if (getAction == "Edit") {
            Employee.Id = $scope.employeeId;
            var getData = myService.updateEmp(Employee);
            getData.then(function (msg) {
                GetAllEmployees();
                ClearFields();
                $("#alertModal").modal('show');
                $scope.msg = msg.data;
            }, function (msg) {
                $("#alertModal").modal('show');
                $scope.msg = msg.data;
            });
        }
        else {
            var getData = myService.AddEmp(Employee);
            getData.then(function (msg) {
                GetAllEmployee();
                $("#alertModal").modal('show');
                $scope.msg = msg.data;
                ClearFields();

            }, function (msg) {
                $("#alertModal").modal('show');
                $scope.msg = msg.data;
            });
        }
        GetAllEmployee();
        $scope.refresh();
    }

    $scope.AddEmployeeDiv = function () {
        ClearFields();
        $scope.Action = "Add";
        GetDepartments();
        GetDesignations();       
    }

    function GetDesignations() {
        var desg = myService.GetDesignations();

        desg.then(function (dsg) {
            $scope.designations = dsg.data;
        }, function (dsg) {
            $("#alertModal").modal('show');
            $scope.msg = "Error in filling designation drop down !";
        });
    }

    function GetDepartments() {
        var departments = myService.GetDepartment();

        departments.then(function (dep) {
            $scope.departments = dep.data;
        }, function (dep) {
            $("#alertModal").modal('show');
            $scope.msg = "Error in filling departments drop down !";
        });
    }

    $scope.alertmsg = function () {
        $("#alertModal").modal('hide');
    };

    function ClearFields() {
        $scope.employeeId = "";
        $scope.employeeName = "";
        $scope.employeeDOB = "";
        $scope.employeeGender = "";
        $scope.employeeEmail = "";
        $scope.employeeMobile = "";
        $scope.employeeAddress = "";
        $scope.employeeJoiningDate = "";
        $scope.employeeDepartment = "";
        $scope.employeeDesignation = "";
    }
});