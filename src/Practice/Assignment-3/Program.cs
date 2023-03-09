// See https://aka.ms/new-console-template for more information

using ChessBoardConsoleGame;
//using ChessGame;

Console.OutputEncoding = System.Text.Encoding.Unicode;


ChessGame chessGame = new ChessGame(/*new Player("Shamim", true), new Player("Saba", false)*/);
chessGame.print();
chessGame.DisplayBoard();
chessGame.MovePiece(new Square(1, 1), new Square(2, 1));

//Console.WriteLine("Chess Board 2D Design");
//ChessBoards chessBoard = new ChessBoards();
//chessBoard.PrintBoard();
//chessBoard.MovePiece();


