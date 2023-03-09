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
            int dx = Math.Abs(currentSquare.X - targetSquare.X);
            int dy = Math.Abs(currentSquare.Y - targetSquare.Y);

            if ((dx == 1 && dy == 0) || (dx == 0 && dy == 1) || (dx == 1 && dy == 1))
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
