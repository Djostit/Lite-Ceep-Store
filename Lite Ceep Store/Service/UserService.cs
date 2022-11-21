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
        private const string PATH = @"Jsons\user.json";
        private static async Task ReadUsers() => Global.Users = JsonConvert.DeserializeObject<List<User>>(await File.ReadAllTextAsync(Path.GetFullPath(PATH).Replace(@"\bin\Debug\net7.0-windows\", @"\")));
        private static async Task SaveUser() => await File.WriteAllTextAsync(Path.GetFullPath(PATH).Replace(@"\bin\Debug\net7.0-windows\", @"\"), JsonConvert.SerializeObject(Global.Users, Formatting.Indented));

        public async Task<bool> AuthorizeUser(string Username, string Password)
        {
            await ReadUsers();

            var user = Global.Users.SingleOrDefault(u => u.Username.Equals(Username));

            if (user == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(Password, user.Password);
        }

        public async Task AddUser(string Name, string LastName, string Birthday, string Country, string Username, string Password)
        {
            await ReadUsers();

            Global.Users.Add( new User
                {
                    Name = Name,
                    LastName = LastName,
                    Birthday = Birthday,
                    Country = Country,
                    Username = Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(Password)
                });

            await SaveUser();
        }
    }
}
