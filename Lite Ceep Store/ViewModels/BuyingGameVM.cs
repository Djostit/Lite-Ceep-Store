using DevExpress.Mvvm;
using Lite_Ceep_Store.Messages;
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
    public class BuyingGameVM : BindableBase
    {
        private readonly MessageBus _messageBus;
        private Game Game;
        public BuyingGameVM(MessageBus messageBus)
        {
            _messageBus= messageBus;
            _messageBus.Receive<GameMessage>(this, async game => /*Game = game.Game*/ Debug.WriteLine(game.Game.Title));
            
        }
    }
}
