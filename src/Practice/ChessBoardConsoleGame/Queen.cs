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
        public Queen(bool isWhite) : base("Queen", 4, isWhite)
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
                    if (board.GetSquare(currentSquare.X + 1, col).Piece != null)
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
                int startCol = currentSquare.Y < targetSquare.Y ? currentSquare.Y : targetSquare.Y;
                int endCol = currentSquare.Y > targetSquare.Y ? currentSquare.Y : targetSquare.Y;
                int crow = 1;
                for (int col = startCol + 1; col < endCol; col++)
                {
                    crow += currentSquare.X++;
                    if (board.GetSquare(crow, col).Piece != null)
                    {
                        return false;
                    }
                }

            }


            //int step = targetSquare.X == currentSquare.X ? Math.Sign(targetSquare.Y - currentSquare.Y) : (targetSquare.Y == currentSquare.Y ? Math.Sign(targetSquare.X - currentSquare.X) :
            //    Math.Sign(targetSquare.X - currentSquare.X) * Math.Sign(targetSquare.Y - currentSquare.Y));
            //int i = currentSquare.X + step;
            //int j = currentSquare.Y + step;
            //while (i != targetSquare.X || j != targetSquare.Y) {
            //    if(board.GetSquare(i, j) != null)
            //    {
            //        return false;
            //    }
            //    i += step / Math.Abs(step) * (targetSquare.X == currentSquare.X ? 0 : 1);
            //    j += step / Math.Abs(step) * (targetSquare.Y == currentSquare.Y ? 0 : 1);
            //}
            //if(board.GetSquare(targetSquare.X, targetSquare.Y) == null || board.GetSquare(targetSquare.X, targetSquare.Y).Piece.IsWhite != IsWhite) {
            //    return true;
            //}


            //return false;
            //// Check if there are any pieces in the way of the rook's path
            //if (currentSquare.X == targetSquare.X)
            //{
            //    // Moving horizontally
            //    int startColHorizontally = currentSquare.Y < targetSquare.Y ? currentSquare.Y : targetSquare.Y;
            //    int endColHorizontally = currentSquare.Y > targetSquare.Y ? currentSquare.Y : targetSquare.Y;
            //    for (int cols = startColHorizontally + 1; cols < endColHorizontally; cols++)
            //    {
            //        if (board.GetSquare(currentSquare.X, cols).Piece != null)
            //        {
            //            return false;
            //        }
            //    }
            //}
            //else
            //{
            //    // Moving vertically
            //    int startRowVertically = currentSquare.X < targetSquare.X ? currentSquare.X : targetSquare.X;
            //    int endRowVertically = currentSquare.X > targetSquare.X ? currentSquare.X : targetSquare.X;
            //    for (int rows = startRowVertically + 1; rows < endRowVertically; rows++)
            //    {
            //        if (board.GetSquare(rows, targetSquare.Y).Piece != null)
            //        {
            //            return false;
            //        }
            //    }
            //}

            //// Check if the queen is moving diagonally
            //if (Math.Abs(currentSquare.X - targetSquare.X) != Math.Abs(currentSquare.Y - targetSquare.Y))
            //{
            //    return false;
            //}
            //// Check if there are any pieces in the way of the queen's path
            //int startRow = Math.Min(currentSquare.X, targetSquare.X);
            //int endRow = Math.Max(currentSquare.X, targetSquare.X);
            //int startCol = Math.Min(currentSquare.Y, targetSquare.Y);
            //int endCol = Math.Max(currentSquare.Y, targetSquare.Y);
            //int rowOffset = currentSquare.X < targetSquare.X ? 1 : -1;
            //int colOffset = currentSquare.Y < targetSquare.Y ? 1 : -1;
            //int row = startRow + rowOffset;
            //int col = startCol + colOffset;
            //while (row != endRow && col != endCol)
            //{
            //    if (board.GetSquare(row, col).Piece != null)
            //    {
            //        return false;
            //    }
            //    row += rowOffset;
            //    col += colOffset;
            //}

            // The move is valid
            return true;
        }
    }
}
