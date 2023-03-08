using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class PrintBoard
    {
       // InitializePiece initializePiece = new InitializePiece();
        public bool resetBord = false;
        public InitializePiece piece;
        public void PrintCheseBoard()
        {
            
            #region Print_CheseBoard
            if (!resetBord)
            {
                Console.Clear();
                //Used for colums numbers
                for (int i = 0; i < piece.Size; i++)
                {
                    Console.Write("  " + i);
                }
                Console.WriteLine();
                for (int row = 0; row < piece.Size; row++)
                {
                    Console.Write(row + " "); // used Left side row number
                    for (int col = 0; col < piece.Size; col++)
                    {
                        if ((row + col) % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Magenta;
                        }
                        if (piece.board[row, col] == null)
                        {
                            Console.Write("   ");
                        }
                        else
                        {
                            Console.Write(" " + piece.board[row, col].GetSymbol(piece.board[row, col].Types) + " ");
                        }
                        Console.ResetColor();
                    }
                    //used for row number new line 
                    Console.WriteLine();
                }
                Console.WriteLine("\nPut the input value of selected piece row col and destination row, col: EX- 1 2 2 2 ");
                new MovePiece().Move();
            }
            else
            {
                Console.WriteLine("Put in correct formate (from row & col and to row & col) col: EX- 1 2 2 2 ");
                new MovePiece().Move();
            }

            #endregion
        }
    }
}
