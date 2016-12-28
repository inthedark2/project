'use strict';

angular.
  module('memoryDetail').
  component('memoryDetail', {
    //templateUrl:'memory-detail/memory-detail.template.html',
    //templateUrl:'memdetail.html',
    template:'<div class="rotor"  ng-click="$ctrl.click()">    <img ng-class="curr" src="{{imgFront}}">    <img ng-class="next" src="{{imgBack}}">   </div>',
    controller: ['memoryService','$scope',function MemoryDetailController(memoryService,$scope) {
      //console.log('I init') ;
      var memory = memoryService ;
      var iAm = this ;
      this.unknown = true ;
      this.name = memory.initCells.length ;
      memory.initCells.push(this) ;
//      $scope.imgFront = "img/not-pix.jpg" ;

      $scope.curr = 'rotor-flip' ;
      $scope.next = ['rotor-flip','next'] ;

      this.init = function() {
        $scope.imgFront = "img/not-pix.jpg" ;
        iAm.unknown = true ;
      }

      this.init() ;

      var closeCell = function (){
        if (this.unknown == true) $scope.imgBack  = "img/not-pix.jpg" ;
        else $scope.imgFront = "img/yes.jpg" ;

        $scope.curr = 'rotor-flip' ;
        $scope.next = 'rotor-flip next';

        $scope.$apply() ;
        //console.log('Close') ;
      }

      this.close = closeCell ;

      var open = function (){

          $scope.curr = ['rotor-flip', 'flipped'];
          $scope.next = 'rotor-flip';

        if (memory.openCells.length<2)
        {
          memory.openCells.push(iAm)  ;
          $scope.imgBack = "img/pix" + memory.arrGame[iAm.name] + ".jpg" ;
        }
        if (memory.openCells.length==2) {
          //console.log('Close start') ;
          setTimeout(memory.closeCells, 1000);
        }
      }

      this.click = function(){
        //console.log(this.unknown) ;
        if (memory.openCells.length>=2) return ;
        if (this.unknown) open() ;
      }
    }]
  });
