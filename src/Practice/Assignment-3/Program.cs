// See https://aka.ms/new-console-template for more information

using ChessBoardConsoleGame;
//using ChessGame;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.WriteLine("Enter The White Player Name");
string white_player_Name = Console.ReadLine();
Console.WriteLine("Enter The Black Player Name");
string black_player_Name = Console.ReadLine();

ChessGame chessGame = new ChessGame(new Player(white_player_Name, true), new Player(black_player_Name, false));
chessGame.print();
chessGame.DisplayBoard();
//string[] number = Console.ReadLine().Split(" ");
//if (number.Length == 4)
//{
//    int CX = int.Parse(number[0]);
//    int CY = int.Parse(number[1]);
//    int TX = int.Parse(number[2]);
//    int TY = int.Parse(number[3]);
//    chessGame.MovePiece(new Square(CX, CY), new Square(TX, TY));
//}



//Console.WriteLine("Chess Board 2D Design");
//ChessBoards chessBoard = new ChessBoards();
//chessBoard.PrintBoard();
//chessBoard.MovePiece();


