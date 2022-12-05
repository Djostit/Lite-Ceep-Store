using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lite_Ceep_Store.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text) == null || !e.Text.All(char.IsDigit))
            {
                e.Handled = true;
            }
        }

        private void NoAllowedSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
