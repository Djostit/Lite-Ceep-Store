using DevExpress.Mvvm;
using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Messages;
using Lite_Ceep_Store.Models;
using Lite_Ceep_Store.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class ActivationPageVM : BindableBase
    {
        private readonly KeyService _keyService;
        private readonly MessageBus _messageBus;
        public string Key { get; set; } = _key;
        private static string _key;

        public ActivationPageVM(KeyService keyService, MessageBus messageBus)
        {
            _keyService = keyService;
            _messageBus = messageBus;

            _messageBus.Receive<TextMessage>(this, async key => _key = key.Text);
        }
        public AsyncCommand ActivateCommand => new(async () =>
        {
            if (await _keyService.ActivateKey(Key))
            {
                Global.CurrentUser.Games.Add(new UserGames
                {
                    Id = await _keyService.FindIdGame(Key)
                });
            }
        }, bool () => { return Key is not null && Key.Length == 29; });
    }
}
