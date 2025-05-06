using MyIdentityApp.Services;

namespace MyIdentityApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            // ✅ Add global exception handlers here
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                AppLogger.LogError<App>("Unhandled exception", ex ?? new Exception("Unknown error"));
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                AppLogger.LogError<App>("Unobserved task exception", e.Exception);
                e.SetObserved(); // Prevents app crash
            };
        }
    }
}
