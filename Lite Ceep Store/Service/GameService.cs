﻿

namespace Lite_Ceep_Store.Service
{
    public class GameService
    {
        private const string PATH = @"Jsons\game.json";
        private static async Task ReadGamesAsync() => Global.Games = JsonConvert.DeserializeObject<List<Game>>(await File.ReadAllTextAsync(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\")));
        private static async Task SaveGameAsync() => await File.WriteAllTextAsync(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\"), JsonConvert.SerializeObject(Global.Games, Newtonsoft.Json.Formatting.Indented));

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
        public List<Game> GetLibrary(List<UserGames> ids)
        {
            var a = new List<Game>();

            for (int i = 0; i < ids.Count; i++)
            {
                var game = Global.Games.SingleOrDefault(g => g.Id.Equals(ids[i].Id));
                a.Add(game);
            }

            return a;
        }
    }
}
