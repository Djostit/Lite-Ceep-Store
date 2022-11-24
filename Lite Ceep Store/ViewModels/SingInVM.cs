using DevExpress.Mvvm;
using Lite_Ceep_Store.Messages;
using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class SingInVM : BindableBase
    {
        private readonly PageService _pageService;
        private readonly UserService _userService;
        private readonly MessageBus _messageBus;
        public string Login { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public SingInVM(PageService pageService, UserService userService, MessageBus messageBus)
        {
            _pageService = pageService;
            _userService = userService;
            _messageBus = messageBus;
        }
        public AsyncCommand SignInCommand => new(async() =>
        {
            if (await _userService.AuthorizeUserAsync(Login, Password) == true)
            {
                await _messageBus.SendTo<MainPageVM>(new TextMessage(Login));
                _pageService.ChangePage(new MainPage());
            }
        });
        public DelegateCommand SignUpCommand => new(() =>
        {
            _pageService.ChangePage(new SignUp());
        });
    }
}
