using Lite_Ceep_Store.Models;

namespace Lite_Ceep_Store.Messages
{
    public class UserMessage : IMessage
    {
        public User User { get; set; }
        public UserMessage(User user) => User = user;
    }
}
