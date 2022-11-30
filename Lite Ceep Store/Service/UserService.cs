using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Messages;
using Lite_Ceep_Store.Models;
using Lite_Ceep_Store.ViewModels;
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
        private static async Task ReadUsersAsync() => Users = JsonConvert.DeserializeObject<List<User>>(await File.ReadAllTextAsync(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\")));
        private static async Task SaveUserAsync() => await File.WriteAllTextAsync(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\"), JsonConvert.SerializeObject(Users, Formatting.Indented));
        public List<User> GetUsernames()
        {
            ReadUsersAsync().GetAwaiter();
            return Users;
        }
        public async Task<bool> AuthorizeUserAsync(string username, string password)
        {
            await ReadUsersAsync();

            var user = Users.SingleOrDefault(u => u.Username.Equals(username));

            if (user == null)
                return false;

            Current_Global.CurrentUser = user;

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
                        Password = BCrypt.Net.BCrypt.HashPassword(password),
                        Balance = 0
                    });

            await SaveUserAsync();
        }

        public async Task<bool> CheckUsernameAsync(string Username)
        {
            await ReadUsersAsync();
            return Users.SingleOrDefault(u => u.Username.Equals(Username)) is not null;
        }
        public static async Task SaveCurrentUserAsync()
        {
            if (Current_Global.CurrentUser.Username is null)
                return;

            int index = Users.FindIndex(u => u.Equals(Current_Global.CurrentUser));
            Users[index] = Current_Global.CurrentUser;
            await SaveUserAsync();
        }
    }
}
