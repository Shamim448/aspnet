﻿// See https://aka.ms/new-console-template for more information
using Assignment_3;
using Library;
Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.WriteLine("Chess Board 2D Design");
ChessBoard chessBoard = new ChessBoard(new InitializeBoard(), new DisplayBoard() /*new ChessPiece()*/);
chessBoard.Start();


