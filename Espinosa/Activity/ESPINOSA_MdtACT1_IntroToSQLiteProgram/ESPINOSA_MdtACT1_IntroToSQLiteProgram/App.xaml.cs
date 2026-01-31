using Microsoft.Extensions.DependencyInjection;

namespace ESPINOSA_MdtACT1_IntroToSQLiteProgram
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}