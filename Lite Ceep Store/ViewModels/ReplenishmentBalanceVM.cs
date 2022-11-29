using DevExpress.Mvvm;
using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Messages;
using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lite_Ceep_Store.ViewModels
{
    public class ReplenishmentBalanceVM : BindableBase
    {
        private readonly PageService _pageService;
        public string CurrentBalance { get; set; } = Current_Global.CurrentUser.Balance.ToString() + "₽";
        public bool isSelected { get; set; }
        public List<string> ArrayAmmount { get; set; } = new List<string>() { "50 ₽", "100 ₽", "150 ₽", "200 ₽", "500 ₽", "750 ₽", "1000 ₽", "5000 ₽" };
        public string SelectedAmmount { get; set; }
        public bool isOpen { get; set; }
        public ReplenishmentBalanceVM(PageService pageService)
        {
            _pageService = pageService;
        }
        public AsyncCommand AddMoneyCommand => new(async () => 
        {
            Current_Global.CurrentUser.Balance += int.Parse(SelectedAmmount.Split(' ')[0].ToString());
            isOpen = true;
            await Task.Delay(1500);
            isOpen = false;
            CurrentBalance = Current_Global.CurrentUser.Balance.ToString() + "₽";
        }, bool () => 
        {
            if (isSelected && !string.IsNullOrWhiteSpace(SelectedAmmount))
                return true; return false;
        });
        public DelegateCommand ReturnBackCommand => new(() =>
        {
            _pageService.ChangePage(new MainPage());
        });
    }
}
