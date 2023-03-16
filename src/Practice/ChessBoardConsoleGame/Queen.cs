using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Queen : Piece
    {
       // private Rook _rook;
        public Queen(bool isWhite) : base("Queen", isWhite)
        {
        }
       
        public override bool IsValidMove(Square currentSquare, Square targetSquare, Board board)     
        {
            // Check if the rook is moving horizontally or vertically
            if (currentSquare.X != targetSquare.X && currentSquare.Y != targetSquare.Y
            && (Math.Abs(targetSquare.X - currentSquare.X) != 
            Math.Abs(targetSquare.Y - currentSquare.Y)))
            {
                return false;
            }
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
            
            else if(currentSquare.Y == targetSquare.Y)
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
            //top to bottom right cornner done
            else
            {
                // Moving diagonally
                int stepX = Math.Sign(targetSquare.X - currentSquare.X);
                int stepY = Math.Sign(targetSquare.Y - currentSquare.Y);
                int i = currentSquare.X + stepX;
                int j = currentSquare.Y + stepY;
                while (i != targetSquare.X || j != targetSquare.Y)
                {
                    if (board.GetSquare(i, j).Piece != null)
                    {
                        return false;
                    }
                    i += stepX;
                    j += stepY;
                }
            }
            // The move is valid
            return true;
        }
    }
}
