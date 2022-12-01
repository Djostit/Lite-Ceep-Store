using DevExpress.Mvvm;
using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Models;
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
        public List<Game> ItemSource { get; set; } = Global.Games;
        public int SelectedIndex { get; set; }

        public DelegateCommand BuyGame => new(() => 
        {
            Debug.WriteLine("123");
        });
    }
}
