using DevExpress.Mvvm;
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
        public string Login { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public SingInVM(PageService pageService, UserService userService)
        {
            _pageService = pageService;
            _userService = userService;
        }
        public DelegateCommand SignInCommand => new(() =>
        {
            Debug.WriteLine(_userService.AuthorizeUser(Login, Password));
        });
        public DelegateCommand SignUpCommand => new(() =>
        {
            _pageService.ChangePage(new SignUp());
        });
    }
}
