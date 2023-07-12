namespace ConnectFourLibrary
{
    public interface IGame
    {
        void InitialiseBoard();
        int RegisterMoveToBoard(int move);
        void SwapPlayerTurns();
        bool CheckConnectedFour();
        int PlayerTurn { get; set; }
        public IBoard Board { get; }
    }
}
