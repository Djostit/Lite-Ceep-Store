using Lite_Ceep_Store.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Lite_Ceep_Store.Assets
{
    public static class Global
    {
        public static User CurrentUser { get; set; } = new User();
        public static List<Game> Games { get; set; } = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(Path.GetFullPath(@"Jsons\game.json")
            .Replace(@"\bin\Debug\net7.0-windows\", @"\")));
    }
}
