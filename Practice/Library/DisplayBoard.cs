using Library;
using PrimaryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public class DisplayBoard:IDisplayBoard
    {
        public int c = 0;
        public void DissplayChessBoard(object[,] arrs)
        {
            //Used for colums numbers
            for (int i = 0; i < arrs.GetLength(0); i++)
            {
                Console.Write("  " + i);
            }
            Console.WriteLine();
            int c = 0;
            for (int row = 0; row < arrs.GetLength(0); row++)
            {
                
                Console.Write(row + " ");
                for (int col = 0; col < arrs.GetLength(0); col++)
                {
                    if ((row + col) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    }
                     c ++;
                    //Console.Write(" ♜ ");
                    Piece piece = new Piece();
                    piece.SetPiess(c);
                    Console.ResetColor();
                    
                }//end column

                Console.WriteLine();//used for lest side row number
                
            }
           
            

            Console.ReadLine();
        }
    }
   
}
