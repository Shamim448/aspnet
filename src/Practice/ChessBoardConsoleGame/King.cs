using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChessBoardConsoleGame
{
    public class King : Piece
    {
        public King(bool isWhite) : base("King", 100, isWhite)
        {
        }

        public override bool IsValidMove(Square currentSquare, Square targetSquare, Board board)
        {
            // Calculate absolute difference in row and column indices
            int rowDiff = Math.Abs(targetSquare.X - currentSquare.X);
            int colDiff = Math.Abs(targetSquare.Y - currentSquare.Y);

            if ((rowDiff == 1 && colDiff == 0) || (rowDiff == 0 && colDiff == 1) || (rowDiff == 1 && colDiff == 1))
            {
                // check if the target square is empty or contains an opponent's piece
                if (board.GetSquare(targetSquare.X, targetSquare.Y).Piece == null ||
                    board.GetSquare(targetSquare.X, targetSquare.Y).Piece.IsWhite != IsWhite)
                {
                    return true;
                }
            }

            return false;
        }


    }
}
