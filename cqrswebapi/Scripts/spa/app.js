angular.module("readApp", ['ngRoute'])
       .config(configure);

configure.$inject = ['$routeProvider'];

function configure($routeProvider) {
    $routeProvider.when('/app/home',
        {
            templateUrl: 'Scripts/spa/home.html',
            controller: 'homeController'
        })
        .otherwise({
            redirectTo: '/app/home', controller: 'homeController'
        });

}