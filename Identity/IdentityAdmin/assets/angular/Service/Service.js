﻿'use strict';
app.factory('authService', ['$http',  function ($http) {

   
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: ""
    };

  

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        

      var result =  $http.post(apiUrl + 'Token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

          //  localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });

            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;

        }).error(function (err, status) {

        });

      return result;

    };

    
    authServiceFactory.login = _login;

    return authServiceFactory;
}]);