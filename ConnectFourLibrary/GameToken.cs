namespace ConnectFourLibrary
{
    public class GameToken
    {
        public TokenColour color;

        public GameToken()
        {
            color = TokenColour.Blank;
        }

        public GameToken(TokenColour color)
        {
            this.color = color;
        }
    }
}
