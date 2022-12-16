using System.Diagnostics;

namespace Lite_Ceep_Store.ViewModels
{
    public class SettingPageVM : BindableBase
    {
        private readonly PageService _pageService;
        private readonly GameService _gameService;

        #region Pass
        private const string AdmPass = "$2a$11$1TS7rLdzAiypFZAkEUs9XOKPGVm7Y1s/ILlRVqCoSi1O82ZYq/SpG";
        #endregion

        public string Source { get; set; } = Path.GetFullPath($@"Assets\graph\AWbHE6aev0xvC9aeVAllAedg3g5ykf.jpg").Replace(@"\bin\Debug\net7.0-windows\", @"\");
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsEnabled { get; set; } = false;
        public Visibility IsVisible { get; set; } = Visibility.Visible;
        public string ErrorMessage { get; set; } 

        public string AdminPassword
        {
            get { return GetValue<string>(); }
            set { SetValue(value, changedCallback: OnPasswordChanged); }
        }

        private void OnPasswordChanged()
        {
            if (AdminPassword.Length == 9
                && BCrypt.Net.BCrypt.Verify(AdminPassword, AdmPass))
            {
                IsEnabled = true;
                IsVisible = Visibility.Collapsed;
            }
        }

        public SettingPageVM(PageService pageService, GameService gameService)
        {
            _pageService = pageService;
            _gameService = gameService;
        }
        public DelegateCommand ReturnBackCommand => new(() =>
        {
            _pageService.ChangePage(new MainPage());
        });
        public DelegateCommand ChoiceSoure => new(() =>
        {
            OpenFileDialog ofd = new()
            {
                Filter = "Изображение (*.jpg)|*.jpg",
                Title = "Открытие картинки"
            };
            if (ofd.ShowDialog() is true)
            {
                Source = ofd.FileName;
            }
        });
        public AsyncCommand SaveCommand => new(async () =>
        {
            await _gameService.AddGameAsync(Title.Trim(), Source, Description.Trim(), Price);
            Source = Path.GetFullPath($@"Assets\graph\AWbHE6aev0xvC9aeVAllAedg3g5ykf.jpg").Replace(@"\bin\Debug\net7.0-windows\", @"\");
            Title = string.Empty;
            Description = string.Empty;
            Price = 0;

        }, bool () =>
        {
            if (Source.Contains("AWbHE6aev0xvC9aeVAllAedg3g5ykf.jpg"))
                ErrorMessage = "Не выбрано изображение";
            else if (string.IsNullOrWhiteSpace(Title))
                ErrorMessage = "Название не может быть пустым";
            else if (Title.Trim().Length < 3)
                ErrorMessage = "Название короткое";
            else if (Global.Games.SingleOrDefault(g => g.Title.ToLower().Equals(Title.ToLower())) != null)
                ErrorMessage = "Игра с таким названием уже существует";
            else if (string.IsNullOrWhiteSpace(Description))
                ErrorMessage = "Описание не может быть пустым";
            else if (Description.Trim().Length < 20)
                ErrorMessage = "Описание короткое";
            else if (Price < 25)
                ErrorMessage = "Игра не может стоить меньше 25 ₽";
            else
                ErrorMessage = string.Empty;

            if (ErrorMessage.Equals(string.Empty))
                return true; return false;
        });
    }
}