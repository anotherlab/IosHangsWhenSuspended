using System.Diagnostics;

namespace IosHangsWhenSuspended
{
    public partial class App : Application
    {
        public bool QuitInsteadOfSleep { get; set; } = false;


        public App()
        {
            Console.WriteLine("App: Create");
            InitializeComponent();

            // Set to true to have the app terminate instead of suspending on iOS
            // QuitInsteadOfSleep = true;

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            Console.WriteLine("App: OnStart");
        }

        protected override void OnSleep()
        {
            // Is called the first time the app is suspended
            Console.WriteLine("App: OnSleep");


            if (QuitInsteadOfSleep)
            {
                // This will cause the app to quit instead of suspending
                // The app will be restarted from scratch when the app is resumed
#if IOS
                System.Environment.Exit(0);
#endif
            }
            else
            {
                base.OnSleep();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            // Does not get called when the app is resumed on iOS
            // Gets called repeatedly on Windows while app is minimized
            Console.WriteLine("App: OnResume");
        }
    }

}
