namespace Lite_Ceep_Store.ViewModels
{
    public class SingInVM : BindableBase
    {
        private readonly PageService _pageService;
        private readonly UserService _userService;
        private readonly MessageBus _messageBus;
        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorMessageUsername { get; set; }
        public string ErrorMessagePassword { get; set; }
        public string ErrorMessageButton { get; set; }

        public SingInVM(PageService pageService, UserService userService, MessageBus messageBus)
        {
            _pageService = pageService;
            _userService = userService;
            _messageBus = messageBus;
        }
        public AsyncCommand SignInCommand => new(async () =>
        {
            if (await _userService.AuthorizeUserAsync(Username, Password) == true)
            {
                ErrorMessageButton = string.Empty;
                _pageService.ChangePage(new MainPage());
            }
            else
            {
                ErrorMessageButton = "Неверное имя пользователя или пароль";
            }
        }, bool () =>
        {
            if (string.IsNullOrWhiteSpace(Username))
                ErrorMessageUsername = "Обязательно";
            else if (Username.Length < 3)
                ErrorMessageUsername = "Слишком короткий";
            else
                ErrorMessageUsername = string.Empty;

            if (string.IsNullOrWhiteSpace(Password))
                ErrorMessagePassword = "Обязательно";
            else if (Password.Length < 7)
                ErrorMessagePassword = "Слишком короткий";
            else if (Password.Contains(' ')
                    || !Password.Any(char.IsDigit)
                    || !Password.Any(char.IsLetter))
                ErrorMessagePassword = "Неверный формат";
            else
                ErrorMessagePassword = string.Empty;

            if (ErrorMessageUsername.Equals(string.Empty)
                && ErrorMessagePassword.Equals(string.Empty))
                return true;
            else
                return false;
        });
        public DelegateCommand SignUpCommand => new(() =>
        {
            _pageService.ChangePage(new SignUp());
        });
    }
}
