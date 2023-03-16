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
        private readonly ChessBoard _board;
        private readonly ChessBoardDisplay _boardDisplay;
        private readonly PieceMover _pieceMover;
        private  Player _player1;
        private  Player _player2;

        public ChessGame()
        {
            _player1 = new Player("White", true);
            _player2 = new Player("Black", false);
            _board = new ChessBoard(_player1,  _player2);
            _boardDisplay = new ChessBoardDisplay(_board);
            _pieceMover = new PieceMover(_board);
        }
        public void Play()
        {
            while (true) { 
            // Display the board
            _boardDisplay.Display();

            string input = Console.ReadLine();
            // Parse the input into coordinates
            string[] coordinates = input.Split(' ');
            if (coordinates.Length != 4)
            {
              _boardDisplay.Display();
              Console.WriteLine("Please enter two valid coordinates in the format 'x1 y1 x2 y2'.");                 
              return;
            }

            int currentX, currentY, targetX, targetY;
            if (!int.TryParse(coordinates[0], out currentX) ||
                !int.TryParse(coordinates[1], out currentY) ||
                !int.TryParse(coordinates[2], out targetX) ||
                !int.TryParse(coordinates[3], out targetY))
            {

                Console.WriteLine("Please enter two valid coordinates in the format 'x1 y1 x2 y2'.");
                return;
            }
            Square currentSquare = new Square(currentX, currentY);
            Square targetSquare = new Square(targetX, targetY);
            _pieceMover.Move(currentSquare, targetSquare);
                
            }
            
        }

        
    }
}
