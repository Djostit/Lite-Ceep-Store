using Lite_Ceep_Store.Service;
using Lite_Ceep_Store.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Lite_Ceep_Store
{
    internal class ViewModelLocator
    {
        private static ServiceProvider _provider;
        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<WindowVM>();
            services.AddTransient<SingInVM>();
            services.AddTransient<SingUpVM>();

            services.AddTransient<MainPageVM>();
            services.AddTransient<StorePageVM>();
            services.AddTransient<LibraryPageVM>();
            services.AddTransient<ActivationPageVM>();
            services.AddTransient<ReplenishmentBalanceVM>();
            services.AddTransient<SettingPageVM>();

            services.AddTransient<BuyingGameVM>();


            services.AddSingleton<PageService>();
            services.AddSingleton<PageServiceInside>();
            services.AddSingleton<UserService>();
            services.AddSingleton<GameService>();
            services.AddSingleton<MessageBus>();
            services.AddSingleton<CheckService>();
            services.AddSingleton<KeyService>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }
        public WindowVM WindowVM => _provider.GetRequiredService<WindowVM>();
        public SingInVM SingInVM => _provider.GetRequiredService<SingInVM>();
        public SingUpVM SingUpVM => _provider.GetRequiredService<SingUpVM>();
        public MainPageVM MainPageVM => _provider.GetRequiredService<MainPageVM>();
        public StorePageVM StorePageVM => _provider.GetRequiredService<StorePageVM>();
        public LibraryPageVM LibraryPageVM => _provider.GetRequiredService<LibraryPageVM>();
        public ActivationPageVM ActivationPageVM => _provider.GetRequiredService<ActivationPageVM>();
        public ReplenishmentBalanceVM ReplenishmentBalanceVM => _provider.GetRequiredService<ReplenishmentBalanceVM>();
        public SettingPageVM SettingPageVM => _provider.GetRequiredService<SettingPageVM>();
        public BuyingGameVM BuyingGameVM => _provider.GetRequiredService<BuyingGameVM>();

    }
}
