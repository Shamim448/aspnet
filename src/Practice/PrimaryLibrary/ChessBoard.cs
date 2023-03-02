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
       // private readonly IChessPiece _chessPiece;
        public ChessBoard(Initializer initializer, IDisplayBoard DisplayBoard /*IChessPiece chessPiece*/)
        {
            _initializer = initializer;
            _displayBoard = DisplayBoard;
            //_chessPiece = chessPiece;
        }
        //public ChessBoard(IChessPiece chessPiece)
        //{
           
        //}
        //public string Symbol { get; set; }
        public void Start()
        {
            ChessBoard[,] chessBoard = new ChessBoard[8, 8];
            _initializer.InitilezeChessBoard(chessBoard);
            _displayBoard.DissplayChessBoard(chessBoard);
           // _chessPiece.SetPeice(chessBoard);

        }
        
        
    }
}
