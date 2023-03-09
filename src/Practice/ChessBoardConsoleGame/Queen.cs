using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Queen : Piece
    {
        public Queen(bool isWhite) : base("Queen", 4, isWhite)
        {
        }

        public override bool IsValidMove(Square currentSquare, Square targetSquare, Board board)
        {
            throw new NotImplementedException();
        }
    }
}
