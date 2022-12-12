namespace Lite_Ceep_Store.ViewModels
{
    public class ActivationPageVM : BindableBase
    {
        private readonly KeyService _keyService;
        private readonly MessageBus _messageBus;
        public string Key { get; set; } = _key;
        private static string _key;
        public string ErrorMessageKey { get; set; }
        public bool isOpen { get; set; }
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
                ErrorMessageKey = string.Empty;
            }
            else
                ErrorMessageKey = "Неверный ключ или игра уже активирована";
            isOpen = true;
            await Task.Delay(1500);
            isOpen = false;
        }, bool () => { return Key is not null && Key.Length == 29; });
    }
}
