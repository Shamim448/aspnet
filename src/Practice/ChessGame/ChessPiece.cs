using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ChessPiece
    {
        public string Types { get; set; }
        public string Color { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        //Constractor
        public ChessPiece(string type, string color, int row, int col)
        {
            Types = type;
            Color = color;
            Row = row;
            Col = col;
        }
        //check type and set piece symbol
        public string GetSymbol(string type)
        {
            string chessType = type switch
            {
                "rook" => "♜",
                "knight" => "♞",
                "bishop" => "♝",
                "queen" => "♛",
                "king" => "♚",
                "pawn" => "♙",
                _ => " ",
            };
            return chessType;
        }
        



    }
}
