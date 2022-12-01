using DevExpress.Mvvm;
using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lite_Ceep_Store.ViewModels
{
    public class SettingPageVM : BindableBase
    {
        private readonly PageService _pageService;
        private readonly GameService _gameService;

        public string Source { get; set; }
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
        public AsyncCommand SaveCommand => new(async() => 
        {
            await _gameService.AddGameAsync(Title, Source, Description, Price);
        });
    }
}
