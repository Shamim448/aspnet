namespace ChessBoardConsoleGame
{
  public abstract class Piecess {
        public string Name { get; protected set; }
        public bool IsWhite { get; protected set; }

        public Piecess(string name, bool isWhite)
        {
            Name = name;
            IsWhite = isWhite;
        }
        public abstract bool IsValidMove(Square currentSquare, Square targetSquare, Board board);
        public abstract string GetSymbol(string name);
          
    }

}