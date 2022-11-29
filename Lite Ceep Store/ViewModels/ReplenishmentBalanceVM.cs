using DevExpress.Mvvm;
using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Messages;
using Lite_Ceep_Store.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class ReplenishmentBalanceVM : BindableBase
    {
        public string CurrentBalance { get; set; }

        private readonly MessageBus _messageBus;
        public ReplenishmentBalanceVM(MessageBus messageBus)
        {
            _messageBus = messageBus;

            _messageBus.Receive<TextMessage>(this, async message => CurrentBalance = message.Text);

        }
    }
}
