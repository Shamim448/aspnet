using PrimaryLibrary;
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
        private readonly Initializer _initializer;
        private readonly IDisplayBoard _displayBoard;
        public ChessBoard(Initializer initializer, IDisplayBoard DisplayBoard)
        {
            _initializer = initializer;
            _displayBoard = DisplayBoard;
        }

        public void Start()
        {
            ChessBoard[,] chessBoard = new ChessBoard[8, 8];
            _initializer.InitilezeChessBoard(chessBoard);
            _displayBoard.DissplayChessBoard(chessBoard);
        }
        
        
    }
}
