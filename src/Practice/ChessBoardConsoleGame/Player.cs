using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardConsoleGame
{
    public class Player
    {
        public string Name { get; set; }
        public bool IsWhite { get; set; }

        public Player(string name, bool isWhite)
        {
            Name = name;
            IsWhite = isWhite;
        }

        public void MakeMove(Board board, int row, int column)
        {
            if (board.IsCellEmpty(row, column))
            {
                //board.PlacePiece(row, column, this); // place the current player's game piece at the specified location
            }
            else
            {
                Console.WriteLine("Invalid move - cell is already occupied.");
            }
        }
    }
}
