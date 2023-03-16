namespace ChessBoardConsoleGame
{
  public abstract class Piece {
        public string Name { get; protected set; }
        public bool IsWhite { get; protected set; }

        public Piece(string name, bool isWhite)
        {
            Name = name;
            IsWhite = isWhite;
        }
        public abstract bool IsValidMove(Square currentSquare, Square targetSquare, Board board);
        public string GetSymbol(string type)
        {
                string chessType = type switch
                {
                    "Rook" => " ♜ ",
                    "Knight" => " ♞ ",
                    "Bishop" => " ♝ ",
                    "Queen" => " ♛ ",
                    "King" => " ♔ ", 
                    "Pawn" => " ♙ ",
                    _ => " ",
                };
                return chessType;
        }
   
    }

}