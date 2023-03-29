using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Bishop : IPiece
    {
        public string Name { get; }
        public bool IsWhite { get; }

        public Bishop(bool isWhite)
        {
            Name = "Bishop";
            IsWhite = isWhite;
        }

        public string GetSymbol(string name)
        {
            return IsWhite ? " ♗ " : " ♝ ";
        }
        public bool IsValidMove(Square currentSquare, Square targetSquare, Board board)
        {
            // Check if the rook is moving horizontally or vertically
            if ((Math.Abs(targetSquare.X - currentSquare.X) != Math.Abs(targetSquare.Y - currentSquare.Y)))
            {
                return false;
            }
            // Moving diagonally
            Move.Diagonally(currentSquare, targetSquare, board);
            
            //// The move is valid
            return true;
        }

       
    }
}
