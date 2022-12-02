﻿using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.Service
{
    public class KeyService
    {
        public static List<Key> keys = new List<Key>();

        private static readonly Random rnd = new();

        private const string KEY_CHARS = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789";

        private const string PATH = @"Jsons\key.json";

        private static async Task ReadKeysAsync() => keys = JsonConvert.DeserializeObject<List<Key>>(await File.ReadAllTextAsync(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\")));
        private static async Task SaveKeyAsync() => await File.WriteAllTextAsync(Path.GetFullPath(PATH)
            .Replace(@"\bin\Debug\net7.0-windows\", @"\"), JsonConvert.SerializeObject(keys, Formatting.Indented));
        public async Task CreateKey(int ID)
        {
            await ReadKeysAsync();
            keys.Add(new Key 
            {
                ID = ID,
                KEY = $"{CreateKeyPart(rnd)}-{CreateKeyPart(rnd)}-{CreateKeyPart(rnd)}-{CreateKeyPart(rnd)}-{CreateKeyPart(rnd)}",
                Status = Key.Status_key.Not_activated
            });
            await SaveKeyAsync();
        }

        private static string CreateKeyPart(Random rnd)
        {
            string value = string.Empty;
            for (int i = 0; i < 5; i++) { value += KEY_CHARS[rnd.Next(0, KEY_CHARS.Length)]; }
            return value;
        }

        public async Task<bool> ActivateKey(string key)
        {
            await ReadKeysAsync();

            int index = keys.FindIndex(k => k.KEY.Equals(key));

            if (keys.SingleOrDefault(u => u.KEY.Equals(key)).Status.Equals(Key.Status_key.Actived))
                return false;

            keys[index].Status = Key.Status_key.Actived;
            await SaveKeyAsync();
            return true;
        }
    }
}
