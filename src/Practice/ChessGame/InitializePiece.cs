using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class InitializePiece
    {
        public ChessPiece[,] board;
        public int Size = 8;
        
        public void SetPiece()
        {
            #region Initialize Piece
            //Set white color piece
            board = new ChessPiece[Size, Size];
            board[0, 0] = new ChessPiece("rook", "white", 0, 0);
            board[0, 1] = new ChessPiece("knight", "white", 0, 1);
            board[0, 2] = new ChessPiece("bishop", "white", 0, 2);
            board[0, 3] = new ChessPiece("queen", "white", 0, 3);
            board[0, 4] = new ChessPiece("king", "white", 0, 4);
            board[0, 5] = new ChessPiece("bishop", "white", 0, 5);
            board[0, 6] = new ChessPiece("knight", "white", 0, 6);
            board[0, 7] = new ChessPiece("rook", "white", 0, 7);
            for (int i = 0; i < Size; i++)
            {
                board[1, i] = new ChessPiece("pawn", "white", 1, i);
            }

            //Set black color piece
            board[7, 0] = new ChessPiece("rook", "black", 7, 0);
            board[7, 1] = new ChessPiece("knight", "black", 7, 1);
            board[7, 2] = new ChessPiece("bishop", "black", 7, 2);
            board[7, 3] = new ChessPiece("queen", "black", 7, 3);
            board[7, 4] = new ChessPiece("king", "black", 7, 4);
            board[7, 5] = new ChessPiece("bishop", "black", 7, 5);
            board[7, 6] = new ChessPiece("knight", "black", 7, 6);
            board[7, 7] = new ChessPiece("rook", "black", 7, 7);
            for (int i = 0; i < Size; i++)
            {
                board[6, i] = new ChessPiece("pawn", "white", 6, i);
            }
            #endregion
        }

    }
}
