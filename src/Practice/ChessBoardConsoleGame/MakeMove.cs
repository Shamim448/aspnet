using ChessBoardConsoleGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class MakeMove
    {
        private ChessGame _game;
        public MakeMove(ChessGame game)
        {
            _game = game;
        }
        //Move selected piece
        public void MovePiece(Square currentSquare, Square targetSquare)
        {
            #region Move selected Piece
            // Check if target square is on the board
            if (targetSquare.X < 0 || targetSquare.X > 7 || targetSquare.Y < 0 || targetSquare.Y > 7)
            {
                Console.WriteLine("Invalid move: Out of square range.");
                _game.resetBord = true;
                _game.DisplayBoard();
            }
            if (_game._board.GetSquare(currentSquare.X, currentSquare.Y).Piece == null)
            {
                Console.WriteLine("Invalid move: no piece found on the current square.");
                _game.resetBord = true;
                _game.DisplayBoard();
            }
            if (_game._board.GetSquare(currentSquare.X, currentSquare.Y).Piece.IsWhite != _game._currentPlayer.IsWhite)
            {
                Console.WriteLine("Invalid move: selected piece does not belong to the current player.");
                _game.resetBord = true;
                _game.DisplayBoard();
            }
            if (_game._board.GetSquare(targetSquare.X, targetSquare.Y).Piece != null &&
                 _game._board.GetSquare(targetSquare.X, targetSquare.Y).Piece.IsWhite == _game._currentPlayer.IsWhite)
            {
                Console.WriteLine("Invalid move: selected piece cannot capture a piece of the same color.");
                _game.resetBord = true;
                _game.DisplayBoard();
            }
            if (!_game._board.GetSquare(currentSquare.X, currentSquare.Y).Piece.IsValidMove(currentSquare, targetSquare, _game._board))
            {
                Console.WriteLine("Invalid move: selected piece cannot move to the target square.");
                _game.resetBord = true;
                _game.DisplayBoard();
            }
            // switch the current player
            _game._currentPlayer = _game._currentPlayer == _game._whitePlayer ? _game._blackPlayer : _game._whitePlayer;
            //Print Message update agter valid move
            _game.massage = _game._board.GetSquare(currentSquare.X, currentSquare.Y).Piece.Name + " Move done, Now your turn: " + _game._currentPlayer.Name;
            // move the piece to the target square
            _game._board.GetSquare(targetSquare.X, targetSquare.Y).Piece = _game._board.GetSquare(currentSquare.X, currentSquare.Y).Piece;
            _game._board.GetSquare(currentSquare.X, currentSquare.Y).Piece = null; //set null previos square
            _game.resetBord = false;
            _game.DisplayBoard();
            #endregion
        }
    }
}
