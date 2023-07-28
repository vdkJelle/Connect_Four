namespace ConnectFourLibrary
{
    public interface IGame
    {
        MoveResult CalculateMove(int move);
        bool GameEnded { get; set; }
        IPlayer PlayerTurn { get; set; }
        int MaxPossibleMoves { get; set; }
        public IBoard Board { get; }
        public IPlayer PlayerOne { get; set; }
        public IPlayer PlayerTwo { get; set; }
    }
}
