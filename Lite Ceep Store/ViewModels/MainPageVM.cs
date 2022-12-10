using System.Diagnostics;

namespace Lite_Ceep_Store.ViewModels
{
    public class MainPageVM : BindableBase
    {
        private readonly PageServiceInside _pageServiceInside;
        private readonly PageService _pageService;
        public string CurrentBalance { get; set; } = Global.CurrentUser.Balance.ToString() + "₽";
        public Page? PageSource { get; set; } = new StorePage();
        public string? Username { get; set; } = Global.CurrentUser.Username == null ? Global.CurrentUser.Username : new CultureInfo("ru-RU").TextInfo.ToTitleCase(Global.CurrentUser.Username);
        public string? LogoUsername { get; set; } = Global.CurrentUser.Username == null ? Global.CurrentUser.Username : Global.CurrentUser.Username.ToUpper().ToArray()[0].ToString();
        public MainPageVM(PageServiceInside pageServiceInside, PageService pageService)
        {
            _pageServiceInside = pageServiceInside;
            _pageService = pageService;

            _pageServiceInside.OnPageChanged += (page) => PageSource = page;
        }
        public DelegateCommand CommandStore => new(() =>
        {
            _pageServiceInside.ChangePage(new StorePage());
        });
        public DelegateCommand CommandLibrary => new(() =>
        {
            _pageServiceInside.ChangePage(new StoreLibrary());
        });
        public DelegateCommand CommandActivation => new(() =>
        {
            _pageServiceInside.ChangePage(new ActivationPage());
        });
        public DelegateCommand CommandReplenishmentBalance => new(() =>
        {
            _pageService.ChangePage(new ReplenishmentBalance());
        });
        public DelegateCommand CommandSetting => new(() =>
        {
            _pageService.ChangePage(new SettingPage());
        });
        public AsyncCommand CommandExit => new(async () =>
        {
            await UserService.SaveCurrentUserAsync();
            Global.CurrentUser = null;

            _pageService.ChangePage(new SignIn());
        });
    }
}
