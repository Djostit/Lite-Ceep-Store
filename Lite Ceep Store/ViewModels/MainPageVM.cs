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
using System.Windows.Controls;

namespace Lite_Ceep_Store.ViewModels
{
    public class MainPageVM : BindableBase
    {
        private readonly PageServiceInside _pageServiceInside;
        private readonly MessageBus _messageBus;
        private readonly PageService _pageService;
        public string CurrentBalance { get; set; }
        public Page? PageSource { get; set; } = new StorePage();
        public string LogoUsername { get; set; }
        public MainPageVM(PageServiceInside pageServiceInside, MessageBus messageBus, PageService pageService)
        {
            _pageServiceInside = pageServiceInside;
            _messageBus = messageBus;
            _pageService = pageService;

            _pageServiceInside.OnPageChanged += (page) => PageSource = page;

            _messageBus.Receive<TextMessage>(this, async message => LogoUsername = message.Text.Split(' ')[0].ToString().ToUpper().ToArray()[0].ToString());

            _messageBus.Receive<TextMessage>(this, async message => CurrentBalance = $"{message.Text.Split(' ')[1]}₽");
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
        public AsyncCommand CommandReplenishmentBalance => new(async() =>
        {
            _pageService.ChangePage(new ReplenishmentBalance());
            await _messageBus.SendTo<ReplenishmentBalanceVM>(new TextMessage(CurrentBalance));
        });
    }
}
