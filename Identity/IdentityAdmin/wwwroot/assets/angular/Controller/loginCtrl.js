﻿

app.controller("loginCtrl", function ($scope, authService) {

    authService.isLoginByRedirect();
    $scope.Name = "Hüseyin";
    $scope.Surname = "Altunbaş";
    //var t = loginService.login("rrr", "ttt");
    //console.log(authService.getUser());
    //console.log(authService.getToken());
    //alert(authService.isLogin());

    $scope.login = function () {
        
        $scope.message = "";

        $scope.login = function () {

            $scope.loginData = {
                Email: $scope.Email,
                Password: $scope.Password
            };

            authService.login($scope.loginData).then(function (response) {
             
                $location.path('/');
                //console.log(response);

            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };


    };
});