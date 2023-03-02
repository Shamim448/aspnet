using Assignment_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public  class Piece
    {
        public void SetPiess(int celNumber)
        {
            if (celNumber <= 16 || celNumber > 48)
            {
                string y = celNumber switch
                {
                    0 => " ♜ ",
                    1 => " ♝ ",
                    2 => " ♞ ",
                    3 => " ♝ ",
                    4 => " ♜ ",
                    5 => " ♜ ",
                    6 => " ♜ ",
                    7 => " ♜ ",
                    _ => " ♜ ",
                };
                Console.Write(y);
            }
            else
            {
                Console.Write("   ");
            }
            
        }
        public string Color { get; set; }
        public string Symbol { get; set; }

        
    }
   
}
