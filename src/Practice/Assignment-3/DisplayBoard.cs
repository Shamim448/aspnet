using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public class DisplayBoard
    {
        public void DissplatChessBoard(ChessPiece[,] chessBoard)
        {
            for (int i = 0; i < chessBoard.GetLength(0); i++)
            {
                Console.Write("  " + i);
            }
            Console.WriteLine();

            for (int row = 0; row < chessBoard.GetLength(0); row++)
            {
                Console.Write(row + " ");
                for (int col = 0; col < chessBoard.GetLength(0); col++)
                {
                    if((row + col) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else 
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    }
                    Console.Write("   ");
                   
                    Console.ResetColor();
                }//end column

                Console.WriteLine();//used for lest side row number
            }
           
           
            Console.ReadLine();
        }
        
    }
}
