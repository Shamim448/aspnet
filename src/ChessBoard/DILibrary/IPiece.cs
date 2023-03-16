using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DILibrary
{
    public interface IPiece
    {
        public abstract bool IsValidMove(object currentSquare, object targetSquare, object board);
        public abstract string GetSymbol(string name);
    }
}
