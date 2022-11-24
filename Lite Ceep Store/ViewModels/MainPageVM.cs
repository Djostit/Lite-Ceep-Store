using DevExpress.Mvvm;
using Lite_Ceep_Store.Messages;
using Lite_Ceep_Store.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class MainPageVM : BindableBase
    {
        private readonly PageService _pageService;
        private readonly MessageBus _messageBus;
        private string _username;
        public MainPageVM(PageService pageService, MessageBus messageBus)
        {
            _pageService = pageService;
            _messageBus = messageBus;

            _messageBus.Receive<TextMessage>(this, async message => _username = message.Text);
        }
        public DelegateCommand Send => new(() => 
        {
            Debug.WriteLine(_username);
        });
    }
}
