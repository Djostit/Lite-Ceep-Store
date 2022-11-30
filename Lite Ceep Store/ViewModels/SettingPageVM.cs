using DevExpress.Mvvm;
using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class SettingPageVM : BindableBase
    {
        private readonly PageService _pageService;
        public SettingPageVM(PageService pageService)
        {
            _pageService = pageService;
        }
        public DelegateCommand ReturnBackCommand => new(() =>
        {
            _pageService.ChangePage(new MainPage());
        });
    }
}
