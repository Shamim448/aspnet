using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class ChessBoard
    {
        private Player _whitePlayer;
        private Player _blackPlayer;
        private Player _currentPlayer;
        private Board _board;
        public string massage { get; set; }
        public ChessBoard(Player whitePlayer, Player blackPlayer)
        {
            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;
            _currentPlayer = _currentPlayer != _whitePlayer ? _whitePlayer : _blackPlayer;
            _board = new Board();
            massage = _currentPlayer.Name + " Player, Please enter two valid coordinates in the format '1 3 3 3'.";
        }
        public void DisplayBoard()
        {
            #region print chess board in console  
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Welcome to 2D Chess Board Console Game");
            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + massage );
            #endregion
        }

        public void MovePiece(Square currentSquare, Square targetSquare)
        {
            #region Move selected Piece
            
            // Check if target square is on the board
            if (targetSquare.X < 0 || targetSquare.X > 7 || targetSquare.Y < 0 || targetSquare.Y > 7)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                massage = "Invalid move: Out of square range, Put The Valid range.";
                return;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (_board.GetSquare(currentSquare.X, currentSquare.Y).Piece == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                massage = "Invalid move: no piece found on the current square. select correct square";
                return;
            }
            if (_board.GetSquare(currentSquare.X, currentSquare.Y).Piece.IsWhite != _currentPlayer.IsWhite)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                massage = "Invalid move: selected piece does not belong to the current player, select right Player.";
                return;
            }
            if (_board.GetSquare(targetSquare.X, targetSquare.Y).Piece != null &&
                 _board.GetSquare(targetSquare.X, targetSquare.Y).Piece.IsWhite == _currentPlayer.IsWhite)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                massage = "Invalid move: selected piece cannot capture a piece of the same color, select right color.";
                return;
            }

            if (!_board.GetSquare(currentSquare.X, currentSquare.Y).Piece.IsValidMove(currentSquare, targetSquare, _board))
            {
                massage = "Invalid move: selected piece cannot move to the target square, put right valid square.";
                return;
            }
            // switch the current player
            _currentPlayer = _currentPlayer == _whitePlayer ? _blackPlayer : _whitePlayer;
            //Print Message update agter valid move
            massage = _board.GetSquare(currentSquare.X, currentSquare.Y).Piece.Name + " Move done, Now your turn: " + _currentPlayer.Name;
            // move the piece to the target square
            _board.GetSquare(targetSquare.X, targetSquare.Y).Piece = _board.GetSquare(currentSquare.X, currentSquare.Y).Piece;
            _board.GetSquare(currentSquare.X, currentSquare.Y).Piece = null; //set null previos square
            #endregion
        }

    }
}
