'use strict';

// Register `memoryList` component, along with its associated controller and template
angular.
  module('memoryList').
  component('memoryList', {
    //template: '<h2>{{$ctrl.count}}</h2><div ng-repeat="n in $ctrl.arr track by $index "><memory-detail></memory-detail></div>' ,
    //templateUrl: 'components/event/event-list/event-list.template.html'
    //templateUrl:'memory-list/memory-list.template.html',
    template:'<div class="page">    <div class="name">    <h1>Memory <span class="red">Game</span></h1>    <img class="logo" src="img/shell.png">    </div>    <div ng-hide="game" class="game">    <div class="clears text">    <h2>Attempts : <span class="attempts">{{memorys.clickCount}} </span> <span class="red">Open: {{memorys.matchesCells}}</span></h2></div><div class="field row">    <div class="center-block">    <div class="square" ng-repeat="n in $ctrl.arr track by $index">    <memory-detail></memory-detail>    </div>    </div>    </div>    </div>    <div ng-hide="winers" class="row text winers">    <div>    <h2>You win !!!</h2></div><div class="field row clears">    <div class="center-block">    <h2>Attempts : <span class="attempts">{{memorys.clickCount}} </span> Open: {{memorys.matchesCells}}</h2></div></div><div class="field row clears">    <h2>Congratulations !!!</h2></div><a href="#!/{{winer}}">    <button> Start new Game</button></a>',
    controller: ['memoryService','$scope',function GreetUserController(memoryService,$scope) {
      memoryService.initGame() ;

      this.arr=memoryService.arrGame ;
      $scope.game = false ;
      $scope.winers = true ;
      $scope.memorys = memoryService ;

      this.win = function(winCount) {
        console.log('win') ;
        $scope.game = true ;
        $scope.winers = false ;
        $scope.winer = winCount ;
      }

      this.start = function() {
        memoryService.initGame() ;
        this.arr=memoryService.arrGame ;
        $scope.game = false ;
        $scope.winers = true ;
      }

      memoryService.tellWin = this.win ;

    }]
  });
