using Assignment_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ChessPiece : IChessPiece
    {
        public string Symbol { get; set; }
        public void SetPeice(object[,] arrs)
        {
            arrs[0, 0] = "+++";
            arrs[0, 1] = "U + 2656";
            arrs[0, 2] = "U + 2656";
            arrs[0, 3] = "U + 2656";
            arrs[0, 4] = "U + 2656";
            arrs[0, 5] = "U + 2656";
            arrs[0, 6] = "U + 2656";
            arrs[0, 7] = "U + 2656";
        }
       
    }
}
