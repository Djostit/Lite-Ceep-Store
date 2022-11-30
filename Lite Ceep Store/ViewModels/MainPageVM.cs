using DevExpress.Mvvm;
using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.Views;
using System.Linq;
using System.Windows.Controls;

namespace Lite_Ceep_Store.ViewModels
{
    public class MainPageVM : BindableBase
    {
        private readonly PageServiceInside _pageServiceInside;
        private readonly MessageBus _messageBus;
        private readonly PageService _pageService;
        public string CurrentBalance { get; set; } = Current_Global.CurrentUser.Balance.ToString() + "₽";
        public Page? PageSource { get; set; } = new StorePage();
        public string? LogoUsername { get; set; } = Current_Global.CurrentUser.Username == null ? Current_Global.CurrentUser.Username : Current_Global.CurrentUser.Username.ToUpper().ToArray()[0].ToString();
        public MainPageVM(PageServiceInside pageServiceInside, MessageBus messageBus, PageService pageService)
        {
            _pageServiceInside = pageServiceInside;
            _messageBus = messageBus;
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
    }
}
