using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ValidMove
    {
        public bool IsvalidMove(ChessPiece piece, int toRow, int toCol)
        {
            string piceType = piece.Types;
            bool y = piceType switch
            {
                "pawn" => (piece.Row + 1 == toRow) && (piece.Col == toCol),

                "_" => false
            };
            return y;
        }
    }
}
