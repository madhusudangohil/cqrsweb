(function () {
    'use strict';
    angular.module('readApp').service('homeService', homeService);

    homeService.$inject = ['$http'];

    function homeService($http) {

        this.getTvState = function(id) {
            return $http.get("http://localhost:20082/api/HomeApi/" + id)
                    .then(function (response) {
                        return response.data;
                    })
                    .catch(function (error) {
                        throw error;
                });
        };

        this.RegisterTelevision = function () {
            return $http.post("http://localhost:6743/api/Television/Register")
                    .then(function (response) {
                        return response.data;
                    })
                    .catch(function (error) {
                        throw error;
                    });
        };

        this.getAllTvs = function () {
            return $http.get("http://localhost:20082/api/HomeApi/" )
                    .then(function (response) {
                        return response.data;
                    })
                    .catch(function (error) {
                        throw error;
                    });
        };

        this.PowerOn = function (tv) {
            return $http({
                url: "http://localhost:6743/api/Television/PowerOn",
                method: "POST",
                data: JSON.stringify(tv)
                })
                    .then(function (response) {
                        return response.data;
                    })
                    .catch(function (error) {
                        throw error;
                    });
        };

        this.PowerOff = function (tv) {
            return $http({
                url: "http://localhost:6743/api/Television/PowerOff",
                method: "POST",
                data: JSON.stringify(tv)
            })
                    .then(function (response) {
                        return response.data;
                    })
                    .catch(function (error) {
                        throw error;
                    });
        };

        this.ChangeChannel = function (tv) {
            return $http({
                url: "http://localhost:6743/api/Television/ChangeChannel",
                method: "POST",
                data: JSON.stringify(tv)
            })
                    .then(function (response) {
                        return response.data;
                    })
                    .catch(function (error) {
                        throw error;
                    });
        };
    }
})();