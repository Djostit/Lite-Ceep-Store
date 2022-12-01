using DevExpress.Mvvm;
using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class ReplenishmentBalanceVM : BindableBase
    {
        private readonly PageService _pageService;
        public string CurrentBalance { get; set; } = Global.CurrentUser.Balance.ToString() + "₽";
        public string SelectedItem { get; set; }
        public bool isSelected { get; set; }
        public List<string> ArrayAmmount { get; set; } = new List<string>() { "50 ₽", "100 ₽", "150 ₽", "200 ₽", "500 ₽", "750 ₽", "1000 ₽", "5000 ₽" };
        public string SelectedAmmount { get; set; }
        public bool isOpen { get; set; }
        private string ErrorMessage { get; set; }
        public ReplenishmentBalanceVM(PageService pageService)
        {
            _pageService = pageService;
        }
        public AsyncCommand AddMoneyCommand => new(async () => 
        {
            Global.CurrentUser.Balance += int.Parse(SelectedAmmount.Split(' ')[0].ToString());
            CurrentBalance = Global.CurrentUser.Balance.ToString() + "₽";
            isOpen = true;
            await Task.Delay(1500);
            isOpen = false;
            
        }, bool () => 
        {
            if (!isSelected)
            {
                ErrorMessage = "Не принято условие использования";
            }
            else if (string.IsNullOrWhiteSpace(SelectedItem))
            {
                ErrorMessage = "Не выбран способ пополнения";
            }
            else if (string.IsNullOrWhiteSpace(SelectedAmmount))
            {
                ErrorMessage = "Не выбрана сумма пополнения";
            }
            else
            {
                ErrorMessage = string.Empty;
            }

            if (ErrorMessage.Equals(string.Empty))
                return true; return false;

        });
        public DelegateCommand ReturnBackCommand => new(() =>
        {
            _pageService.ChangePage(new MainPage());
        });
    }
}
