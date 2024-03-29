﻿namespace Lite_Ceep_Store.ViewModels
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
            if (Global.CurrentUser.Balance < int.Parse(Game.Price.Split(' ')[0]))
                return;

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
