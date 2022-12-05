using System.Collections.Generic;

namespace Lite_Ceep_Store.Models
{
    public class User
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Birthday { get; set; }
        public string? Country { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? Balance { get; set; }
        public List<UserGames> Games { get; set; } = new();
    }
}
