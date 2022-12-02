using DevExpress.Mvvm;
using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Messages;
using Lite_Ceep_Store.Models;
using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class BuyingGameVM : BindableBase
    {
        private readonly MessageBus _messageBus;
        private readonly PageService _pageService;
        private readonly CheckService _checkService;
        private readonly KeyService _keyService;
        public static Game Game { get; set; } = new Game();
        public string Username { get; set; } = Global.CurrentUser.Username;
        public string Title { get; set; } = Game.Title;
        public string sourceImage { get; set; } = Game.DisplayedImage;
        public string Description { get; set; } = Game.Description;
        public string Price { get; set; } = Game.Price;
        public bool BoolCheck { get; set; }
        public BuyingGameVM(MessageBus messageBus, PageService pageService, CheckService checkService, KeyService keyService)
        {
            _messageBus = messageBus;
            _pageService = pageService;
            _checkService = checkService;
            _keyService = keyService;

            _messageBus.Receive<GameMessage>(this, async game => Game = game.Game);
            
        }
        public DelegateCommand ReturnBackCommand => new(() => 
        {
            _pageService.ChangePage(new MainPage());
        });

        public AsyncCommand BuyGameCommand => new(async() => 
        {
            Global.CurrentUser.Balance -= int.Parse(Game.Price.Split(' ')[0]);
            string key = await _keyService.CreateKey(Game.Id);

            if (BoolCheck)
                _checkService.GetCheck(Game.Title, Game.Description, Game.Price, key);
        });
    }
}
