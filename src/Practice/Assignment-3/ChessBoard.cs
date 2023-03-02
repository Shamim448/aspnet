﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public class ChessBoard
    { 
        public void Start()
        {
            ChessPiece[,] chessBoard = new ChessPiece[8, 8];
            InitializeBoard initialize = new InitializeBoard();
            initialize.InitilezeChessBoard(chessBoard);
            DisplayBoard display = new DisplayBoard();
            display.DissplatChessBoard(chessBoard);
        }
        
        //public void PrintChessBoard()
        //{
        //    #region Print_Chess_Board
        //    Console.WriteLine("    0   1   2   3   4   5   6   7");
        //    for(int r = 0; r < DIMENTION; r++)
        //    {
        //        Console.Write("  ");
        //        for(int c = 0; c < DIMENTION; c++)
        //        {
        //            Console.Write(ChessBoardHorizontalSymbol);
        //        }
        //        Console.Write("+\n");//Print last + and new line
        //       Console.Write(r + " ");
        //        for (int c = 0; c <= DIMENTION; c++)
        //        {
        //            Console.Write(ChessBoardVarticalSymbol + "  ");
        //        }
        //        Console.WriteLine();

        //    }
        //    Console.Write("  ");
        //    for (int r = 0;r < DIMENTION; r++)
        //    {
        //        Console.Write(ChessBoardHorizontalSymbol);
               
        //    }
        //    Console.Write("+\n");
        //    Console.ReadLine();
        //    #endregion
        //}

    }
}