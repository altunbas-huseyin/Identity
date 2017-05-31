'use strict';
app.service('permissionService', ['$http', function ($http, $location) {

    var permissionService = {};

    var _get = function () {

        var config = {
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'Token': Token
            }
        };

        var result = $http.get(apiUrl + 'api/Permission', config).success(function (response) {
            console.log(response.status);

        }).error(function (err, status) {

        });

        return result;

    };

    
    var _add = function (data) {

       
        var result = $http.post(apiUrl + 'api/Permission', JSON.stringify(data),
            {
                headers: { 'Content-Type': 'application/json; charset=utf-8', 'Token': Token }

            }).success(function (response) {

                console.log(response.status);

            }).error(function (err, status) {

            });


        return result;

    };

    var _update = function (data) {

       
        var result = $http.put(apiUrl + 'api/Permission', JSON.stringify(data),
            {
                headers: { 'Content-Type': 'application/json; charset=utf-8', 'Token': Token }

            }).success(function (response) {

                console.log(response.status);

            }).error(function (err, status) {

            });


        return result;

    };

   var _destroy = function (data) {

       
       var config = {
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'api-version': 1,
                'Token': Token
            }
        };

        var result = $http.delete(apiUrl + 'api/Permission/', config).success(function (response) {
            console.log(response.status);

        }).error(function (err, status) {

        });

        return result;

    };




    permissionService.get = _get;
    permissionService.add = _add;
    permissionService.update = _update;
    permissionService.destroy = _destroy;
    return permissionService;
}]);
