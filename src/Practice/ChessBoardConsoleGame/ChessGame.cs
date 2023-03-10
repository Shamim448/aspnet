using System;
using System.Collections.Generic;
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
        
        private Board _board;

        private Player _whitePlayer;
        private Player _blackPlayer;
        private Player _currentPlayer;

        private Square currentSquare;
        private Square targetSquare;
        private bool resetBord = false;
        public ChessGame(/*Player whitePlayer, Player blackPlayer*/)
        {
            _board = new Board();
            //_whitePlayer = whitePlayer;
            //_blackPlayer = blackPlayer;
            //_currentPlayer = _whitePlayer;
        }

        public void DisplayBoard()
        {
            if (!resetBord) {
                Console.Clear();
            }
                for (int i = 0; i < 8; i++)
                 {
                for (int j = 0; j < 8; j++)
                {
                    Square square = _board.GetSquare(i, j);
                    if ((i + j) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    }
                    if (square.Piece != null)
                    {
                        Console.Write( square.Piece.GetSymbol(square.Piece.Name)); // print the first character of the piece name
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
            Console.WriteLine("Please Put the currentsqure & targetesqure") ;
            string[] number = Console.ReadLine().Split(" ");
            if (number.Length == 4)
            {
                int CX = int.Parse(number[0]);
                int CY = int.Parse(number[1]);
                int TX = int.Parse(number[2]);
                int TY = int.Parse(number[3]);
                MovePiece(new Square(CX, CY), new Square(TX, TY));
            }
            //MovePiece(currentSquare, targetSquare);
        }
        
        public void MovePiece(Square currentSquare, Square targetSquare)
        {
            //Square currentSquare;
            //Square targetSquare;
            //_board.GetSquare(currentSquare.X = 1, currentSquare.Y = 1);
            if (_board.GetSquare(currentSquare.X, currentSquare.Y).Piece == null)
            {
              //  throw new ArgumentException("Invalid move: no piece found on the current square.");
                Console.WriteLine("Invalid move: no piece found on the current square.");
                resetBord = true;
                DisplayBoard();
                
            }

            //if (_board.GetSquare(currentSquare.X, currentSquare.Y).Piece.IsWhite != _currentPlayer.IsWhite)
            //{
            //    throw new ArgumentException("Invalid move: selected piece does not belong to the current player.");
            //}

            //if (!_board.GetSquare(currentSquare.X, currentSquare.Y).Piece.IsValidMove(currentSquare, targetSquare, _board))
            //{
            //    throw new ArgumentException("Invalid move: selected piece cannot move to the target square.");
            //}

            //if (_board.GetSquare(targetSquare.X, targetSquare.Y).Piece != null &&
            //    _board.GetSquare(targetSquare.X, targetSquare.Y).Piece.IsWhite == _currentPlayer.IsWhite)
            //{
            //    throw new ArgumentException("Invalid move: selected piece cannot capture a piece of the same color.");
            //}
           if( ! _board.GetSquare(currentSquare.X, currentSquare.Y).Piece.IsValidMove(currentSquare, targetSquare, _board))
            {
                Console.WriteLine("Invalid Move for the given piece");
                resetBord = true;
                DisplayBoard();
            }

            // move the piece to the target square
            _board.GetSquare(targetSquare.X, targetSquare.Y).Piece = _board.GetSquare(currentSquare.X, currentSquare.Y).Piece;
            _board.GetSquare(currentSquare.X, currentSquare.Y).Piece = null;
            resetBord = false;
            Console.WriteLine("Moved Done");
            DisplayBoard();
            // switch the current player
            _currentPlayer = _currentPlayer == _whitePlayer ? _blackPlayer : _whitePlayer;
        }

        
       


        //public void Start()
        //{
        //    // game loop
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
