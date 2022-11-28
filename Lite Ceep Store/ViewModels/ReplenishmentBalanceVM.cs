using DevExpress.Mvvm;
using Lite_Ceep_Store.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class ReplenishmentBalanceVM : BindableBase
    {
        public string CurretBalance { get; set; } = $"{Current_Global.CurrentUser[0].Balance}₽";
    }
}
