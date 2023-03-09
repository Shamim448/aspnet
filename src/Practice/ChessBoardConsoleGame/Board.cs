using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Board
    {
        private Square[,] _squares;

        public Board()
        {
            _squares = new Square[8, 8];
            // initialize board squares
            InitializeBoard();
        }

        public Square GetSquare(int x, int y)
        {
            return _squares[x, y];
        }

        private void InitializeBoard()
        {
            // initialize white pieces
            _squares[0, 0] = new Square(0, 0, new King(true));
            _squares[0, 1] = new Square(0, 1, new King(true));
            _squares[0, 2] = new Square(0, 2, new King(true));
            _squares[0, 3] = new Square(0, 3, new King(true));
            _squares[0, 4] = new Square(0, 4, new King(true));
            _squares[0, 5] = new Square(0, 5, new King(true));
            _squares[0, 6] = new Square(0, 6, new King(true));
            _squares[0, 7] = new Square(0, 7, new King(true));
            for (int i = 0; i < 8; i++)
            {
                _squares[1, i] = new Square(1, i, new King(true));
            }

            // initialize empty squares
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _squares[i, j] = new Square(i, j);
                }
            }
            // initialize black pieces
            _squares[7, 0] = new Square(7, 0, new King(false));
            _squares[7, 1] = new Square(7, 1, new King(false));
            _squares[7, 2] = new Square(7, 2, new King(false));
            _squares[7, 3] = new Square(7, 3, new King(false));
            _squares[7, 4] = new Square(7, 4, new King(false));
            _squares[7, 5] = new Square(7, 5, new King(false));
            _squares[7, 6] = new Square(7, 6, new King(false));
            _squares[7, 7] = new Square(7, 7, new King(false));
            for (int i = 0; i < 8; i++)
            {
                _squares[6, i] = new Square(6, i, new King(false));
            }
        }

        //        //public void MovePiece(Square currentSquare, Square targetSquare)
        //        //{
        //        //    // move piece logic
        //        //}

        internal bool IsCellEmpty(int row, int column)
        {
            throw new NotImplementedException();
        }
    }
}
