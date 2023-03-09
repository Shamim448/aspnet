using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Bishop : Piece
    {
        public Bishop(bool isWhite) : base("Bishop", 3, isWhite)
        {
        }

        public override bool IsValidMove(Square currentSquare, Square targetSquare, Board board)
        {
            throw new NotImplementedException();
        }
    }
}
