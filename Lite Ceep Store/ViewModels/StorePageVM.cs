using DevExpress.Mvvm;
using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Models;
using Lite_Ceep_Store.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class StorePageVM : BindableBase
    {
        private readonly CheckService _checkService;
        public List<Game> ItemSource { get; set; } = Global.Games;
        public Game SelectedIndex
        {
            get { return GetValue<Game>(); }
            set { SetValue(value, changedCallback: OnFirstNameChanged); }
        }
        private async void OnFirstNameChanged()
        {
            //Debug.WriteLine(SelectedIndex.Title);
            _checkService.GetCheck(SelectedIndex.Title, SelectedIndex.Description, SelectedIndex.Price, "NL9YA-PB69J-3E2AA-ICNWC-WNF66");
            //await _keyService.CreateKey(SelectedIndex.Id);

        }
        public StorePageVM(CheckService checkService)
        {
            _checkService = checkService;
        }
    }
}
