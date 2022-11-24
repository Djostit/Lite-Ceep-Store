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
            services.AddScoped<SingInVM>();
            services.AddScoped<SingUpVM>();
            services.AddScoped<MainPageVM>();


            services.AddSingleton<PageService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<MessageBus>();

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

    }
}
