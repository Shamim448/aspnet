using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Rook : Piece
    {
        public Rook(bool isWhite) : base("Rook", 1, isWhite)
        {
        }

        public override bool IsValidMove(Square currentSquare, Square targetSquare, Board board)
        {
            throw new NotImplementedException();
        }
    }
}
