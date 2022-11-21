using System;
using System.Windows.Controls;

namespace Lite_Ceep_Store.Service
{
    public class PageService
    {
        public event Action<Page>? OnPageChanged;

        public void ChangePage(Page page) => OnPageChanged?.Invoke(page);
    }
}
