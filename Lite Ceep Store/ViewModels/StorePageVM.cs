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
    public class StorePageVM : BindableBase
    {
        private readonly MessageBus _messageBus;
        private readonly PageService _pageService;
        public List<Game> ItemSource { get; set; } = Global.Games;
        public Game Game
        {
            get { return GetValue<Game>(); }
            set { SetValue(value, changedCallback: OnGameChanged); }
        }
        private async void OnGameChanged()
        {
            await _messageBus.SendTo<BuyingGameVM>(new GameMessage(Game));
            _pageService.ChangePage(new BuyingGame());

        }
        public StorePageVM(MessageBus messageBus, PageService pageService)
        {
            _messageBus = messageBus;
            _pageService = pageService;
        }
    }
}
