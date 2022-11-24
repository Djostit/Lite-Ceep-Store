using DevExpress.Mvvm;
using Lite_Ceep_Store.Models;
using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class SingUpVM : BindableBase
    {
        private readonly PageService _pageService;
        private readonly UserService _userService;

        public ObservableCollection<Country> Country { get; set; }
        = new ObservableCollection<Country>(JsonConvert.DeserializeObject<List<Country>>(File
            .ReadAllText(Path.GetFullPath(@"Jsons\countries.json")
            .Replace(@"\bin\Debug\net7.0-windows\", @"\"))));

        public DateTime DateStart { get; set; } = DateTime.Now.AddYears(-100);
        public DateTime DateEnd { get; set; } = DateTime.Now.AddYears(-12);
        public Country SelectedCountry { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string ErrorMessageName { get; set; }
        public string ErrorMessageLastName { get; set; }
        public string ErrorMessageBirthday { get; set; }
        public string ErrorMessageUsername { get; set; }
        public string ErrorMessagePassword { get; set; }
        public string ErrorMessageRepeatPassword { get; set; }
        public List<User> usernames { get; set; } = new();

        public SingUpVM(PageService pageService, UserService userService)
        {
            _pageService = pageService;
            _userService = userService;
            SelectedCountry = Country.SingleOrDefault(c => c.code.Equals(CultureInfo.CurrentCulture.Name.Split('-')[1]));
            usernames = _userService.ReceiveUsernames();
        }
        public AsyncCommand SignUpCommand => new(async () => 
        {
            await _userService.AddUserAsync(Name, LastName, new DateOnly(int.Parse(Birthday.Split('.')[2]),
                                                                        int.Parse(Birthday.Split('.')[0]),
                                                                        int.Parse(Birthday.Split('.')[1]))
                                                                        .ToString(), SelectedCountry.name, Username, Password);
            _pageService.ChangePage(new SignIn());
        }, bool () =>
        {
            if (string.IsNullOrWhiteSpace(Name))
                ErrorMessageName = "Обязательно";
            else
                ErrorMessageName = string.Empty;

            if (string.IsNullOrWhiteSpace(LastName))
                ErrorMessageLastName = "Обязательно";
            else
                ErrorMessageLastName = string.Empty;

            if (string.IsNullOrWhiteSpace(Birthday))
                ErrorMessageBirthday = "Обязательно";
            else if (int.Parse(Birthday.Split('.')[2].Split(' ')[0]) > DateEnd.Year)
                ErrorMessageBirthday = "Неверный формат";
            else if (int.Parse(Birthday.Split('.')[2].Split(' ')[0]) < DateStart.Year)
                ErrorMessageBirthday = "Неверный формат";
            else
                ErrorMessageBirthday = string.Empty;

            if (string.IsNullOrWhiteSpace(Username))
                ErrorMessageUsername = "Обязательно";
            else if (Username.Length < 3)
                ErrorMessageUsername = "Слишком короткий";
            else if (usernames.SingleOrDefault(u => u.Username.Equals(Username)) != null)
                ErrorMessageUsername = "Уже существует";
            else
                ErrorMessageUsername = string.Empty;

            if (string.IsNullOrWhiteSpace(Password))
                ErrorMessagePassword = "Обязательно";
            else if (Password.Length < 7)
                ErrorMessagePassword = "Слишком короткий";
            else if (Password.Contains(' ')
                    || !Password.Any(char.IsDigit)
                    || !Password.Any(char.IsLetter))
                ErrorMessagePassword = "Неверный формат";
            else
                ErrorMessagePassword = string.Empty;

            if (string.IsNullOrWhiteSpace(RepeatPassword)
                || !string.IsNullOrWhiteSpace(RepeatPassword)
                && !RepeatPassword.Equals(Password))
                ErrorMessageRepeatPassword = "Пароли не совпадают";
            else
                ErrorMessageRepeatPassword = string.Empty;

            if (ErrorMessageName.Equals(string.Empty)
                && ErrorMessageLastName.Equals(string.Empty)
                && ErrorMessageBirthday.Equals(string.Empty)
                && ErrorMessageUsername.Equals(string.Empty)
                && ErrorMessagePassword.Equals(string.Empty)
                && ErrorMessageRepeatPassword.Equals(string.Empty))
                return true;
            else
                return false;
        });
        public DelegateCommand SignInCommand => new(() =>
        {
            _pageService.ChangePage(new SignIn());
        });
    }
}
