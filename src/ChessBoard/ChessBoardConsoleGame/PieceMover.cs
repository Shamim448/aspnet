using ChessBoardConsoleGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class PieceMover {
        private readonly ChessBoard _board;

        public PieceMover(ChessBoard board)
        {
            _board = board;
        }
        public void Move(Square currentSquare, Square targetSquare)
        {
             _board.MovePiece(currentSquare, targetSquare);
        }

    }
}
