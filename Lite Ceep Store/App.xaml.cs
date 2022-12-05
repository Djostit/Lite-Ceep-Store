using Lite_Ceep_Store.Service;
using System.Windows;

namespace Lite_Ceep_Store
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ViewModelLocator.Init();

            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await UserService.SaveCurrentUserAsync();
            base.OnExit(e);
        }
    }
}
