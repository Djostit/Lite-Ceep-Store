using Lite_Ceep_Store.Models;

namespace Lite_Ceep_Store.Messages
{
    public class GameMessage : IMessage
    {
        public Game Game { get; set; }
        public GameMessage(Game game) => Game = game;
    }
}
