'use strict';

angular.module('memoryGame').factory('memoryService', function() {
  var iAm = this ;
  iAm.winer = 0 ;
  iAm.initCells = [] ;
  iAm.openCells = [] ;
  iAm.forWin = 6 ; //что бы выграть нужно кол ячеек

  function getRandom(n) //где число 0 до n
  {
    return Math.floor(Math.random() * (n + 1));
  }

  iAm.initGame = function() {
    console.log('start! start!') ;
    console.log(iAm.initCells) ;
   for (var j=0;j<iAm.initCells.length;j++){
     console.log(iAm.initCells[j]) ;
     console.log(iAm.initCells[j].unknown) ;
      iAm.initCells[j].init() ;
     console.log(iAm.initCells[j].unknown) ;
    } ;/* */

    iAm.initCells = [] ;
    iAm.openCells = [] ;
    iAm.arrGame = [] ;
    iAm.clickCount = 0 ;
    iAm.matchesCells = 0 ;
    var arrInd = [] ;
    var arrVal = [] ;
    var i = 0 ;

    for (i=0;i<iAm.forWin;i++) { arrInd.push(i) ; arrVal.push(2) ; } ;

    iAm.arrGame = [];

    while(arrVal.length>0) {
      i=getRandom(arrVal.length-1) ;
      iAm.arrGame.push(arrInd[i]) ;
      if (arrVal[i]==2) {  --arrVal[i] ; }
      else { arrVal.splice(i, 1); arrInd.splice(i, 1); }
    } ;

/*    console.log(iAm.openCells) ;
    console.log(iAm.arrGame) ;*/
  } ;

  iAm.closeCells = function(){
    if (iAm.openCells[0].name!=iAm.openCells[1].name)
      if (iAm.arrGame[iAm.openCells[0].name]==iAm.arrGame[iAm.openCells[1].name])
      {
        iAm.openCells[0].unknown = false ;
        iAm.openCells[1].unknown = false ;
        iAm.matchesCells ++ ;
        if (iAm.matchesCells==iAm.forWin ) iAm.tellWin(iAm.winer++) ;
      }
    iAm.openCells[0].close() ;
    iAm.openCells[1].close() ;
    iAm.openCells = [] ;
    iAm.clickCount ++ ;

  } ;

  return iAm;
});










