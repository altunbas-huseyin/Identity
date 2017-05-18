

//angular.module('myApp', []).controller('loginCtrl', function ($scope, testService) {
//    $scope.Name = "Hüseyin";
//    $scope.Surname = "Altunbaş";
//
//    $scope.login = function () {
//        alert($scope.UserName);
//        alert($scope.Password)
//    };
//});


app.controller("loginCtrl", function ($scope, authService) {
    $scope.Name = "Hüseyin";
    $scope.Surname = "Altunbaş";
    //var t = loginService.login("rrr", "ttt");
    
    

    $scope.login = function () {
        
        //loginService.login("22222", "eeeeee", function (data) {
        //    $scope.venues = data.response;
        //    console.log(data[0]);
        //    console.log("iiii");
        //    alert(data.response);
        //})

        
        $scope.loginData = {
            userName: "Gokhan",
            password: "123456"
        };

        $scope.message = "";

        $scope.login = function () {

            authService.login($scope.loginData).then(function (response) {

                //$location.path('/orders');
                console.log(response);

            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };


    };
});