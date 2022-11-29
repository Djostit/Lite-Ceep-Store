using DevExpress.Mvvm;
using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.Views;
using System.Windows;
using System.Windows.Controls;

namespace Lite_Ceep_Store.ViewModels
{
    public class WindowVM : BindableBase
    {
        private readonly PageService _pageService;
        public Page? PageSource { get; set; }

        public WindowVM(PageService pageService)
        {
            _pageService = pageService;

            _pageService.OnPageChanged += (page) => PageSource = page;

            _pageService.ChangePage(new SignIn());
        }
        public DelegateCommand MinAppCommand => new(() =>
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        });
        public DelegateCommand MaxAppCommand => new(() =>
        {
            Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        });
        public DelegateCommand CloseAppCommand => new(() =>
        {
            Application.Current.Shutdown();
        });
    }
}
