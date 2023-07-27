namespace ConnectFourWeb.Data
{
    public class MoveData
    {
        public int Col { get; set; }
        public string GameId { get; set; }

        public MoveData(int col, string gameId)
        {
            Col = col;
            GameId = gameId;
        }
    }
}
