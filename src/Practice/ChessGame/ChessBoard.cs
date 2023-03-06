using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChessGame
{
    public class ChessBoards
    {
        //2D Array Declear
        private ChessPiece[,] board;
        private const int Size = 8;
        //Constractor for initialize array
        public ChessBoards()
        {
            #region Initialize array into constractor
            //Set white color piece
            board = new ChessPiece[Size, Size];
            board[0, 0] = new ChessPiece("rook", "white", 0, 0);
            board[0, 1] = new ChessPiece("knight", "white", 0, 1);
            board[0, 2] = new ChessPiece("bishop", "white", 0, 2);
            board[0, 3] = new ChessPiece("queen", "white", 0, 3);
            board[0, 4] = new ChessPiece("king", "white", 0, 4);
            board[0, 5] = new ChessPiece("bishop", "white", 0, 5);
            board[0, 6] = new ChessPiece("knight", "white", 0, 6);
            board[0, 7] = new ChessPiece("rook", "white", 0, 7);
            for( int i = 0; i < Size; i++ )
            {
                board[1, i] = new ChessPiece("pawn", "white", 1, i);
            }
            //Set black color piece
            board[7, 0] = new ChessPiece("rook", "black", 7, 0);
            board[7, 1] = new ChessPiece("knight", "black", 7, 1);
            board[7, 2] = new ChessPiece("bishop", "black", 7, 2);
            board[7, 3] = new ChessPiece("queen", "black", 7, 3);
            board[7, 4] = new ChessPiece("king", "black", 7, 4);
            board[7, 5] = new ChessPiece("bishop", "black", 7, 5);
            board[7, 6] = new ChessPiece("knight", "black", 7, 6);
            board[7, 7] = new ChessPiece("rook", "black", 7, 7);
            for (int i = 0; i < Size; i++)
            {
                board[6, i] = new ChessPiece("pawn", "white", 6, i);
            }
           
            #endregion
        }
        //Print Chese Board
        public void PrintBoard()
        {
            #region Print_CheseBoard
            //Used for colums numbers
            for (int i = 0; i < Size; i++)
            {
                Console.Write("  " + i);
            }
            Console.WriteLine();
            for (int row = 0; row < Size; row++)
            {
                Console.Write(row + " "); // used Left side row number
                for (int col = 0; col < Size; col++)
                {
                    if ((row + col) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    }
                    if (board[row, col] == null)
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.Write(" " + board[row, col].GetSymbol(board[row, col].Types) + " ");
                    }
                    Console.ResetColor();
                }
                //used for row number new line 
                Console.WriteLine();
            }
            Console.WriteLine("\n Put the value ");
            
            #endregion
        }

        //Move piece
        public bool MovePiece()
        {
            int formRow = 1; int formCol = 3; int toRow = 3; int toCol = 3;
            string[] number = Console.ReadLine().Split(" ");
            //Console.WriteLine("Enter The row number for select piece, Ex: 3");
            formRow = int.Parse(number[0]);
            formCol = int.Parse(number[1]);
            toRow = int.Parse(number[2]);
            toCol = int.Parse(number[3]);
            //formRow = int.Parse(Console.ReadLine());
            //Console.WriteLine("Enter The col number for select piece, Ex: 3");
            //formCol = int.Parse(Console.ReadLine());
            //Console.WriteLine("Enter The row number for destination of piece, Ex: 3");
            //toRow = int.Parse(Console.ReadLine());
            //Console.WriteLine("Enter The col number for destination of piece, Ex: 3");
            //toCol = int.Parse(Console.ReadLine());
            //If there is no piece
            if (board[formRow,formCol] == null)
            {
                Console.WriteLine("There is no piece at this position");
                return false;
            }
            if(toRow < 0 || toRow > 7 || toCol < 0 || toCol > 7) {
                Console.WriteLine("The distination position out of the range in board");
                return false;
            }
            //check the piece is occupied by same color
            if (board[toRow, toCol] != null && (board[formRow, formCol].Color == board[toRow, toCol].Color))
            {
                Console.WriteLine("The distination position is occupied by a piece of the same color");
                return false;
            }
            //check is valid move
            if (!IsvalidMove(board[formRow, formCol], toRow , toCol))
            {
                Console.WriteLine("Invalid Move for the given piece");
                return false;
            }
            //move the piece in new position
             board[toRow, toCol] = board[formRow, formCol];
            board[formRow, formCol] = null;
            Console.WriteLine("Moved done") ;
            return true;
        }

        //Check valid Move
        private bool IsvalidMove(ChessPiece piece, int toRow, int toCol)
        {
          string piceType = piece.Types;
          bool y = piceType switch
            {
                 "pawn" => (piece.Row + 1 == toRow) && (piece.Col == toCol),
                   
                 "_" => false
            };
            return y;
        }
    }
}
