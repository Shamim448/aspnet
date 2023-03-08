using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChessGame
{
    public class ChessBoards
    {
        //2D Array Declear
        //private ChessPiece[,] board;
        //private const int Size = 8;
        private bool resetBord = false;
        InitializePiece initializePiece = new InitializePiece();
        PrintBoard printBoard = new PrintBoard();
        
        public ChessBoards()
        {
            //Constractor for initialize array
            initializePiece.SetPiece();
            //Print Chese Board
            printBoard.PrintCheseBoard();
        }
        
        
        
           //Move piece


        //Check valid Move
        
    }
}
