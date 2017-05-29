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



    permissionService.get = _get;
    return permissionService;
}]);
