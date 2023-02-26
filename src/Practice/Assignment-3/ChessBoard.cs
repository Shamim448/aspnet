using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public class ChessBoard
    {
        public string[,] _chessBoard;
        public const int DIMENTION = 8;
        public string ChessBoardHorizontalSymbol { get; set; }
        public string ChessBoardVarticalSymbol { get; set; }
        public ChessBoard()
        {
            _chessBoard = new string[DIMENTION, DIMENTION];
            ChessBoardHorizontalSymbol = "+---";
            ChessBoardVarticalSymbol = "| ";
        }
        public void PrintChessBoard()
        {
            Console.WriteLine("    0   1   2   3   4   5   6   7");
            for(int r = 0; r < DIMENTION; r++)
            {
                Console.Write("  ");
                for(int c = 0; c < DIMENTION; c++)
                {
                    Console.Write(ChessBoardHorizontalSymbol);
                }
                Console.Write("+\n");//Print last + and new line
                Console.Write(r + " ");
                for (int c = 0; c <= DIMENTION; c++)
                {
                    Console.Write(ChessBoardVarticalSymbol + "  ");
                }
                Console.WriteLine();

            }
            Console.Write("  ");
            for (int r = 0;r < DIMENTION; r++)
            {
                Console.Write(ChessBoardHorizontalSymbol);
            }
            Console.Write("+\n");

        }
      
    }
}
