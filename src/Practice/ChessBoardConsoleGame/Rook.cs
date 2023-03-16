using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Rook : Piece
    {
        public Rook(bool isWhite) : base("Rook", isWhite)
        {
        }

        public override bool IsValidMove(Square currentSquare, Square targetSquare, Board board)
        {

            // Check if the rook is moving horizontally or vertically
            if (currentSquare.X != targetSquare.X && currentSquare.Y != targetSquare.Y)
            {
                return false;
            }

            // Check if there are any pieces in the way of the rook's path
            if (currentSquare.X == targetSquare.X)
            {
                // Moving horizontally
                int startCol = currentSquare.Y < targetSquare.Y ? currentSquare.Y : targetSquare.Y;
                int endCol = currentSquare.Y > targetSquare.Y ? currentSquare.Y : targetSquare.Y;
                for (int col = startCol + 1; col < endCol; col++)
                {
                    if (board.GetSquare(currentSquare.X, col).Piece != null)
                    {
                        return false;
                    }
                }
            }
            else
            {
                // Moving vertically
                int startRow = currentSquare.X < targetSquare.X ? currentSquare.X : targetSquare.X;
                int endRow = currentSquare.X > targetSquare.X ? currentSquare.X : targetSquare.X;
                for (int row = startRow + 1; row < endRow; row++)
                {
                    if (board.GetSquare(row, targetSquare.Y).Piece != null)
                    {
                        return false;
                    }
                }
            }

            // The move is valid
            return true;
            
        }
    }
}
