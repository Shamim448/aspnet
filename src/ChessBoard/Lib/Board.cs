using Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Board
    {
        private ISquare[,] _arrSquare;
        private ISquare _square;
        public Board(ISquare square)
        {
            _square = square;
            // _squares = new Square[8, 8];
            _arrSquare = new ISquare[8, 8];
            // initialize board squares
            InitializeBoard();
        }

        public ISquare GetSquare(int x, int y)
        {
            return _arrSquare[x, y];
        }

        private void InitializeBoard()
        {
            // initialize white pieces
            _arrSquare[0, 0] = new ISquare(0, 0, true);
            _arrSquare[0, 1] = new ISquare(0, 1, new Knight(true));
            _arrSquare[0, 2] = new ISquare(0, 2, new Bishop(true));
            _arrSquare[0, 3] = new ISquare(0, 3, new Queen(true));
            _arrSquare[0, 4] = new ISquare(0, 4, new King(true));
            _arrSquare[0, 5] = new ISquare(0, 5, new Bishop(true));
            _arrSquare[0, 6] = new ISquare(0, 6, new Knight(true));
            _arrSquare[0, 7] = new ISquare(0, 7, new Rook(true));
            for (int i = 0; i < 8; i++)
            {
                _arrSquare[1, i] = new ISquare(1, i, new Pawn(true));
            }

            // initialize empty squares
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _arrSquare[i, j] = new ISquare(i, j);
                }
            }
            // initialize black pieces
            _arrSquare[7, 0] = new ISquare(7, 0, new Rook(false));
            _arrSquare[7, 1] = new ISquare(7, 1, new Knight(false));
            _arrSquare[7, 2] = new ISquare(7, 2, new Bishop(false));
            _arrSquare[7, 3] = new ISquare(7, 3, new Queen(false));
            _arrSquare[7, 4] = new ISquare(7, 4, new King(false));
            _arrSquare[7, 5] = new ISquare(7, 5, new Bishop(false));
            _arrSquare[7, 6] = new ISquare(7, 6, new Knight(false));
            _arrSquare[7, 7] = new ISquare(7, 7, new Rook(false));
            for (int i = 0; i < 8; i++)
            {
                _arrSquare[6, i] = new ISquare(6, i, new Pawn(false));
            }
        }

              
    }
}
