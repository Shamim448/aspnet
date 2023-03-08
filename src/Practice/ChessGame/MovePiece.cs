using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class MovePiece
    {
        InitializePiece initializePiece = new InitializePiece();
        PrintBoard printBoard = new PrintBoard();
        ValidMove vm= new ValidMove();
        public bool Move()
        {
            int formRow = 1; int formCol = 3; int toRow = 3; int toCol = 3;
            string[] number = Console.ReadLine().Split(" ");
            //
            //check inpur number 4 digit
            if (number.Length == 4)
            {
                formRow = int.Parse(number[0]);
                formCol = int.Parse(number[1]);
                toRow = int.Parse(number[2]);
                toCol = int.Parse(number[3]);
            }
            else
            {
                // Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter The correct value, Ex: 1 1 2 1\n");
                Console.ResetColor();
                //reset privious condition
                printBoard.resetBord = true;
                printBoard.PrintCheseBoard();
            }
            //If there is no piece
            if (initializePiece.board[formRow, formCol] == null)
            {
                Console.WriteLine("There is no piece at this position");
                //reset privious condition
                printBoard.resetBord = true;
                printBoard.PrintCheseBoard();
                return false;
            }
            if (toRow < 0 || toRow > 7 || toCol < 0 || toCol > 7)
            {
                Console.WriteLine("The distination position out of the range in board");
                //reset privious condition
                printBoard.resetBord = true;
                printBoard.PrintCheseBoard();
                return false;
            }
            //check the piece is occupied by same color
            if (initializePiece.board[toRow, toCol] != null && (initializePiece.board[formRow, formCol].Color == initializePiece.board[toRow, toCol].Color))
            {
                Console.WriteLine("The distination position is occupied by a piece of the same color");
                //reset privious condition
                printBoard.resetBord = true;
                printBoard.PrintCheseBoard();
                return false;
            }
            //check is valid move
            if (!vm.IsvalidMove(initializePiece.board[formRow, formCol], toRow, toCol))
            {
                Console.WriteLine("Invalid Move for the given piece");
                //reset privious condition
                printBoard.resetBord = true;
                printBoard.PrintCheseBoard();
                return false;
            }
            //move the piece in new position
            initializePiece.board[toRow, toCol] = initializePiece.board[formRow, formCol];
            initializePiece.board[formRow, formCol] = null;
            //reset privious condition
            printBoard.resetBord = true;
            printBoard.PrintCheseBoard();
            // Console.WriteLine("Moved done") ;
            return true;
        }
    }
}
