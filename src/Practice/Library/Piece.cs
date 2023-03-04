using Assignment_3;
using Library;
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
            if (celNumber <= 16  )
            {
                string y = celNumber switch
                {
                    1 or 8  => " ♜ ",
                    2 or 7 => " ♞ ",
                    3 or 6 => " ♝ ",
                    4 => " ♛ ",
                    5 => " ♚ ",
                    _ => " ♙ ",
                };
                Console.Write(y);
            }
            else if (celNumber > 48) { 
                string y = celNumber switch {
                    57 or 64  => " ♜ ",
                    58 or 63 => " ♘ ",
                    59 or 62 => " ♗ ",
                    60 => " ♛ ",
                    61 => " ♔ ",
                    _ => " ♟ ",
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




