using PrimaryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public class InitializeBoard : Initializer
    {
        public void InitilezeChessBoard(object[,] arrs)
        {
            for (int row = 0; row < arrs.GetLength(0); row++)
            {
                for (int col = 0; col < arrs.GetLength(1); col++)
                {
                    arrs[row, col] = null;
                }
            }
        }
    }
}
