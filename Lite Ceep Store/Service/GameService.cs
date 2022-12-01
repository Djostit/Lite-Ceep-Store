using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.Service
{
    public class GameService
    {
        private const string PATH = @"Jsons\game.json";
        private static async Task ReadGamesAsync() => Global.Games = JsonConvert.DeserializeObject<List<Game>>(await File.ReadAllTextAsync(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\")));
        private static async Task SaveGameAsync() => await File.WriteAllTextAsync(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\"), JsonConvert.SerializeObject(Global.Games, Formatting.Indented));

        public async Task AddGameAsync(string title, string imageSource, string description, int price)
        {
            await ReadGamesAsync();
            string hashImage = BCrypt.Net.BCrypt.HashPassword(imageSource).Substring(13, 30);

            File.Copy(imageSource, $"{Path.GetFullPath(@"Assets\graph\").Replace(@"\bin\Debug\net7.0-windows\", @"\")}{$"{hashImage}.jpg"}");

            Global.Games.Add(new Game
            {
                Id = Global.Games.Count + 1,
                Title = title,
                Image = hashImage + ".jpg",
                Description = description,
                Price = $"{price} ₽"
            });

            await SaveGameAsync();
        }

        //public async Task<bool> CheckUsernameAsync(string Username)
        //{
        //    await ReadUsersAsync();
        //    return Users.SingleOrDefault(u => u.Username.Equals(Username)) is not null;
        //}
        //public static async Task SaveCurrentUserAsync()
        //{
        //    if (Current_Global.CurrentUser.Username is null)
        //        return;

        //    int index = Users.FindIndex(u => u.Equals(Current_Global.CurrentUser));
        //    Users[index] = Current_Global.CurrentUser;
        //    await SaveUserAsync();
        //}
    }
}
