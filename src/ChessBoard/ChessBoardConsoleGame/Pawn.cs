
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Pawn : IPiece
    {
        public string Name { get; }
        public bool IsWhite { get; }

        public Pawn(bool isWhite)
        {
            Name = "Pawn";
            IsWhite = isWhite;
        }

        
        public string GetSymbol(string name)
        {
            return IsWhite ? " ♙ " : " ♙ ";
        }

        //public Pawn(bool isWhite) : base("Pawn", isWhite)
        //{
        //}

        //public override string GetSymbol(string name)
        //{
        //    return " ♙ ";
        //}

        public bool IsValidMove(Square currentSquare, Square targetSquare, Board board)
        {

            //// Check if the target square is occupied by a piece of the same color
            //if (targetSquare.Piece != null && targetSquare.Piece.IsWhite == this.IsWhite)
            //{
            //    return false;
            //}

            int rowDiff = Math.Abs(targetSquare.X - currentSquare.X);
            int colDiff = Math.Abs(targetSquare.Y - currentSquare.Y);

            // Check if the pawn is moving forward
            int direction = this.IsWhite ? 1 : -1;
            if (targetSquare.X != currentSquare.X + direction)
            {
                // Check if the pawn is moving forward two squares on its first move
                if (targetSquare.X != currentSquare.X + 2 * direction || currentSquare.X != (this.IsWhite ? 1 : 6))
                {
                    return false;
                }

                // Check if there is an obstruction in the pawn's path
                Square intermediateSquare = board.GetSquare(currentSquare.X + direction, currentSquare.Y);
                if (intermediateSquare.Piece != null)
                {
                    return false;
                }
            }
            // Check if the pawn is moving diagonally to capture an opponent's piece
            if (colDiff == 1 && rowDiff == 1)
            {
                // Check if there is a piece to capture
                if (targetSquare.Piece == null)
                {
                    return false;
                }
            }
            else if (colDiff != 0 || rowDiff > 2)
            {
                // The pawn can't move horizontally or more than two squares vertically
                return false;
            }

            // The move is valid
            return true;
        }

    }
}
