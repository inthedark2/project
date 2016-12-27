'use strict';

// Define the `memoryApp` module
angular.module('memoryGame', ['ngRoute','memoryList','oauth']);

angular.module('memoryGame').config(['$routeProvider', '$locationProvider',
    function($routeProvider, $locationProvider) {
        $locationProvider.hashPrefix('!');
        $routeProvider.caseInsensitiveMatch = true;

        $routeProvider
            .when('/', {
                template: '<memory-list></memory-list>'
            })
            .when('/oauth/', {
                template: '<oauth></oauth>'
            })
            .when('/:id', {
                    template: '<memory-list></memory-list>'
                })

    }]);