using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public interface ISquare
    {
        public int X { get; set; }
        public int Y { get; set; }
        public IPiece Piece { get; set; }
        public object[,] squares { get; set; }
    }
}
