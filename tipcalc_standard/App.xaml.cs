using System;
using Microsoft.Extensions.DependencyInjection;
using tipcalc_core.Interfaces;
using tipcalc_core.Models;
using tipcalc_data.Interfaces;
using tipcalc_data.Models;
using tipcalc_standard.Views;

using Xamarin.Forms;

namespace tipcalc
{
    public partial class App : Application
    {
        private readonly IFileHelper _fileHelper = DependencyService.Get<IFileHelper>();

        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();
            StartupConfiguration();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        
        private void StartupConfiguration()
        {
            var services = new ServiceCollection();
            services.AddTransient<ITipCalculator, TipCalculator>();
            services.AddTransient<ITipCalcTransaction, TipCalcTransaction>();
            services.AddTransient<ITipDatabase>(s => new TipDatabase(_fileHelper));
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
