using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.Service
{
    public class UserService
    {
        public static List<User> Users { get; set; } = new List<User>();

        private const string PATH = @"Jsons\user.json";
        private async Task ReadUsersAsync() => Users = JsonConvert.DeserializeObject<List<User>>(await File.ReadAllTextAsync(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\")));
        private void ReadUsers() => Users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\")));
        private async Task SaveUserAsync() => await File.WriteAllTextAsync(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\"), JsonConvert.SerializeObject(Users, Formatting.Indented));
        public List<User> ReceiveUsernames()
        {
            ReadUsers();
            return Users;
        }
        public async Task<bool> AuthorizeUserAsync(string username, string password)
        {
            await ReadUsersAsync();

            var user = Users.SingleOrDefault(u => u.Username.Equals(username));

            if (user == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
        public async Task AddUserAsync(string name, string lastName, string birthday, string country, string username, string password)
        {
            await ReadUsersAsync();

            Users.Add( new User
                {
                    Name = name,
                    LastName = lastName,
                    Birthday = birthday,
                    Country = country,
                    Username = username,
                    Password = BCrypt.Net.BCrypt.HashPassword(password)
                });

            await SaveUserAsync();
        }

        public async Task<bool> CheckUsernameAsync(string Username)
        {
            await ReadUsersAsync();
            return Users.SingleOrDefault(u => u.Username.Equals(Username)) != null;
        }
    }
}
