namespace ConnectFourLibrary
{
    public interface IGame
    {
        int RegisterMoveToBoard(int move);
        void SwapPlayerTurns();
        /// <summary>
        /// Help text
        /// </summary>
        /// <returns>If true then game finisahed, otherwise the game continues</returns>
        bool CheckConnectedFour();
        TokenColour PlayerTurn { get; set; }
        int MaxPossibleMoves { get; set; }
        public IBoard Board { get; }
    }
}
