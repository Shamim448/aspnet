using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Knight : Piece
    {
        public Knight(bool isWhite) : base("Knight", 2, isWhite)
        {
        }

        public override bool IsValidMove(Square currentSquare, Square targetSquare, Board board)
        {
            throw new NotImplementedException();
        }
    }
}
