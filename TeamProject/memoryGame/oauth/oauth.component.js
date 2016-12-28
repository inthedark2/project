'use strict';

angular.
  module('oauth').
  component('oauth', {
    template:'<div></div>',
    controller: ['memoryService','$scope',function oauthController(memoryService,$scope,$cordovaOauth) {
      console.log('I init');
      $scope.facebookLogin = function() {
        $cordovaOauth.facebook("686306914879504", ["wiz.sturtup@gmail.com"]).then(function(result) {
          console.log('I autent');
        }, function(error) {
          console.log('error autent');
        });
      }

    }]
  });
