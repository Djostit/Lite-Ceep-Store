namespace Lite_Ceep_Store.ViewModels
{
    public class SuccessfulPayVM : BindableBase
    {
        private readonly MessageBus _messageBus;
        private readonly PageService _pageService;
        private readonly PageServiceInside _pageServiceInside;
        private readonly CheckService _checkService;
        public static Game Game { get; set; } = new Game();
        public string Key { get; set; } = _key;

        private static string _key;
        public SuccessfulPayVM(MessageBus messageBus, PageService pageService, CheckService checkService, PageServiceInside pageServiceInside)
        {
            _messageBus = messageBus;
            _pageService = pageService;
            _pageServiceInside = pageServiceInside;
            _checkService = checkService;

            _messageBus.Receive<GameMessage>(this, async game => Game = game.Game);

            _messageBus.Receive<TextMessage>(this, async key => _key = key.Text);
        }
        public AsyncCommand GetCheckCommand => new(async () =>
        {
            _checkService.GetCheck(Game.Title, Game.Description, Game.Price, Key);
        });
        public DelegateCommand ReturnBackCommand => new(() =>
        {
            _pageService.ChangePage(new MainPage());
        });
        public AsyncCommand GoToActivateCommand => new(async () =>
        {
            await _messageBus.SendTo<ActivationPageVM>(new TextMessage(Key));
            _pageService.ChangePage(new MainPage());
            _pageServiceInside.ChangePage(new ActivationPage());
        });
    }
}
