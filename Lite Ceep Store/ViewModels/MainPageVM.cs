﻿using DevExpress.Mvvm;
using Lite_Ceep_Store.Messages;
using Lite_Ceep_Store.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lite_Ceep_Store.ViewModels
{
    public class MainPageVM : BindableBase
    {
        private readonly PageServiceInside _pageServiceInside;
        private readonly MessageBus _messageBus;
        public string HelloUsername { get; set; }
        public MainPageVM(PageService pageService, MessageBus messageBus)
        {
            _pageService = pageService;
            _messageBus = messageBus;

            //_pageService.OnPageChanged += (page) => PageSource = page;

            _messageBus.Receive<TextMessage>(this, async message => HelloUsername = $"Привет, {message.Text}!");
        }
    }
}
