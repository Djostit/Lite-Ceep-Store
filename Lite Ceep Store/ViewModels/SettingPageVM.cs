using DevExpress.Mvvm;
using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.Views;
using Microsoft.Win32;
using System.IO;

namespace Lite_Ceep_Store.ViewModels
{
    public class SettingPageVM : BindableBase
    {
        private readonly PageService _pageService;
        private readonly GameService _gameService;

        public string Source { get; set; } = Path.GetFullPath($@"Assets\graph\AWbHE6aev0xvC9aeVAllAedg3g5ykf.jpg").Replace(@"\bin\Debug\net7.0-windows\", @"\");
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

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
            await _gameService.AddGameAsync(Title, Source, Description, Price);
        });
    }
}
