// See https://aka.ms/new-console-template for more information

using ChessBoardConsoleGame;
//using ChessGame;

Console.OutputEncoding = System.Text.Encoding.Unicode;


ChessGame chessGame = new ChessGame(/*new Player("Shamim", true), new Player("Saba", false)*/);
chessGame.print();
chessGame.DisplayBoard();
chessGame.MovePiece(new Square(0, 5), new Square(3, 5));

//Console.WriteLine("Chess Board 2D Design");
//ChessBoards chessBoard = new ChessBoards();
//chessBoard.PrintBoard();
//chessBoard.MovePiece();


