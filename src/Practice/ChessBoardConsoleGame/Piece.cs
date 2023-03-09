namespace ChessBoardConsoleGame
{
  public abstract class Piece {
        public string Name { get; protected set; }
        public int Value { get; protected set; }
        public bool IsWhite { get; protected set; }

        public Piece(string name, int value, bool isWhite)
        {
            Name = name;
            Value = value;
            IsWhite = isWhite;
        }
        public string GetSymbol(string type)
        {
            string chessType = type switch
            {
                "Rook" => " ♜ ",
                "Knight" => " ♞ ",
                "Bishop" => " ♝ ",
                "Queen" => " ♛ ",
                "King" => " ♚ ",
                "Pawn" => " ♙ ",
                _ => " ",
            };
            return chessType;
        }
    public abstract bool IsValidMove(Square currentSquare, Square targetSquare, Board board);
    }

}