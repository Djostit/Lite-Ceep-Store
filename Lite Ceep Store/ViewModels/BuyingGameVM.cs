﻿namespace Lite_Ceep_Store.ViewModels
{
    public class BuyingGameVM : BindableBase
    {
        private readonly MessageBus _messageBus;
        private readonly PageService _pageService;
        private readonly KeyService _keyService;
        public static Game Game { get; set; } = new Game();
        public string Username { get; set; } = Global.CurrentUser.Username == null ? Global.CurrentUser.Username : new CultureInfo("ru-RU").TextInfo.ToTitleCase(Global.CurrentUser.Username);
        public string Title { get; set; } = Game.Title;
        public string sourceImage { get; set; } = Game.DisplayedImage;
        public string Description { get; set; } = Game.Description;
        public string Price { get; set; } = Game.Price;
        public bool BoolCheck { get; set; }
        public BuyingGameVM(MessageBus messageBus, PageService pageService, KeyService keyService)
        {
            _messageBus = messageBus;
            _pageService = pageService;
            _keyService = keyService;

            _messageBus.Receive<GameMessage>(this, async game => Game = game.Game);

        }
        public DelegateCommand ReturnBackCommand => new(() =>
        {
            _pageService.ChangePage(new MainPage());
        });

        public AsyncCommand BuyGameCommand => new(async () =>
        {
            Global.CurrentUser.Balance -= int.Parse(Game.Price.Split(' ')[0]);
            string key = await _keyService.CreateKey(Game.Id);

            await _messageBus.SendTo<SuccessfulPayVM>(new GameMessage(Game));
            await _messageBus.SendTo<SuccessfulPayVM>(new TextMessage(key));

            _pageService.ChangePage(new SuccessfulPay());
        });
    }
}
