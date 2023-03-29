using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public static class Move 
    {
        //Remove duplicate code
        public static bool Diagonally(Square currentSquare, Square targetSquare, Board board)
        {
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
            return true;
        }

        public static bool Vartically(Square currentSquare, Square targetSquare, Board board)
        {
            int startRow = currentSquare.X < targetSquare.X ? currentSquare.X : targetSquare.X;
            int endRow = currentSquare.X > targetSquare.X ? currentSquare.X : targetSquare.X;
            for (int row = startRow + 1; row < endRow; row++)
            {
                if (board.GetSquare(row, targetSquare.Y).Piece != null)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool Horizontally(Square currentSquare, Square targetSquare, Board board)
        {
            int startCol = currentSquare.Y < targetSquare.Y ? currentSquare.Y : targetSquare.Y;
            int endCol = currentSquare.Y > targetSquare.Y ? currentSquare.Y : targetSquare.Y;
            for (int col = startCol + 1; col < endCol; col++)
            {
                if (board.GetSquare(currentSquare.X, col).Piece != null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
