// See https://aka.ms/new-console-template for more information

using ChessGame;

Console.OutputEncoding = System.Text.Encoding.Unicode;


//ChessGame chessGame = new ChessGame(new Player("Shamim", true), new Player("Saba", false));
//chessGame.print();
//chessGame.DisplayBoard();

Console.WriteLine("Chess Board 2D Design");
ChessBoards chessBoard = new ChessBoards();
chessBoard.PrintBoard();
chessBoard.MovePiece();


