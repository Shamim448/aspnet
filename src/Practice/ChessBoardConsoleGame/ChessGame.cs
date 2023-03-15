using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class ChessGame
    {

        public void print()
        {
            Console.WriteLine("Welcome Game");
        }
        public Board _board;
        public Player _whitePlayer;
        public Player _blackPlayer;
        public Player _currentPlayer;
        private MakeMove _makeMove;
        //private Square currentSquare;
        //private Square targetSquare;
       public bool resetBord = false;
        public string massage { get; set; }
        public ChessGame(Player whitePlayer, Player blackPlayer, MakeMove makeMove)
        {
            _board = new Board();
            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;
            _currentPlayer = _currentPlayer != _whitePlayer ? _whitePlayer : _blackPlayer;
            massage = "Please Put the currentsqure & targetesqure for " + _currentPlayer.Name;
            _makeMove = makeMove;
        }

        public void DisplayBoard()
        {
            #region print chess board in console   
            if (!resetBord)
            {
                //reset privious output in consol
                Console.Clear();
            }
            //Used for colums numbers
            for (int i = 0; i < 8; i++)
            {
                Console.Write("  " + i);
            }
            Console.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                Console.Write(i + ""); // used Left side row number
                for (int j = 0; j < 8; j++)
                {
                    Square square = _board.GetSquare(i, j);
                    //chease board square color set
                    if ((i + j) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta; 
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    //set piece in the square
                    if (square.Piece != null && square.Piece.IsWhite)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(square.Piece.GetSymbol(square.Piece.Name)); // print the symbol of the white piece 
                    }
                    else if (square.Piece != null && !square.Piece.IsWhite)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(square.Piece.GetSymbol(square.Piece.Name)); // print thesymbol of the black piece
                    }
                    else
                    {
                        Console.Write("   "); // print a dash to represent an empty square
                    }
                    //Console.Write("  ");
                }
                Console.WriteLine(); // move to the next row
                Console.ResetColor();
            }
            Console.WriteLine("\n" + massage);
            //check user input value for change piece position
            string[] number = Console.ReadLine().Split(" ");
            if (number.Length == 4)
            {
                #region Check user input for move piece
                int CX = int.Parse(number[0]);
                int CY = int.Parse(number[1]);
                int TX = int.Parse(number[2]);
                int TY = int.Parse(number[3]);
                _makeMove.MovePiece(new Square(CX, CY), new Square(TX, TY));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please Put the currentsqure & targetesqure correctly. EX: 1 2 2 2");
                resetBord = true;
                DisplayBoard();
                #endregion
            }
            #endregion
        }
        //Move selected piece
        //public void MovePiece(Square currentSquare, Square targetSquare)
        //{
        //    #region Move selected Piece
        //    // Check if target square is on the board
        //    if (targetSquare.X < 0 || targetSquare.X > 7 || targetSquare.Y < 0 || targetSquare.Y > 7)
        //    {
        //       Console.WriteLine("Invalid move: Out of square range.");
        //        resetBord = true;
        //        DisplayBoard();
        //    }

        //    if (_board.GetSquare(currentSquare.X, currentSquare.Y).Piece == null)
        //    {
        //        Console.WriteLine("Invalid move: no piece found on the current square.");
        //        resetBord = true;
        //        DisplayBoard();         
        //    }
        //    if (_board.GetSquare(currentSquare.X, currentSquare.Y).Piece.IsWhite != _currentPlayer.IsWhite)
        //    {
        //        Console.WriteLine("Invalid move: selected piece does not belong to the current player.");
        //        resetBord = true;
        //        DisplayBoard();
        //    }
        //    if (_board.GetSquare(targetSquare.X, targetSquare.Y).Piece != null &&
        //         _board.GetSquare(targetSquare.X, targetSquare.Y).Piece.IsWhite == _currentPlayer.IsWhite)
        //    {
        //        Console.WriteLine("Invalid move: selected piece cannot capture a piece of the same color.");
        //        resetBord = true;
        //        DisplayBoard();
        //    }
           
        //    if ( ! _board.GetSquare(currentSquare.X, currentSquare.Y).Piece.IsValidMove(currentSquare, targetSquare, _board))
        //    {
        //        Console.WriteLine("Invalid move: selected piece cannot move to the target square.");
        //        resetBord = true;
        //        DisplayBoard();
        //    }
        //    // switch the current player
        //    _currentPlayer = _currentPlayer == _whitePlayer ? _blackPlayer : _whitePlayer;
        //    //Print Message update agter valid move
        //    massage = _board.GetSquare(currentSquare.X, currentSquare.Y).Piece.Name + " Move done, Now your turn: " + _currentPlayer.Name ;
        //    // move the piece to the target square
        //    _board.GetSquare(targetSquare.X, targetSquare.Y).Piece = _board.GetSquare(currentSquare.X, currentSquare.Y).Piece;
        //    _board.GetSquare(currentSquare.X, currentSquare.Y).Piece = null; //set null previos square
        //    resetBord = false;
        //    DisplayBoard();
        //    #endregion
        //}




        //public void Start()
        //{

        //}


        //private void EndGame(Player winner)
        //{
        //    // game end logic
        //}

        //private bool IsCheckmate(Player player)
        //{
        //    // checkmate logic
        //}

        //private bool IsStalemate(Player player)
        //{
        //    // stalemate logic
        //}
    }
}
