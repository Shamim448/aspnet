using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Square
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Piece Piece { get; set; }
        public Square(int x, int y, Piece piece = null)
        {
            X = x;
            Y = y;
            Piece = piece;
        }
    }

}
