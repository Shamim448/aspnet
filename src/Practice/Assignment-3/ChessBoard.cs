using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public class ChessBoard
    { 
        public void Start()
        {
            ChessPiece[,] chessBoard = new ChessPiece[8, 8];
            InitializeBoard initialize = new InitializeBoard();
            initialize.InitilezeChessBoard(chessBoard);
            DisplayBoard display = new DisplayBoard();
            display.DissplatChessBoard(chessBoard);
        }
        
        
    }
}
