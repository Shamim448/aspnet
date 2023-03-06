// See https://aka.ms/new-console-template for more information
using Assignment_3;
using ChessGame;
using Library;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.WriteLine("Chess Board 2D Design");
ChessBoards chessBoard = new ChessBoards();
chessBoard.PrintBoard();
chessBoard.MovePiece();



