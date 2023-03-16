using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class ChessBoardDisplay
    {
        private readonly ChessBoard _board;

        public ChessBoardDisplay(ChessBoard board)
        {
            _board = board;
        }
        public void Display()
        {
            _board.DisplayBoard();
        }
    }
}
