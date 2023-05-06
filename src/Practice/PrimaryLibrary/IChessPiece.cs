using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public interface IChessPiece
    {
        public string Symbol { get; }
        public void SetPeice(object[,] arrs);



    }
}
