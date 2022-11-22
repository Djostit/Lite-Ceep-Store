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
        //public static async Task ReadUsers() => Users = JsonConvert.DeserializeObject<List<User>>(await File.ReadAllTextAsync(Path.GetFullPath(PATH).Replace(@"\bin\Debug\net7.0-windows\", @"\")));
        public static async Task ReadUsers() => Users = JsonConvert.DeserializeObject<List<User>>(await File.ReadAllTextAsync("C:\\Users\\Djostit\\Source\\Repos\\Djostit\\Lite-Ceep-Store\\Lite Ceep Store\\Jsons\\user.json"));

        public static async Task SaveUser() => await File.WriteAllTextAsync(Path.GetFullPath(PATH).Replace(@"\bin\Debug\net7.0-windows\", @"\"), JsonConvert.SerializeObject(Users, Formatting.Indented));

        public static async Task<bool> AuthorizeUser(string Username, string Password)
        {
            await ReadUsers();

            var user = Users.SingleOrDefault(u => u.Username.Equals(Username));

            if (user == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(Password, user.Password);
        }

        public async Task AddUser(string Name, string LastName, string Birthday, string Country, string Username, string Password)
        {
            await ReadUsers();

            Users.Add( new User
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

        public async Task<bool> CheckUsernameAsync(string Username)
        {
            await ReadUsers();
            return Users.SingleOrDefault(u => u.Username.Equals(Username)) != null;
        }
    }
}
