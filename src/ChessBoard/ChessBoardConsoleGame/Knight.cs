using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Knight : Piece
    {
        public Knight(bool isWhite) : base("Knight", isWhite)
        {
        }

        public override bool IsValidMove(Square currentSquare, Square targetSquare, Board board)
        {  
            // Calculate absolute difference in row and column indices
            int rowDiff = Math.Abs(targetSquare.X - currentSquare.X);
            int colDiff = Math.Abs(targetSquare.Y - currentSquare.Y);
            // Check if move is a valid knight move
            if (!((rowDiff == 1 && colDiff == 2) || (rowDiff == 2 && colDiff == 1)))
            {
                return false;
            }
            // Check if target square is occupied by same color piece
            if (targetSquare.Piece != null && targetSquare.Piece.IsWhite == this.IsWhite)
            {
                return false;
            }
            // No other checks needed, knight can jump over pieces
            return true;
        }
    }
}
