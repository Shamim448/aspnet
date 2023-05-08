using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Rook : IPiece
    {
        public string Name { get; }
        public bool IsWhite { get; }

        public Rook(bool isWhite)
        {
            Name = "Rook";
            IsWhite = isWhite;
        }

        public string GetSymbol(string name)
        {
            return IsWhite ? " ♖ " : " ♜ ";
        }
        
        public bool IsValidMove(Square currentSquare, Square targetSquare, Board board)
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
                Move.Horizontally(currentSquare, targetSquare, board);
            }
            else
            {
                // Moving vertically
                Move.Vartically(currentSquare, targetSquare, board);
            }

            // The move is valid
            return true;
            
        }

        
    }
}
