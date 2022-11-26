using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lite_Ceep_Store.Service
{
    public class PageServiceInside
    {
        public event Action<Page>? OnPageChanged;

        public void ChangePage(Page page) => OnPageChanged?.Invoke(page);
    }
}
