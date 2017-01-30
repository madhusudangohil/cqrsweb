(function() {
    'use strict';
    angular.module('readApp').controller('homeController', homeController);

    homeController.$inject = ['$scope', 'homeService'];

    function homeController($scope, homeService) {
        $scope.hello = function() {
            $scope.greeting = "hello";
        }

        $scope.InitalizeTelevision = function (id) {
            homeService
                .getTvState(id)
                .then(function(data) {
                    $scope.Television = data;
                });
        }

        $scope.RegisterTelevision = function() {
            homeService
                .RegisterTelevision()
                .then(function (data) {
                    $scope.Message = data;
                    homeService.getAllTvs()
                        .then(function(result) {
                            $scope.tvs = result;
                        });
                });
        }

        $scope.GetAllTelevisions = function () {
            homeService
                .getAllTvs()
                .then(function (data) {
                    $scope.tvs = data;
                });
        }

        $scope.PowerOn = function(tv) {
            homeService.PowerOn(tv)
                .then(function(data) {
                    homeService
                        .getTvState(tv.Id)
                        .then(function(result) {
                            $scope.Television = result;
                        });
                }).catch(function(error) {
                    
                });
        }
        
        $scope.PowerOff = function (tv) {
            homeService.PowerOff(tv)
                .then(function (data) {
                    homeService
                        .getTvState(tv.Id)
                        .then(function (result) {
                            $scope.Television = result;
                        });
                }).catch(function (error) {

                });
        }


        $scope.ChannelUp = function (tv) {
            tv.Channel = tv.ToChannel + 1;
            homeService.ChangeChannel(tv)
                .then(function (data) {
                    homeService
                        .getTvState(tv.Id)
                        .then(function (result) {
                            $scope.Television = result;
                        });
                }).catch(function (error) {

                });
        }

        $scope.ChannelDown = function (tv) {
            tv.Channel = tv.ToChannel - 1;
            homeService.ChangeChannel(tv)
                .then(function (data) {
                    homeService
                        .getTvState(tv.Id)
                        .then(function (result) {
                            $scope.Television = result;
                        });
                }).catch(function (error) {

                });
        }
    }
})();