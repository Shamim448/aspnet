using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Queen : IPiece
    {
        public string Name { get; }
        public bool IsWhite { get; }

        public Queen(bool isWhite)
        {
            Name = "Queen";
            IsWhite = isWhite;
        }


        public string GetSymbol(string name)
        {
            return IsWhite ? " ♕ " : " ♛ ";
        }
        public bool IsValidMove(Square currentSquare, Square targetSquare, Board board)     
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
                Move.Horizontally(currentSquare, targetSquare, board);
            }
            
            else if(currentSquare.Y == targetSquare.Y)
            {
                // Moving vertically
                Move.Vartically(currentSquare, targetSquare, board);
            }
            //top to bottom right cornner done
            else
            {
                Move.Diagonally(currentSquare, targetSquare, board);
                
            }
            // The move is valid
            return true;
        }
    }
}
