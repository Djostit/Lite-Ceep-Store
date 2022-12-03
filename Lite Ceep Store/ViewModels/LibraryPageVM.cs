using DevExpress.Mvvm;
using Lite_Ceep_Store.Assets;
using Lite_Ceep_Store.Models;
using Lite_Ceep_Store.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.ViewModels
{
    public class LibraryPageVM : BindableBase
    {
        private readonly GameService _gameService;
        public static List<Game> ItemSource { get; set; } = new List<Game>();
        public LibraryPageVM(GameService gameService)
        {
            _gameService = gameService;
            ItemSource = _gameService.GetLibrary(Global.CurrentUser.Games);
        }
    }
}
