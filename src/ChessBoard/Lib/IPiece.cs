using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib

{
    public interface IPiece
    {
        public string Name { get; }
        public bool IsWhite { get; }
        public bool IsValidMove(Square currentSquare, Square targetSquare, Board board);
        public string GetSymbol(string name);
    }
}
