using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class PrintBoard
    {

        public void DisplayBoard()
        {
            #region print chess board in console   
            if (!resetBord)
            {
                //reset privious output in consol
                Console.Clear();
            }
            //Used for colums numbers
            for (int i = 0; i < 8; i++)
            {
                Console.Write("  " + i);
            }
            Console.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                Console.Write(i + ""); // used Left side row number
                for (int j = 0; j < 8; j++)
                {
                    Square square = _board.GetSquare(i, j);
                    //chease board square color set
                    if ((i + j) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    //set piece in the square
                    if (square.Piece != null && square.Piece.IsWhite)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(square.Piece.GetSymbol(square.Piece.Name)); // print the symbol of the white piece 
                    }
                    else if (square.Piece != null && !square.Piece.IsWhite)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(square.Piece.GetSymbol(square.Piece.Name)); // print thesymbol of the black piece
                    }
                    else
                    {
                        Console.Write("   "); // print a dash to represent an empty square
                    }
                    //Console.Write("  ");
                }
                Console.WriteLine(); // move to the next row
                Console.ResetColor();
            }
            Console.WriteLine("\n" + massage);
            //check user input value for change piece position
            string[] number = Console.ReadLine().Split(" ");
            if (number.Length == 4)
            {
                #region Check user input for move piece
                int CX = int.Parse(number[0]);
                int CY = int.Parse(number[1]);
                int TX = int.Parse(number[2]);
                int TY = int.Parse(number[3]);
                _makeMove.MovePiece(new Square(CX, CY), new Square(TX, TY));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please Put the currentsqure & targetesqure correctly. EX: 1 2 2 2");
                resetBord = true;
                DisplayBoard();
                #endregion
            }
            #endregion
        }
    }
}
