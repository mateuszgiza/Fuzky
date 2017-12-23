using Autofac;
using Fuzky.Core;
using Fuzky.UI.Windows.Main;
using System;
using System.Windows;
using System.Windows.Threading;
using MainWindow = Fuzky.UI.Windows.Main.MainWindow;

namespace Fuzky.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer container;

        public App()
        {
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                this.container = Bootstrapper.Initialize();
            }
            catch (Exception ex)
            {

            }

            if (this.container != null)
            {
                IMainWindowViewModel window = this.container.Resolve<IMainWindowViewModel>();
                MainWindow = (MainWindow)window.Window;
                MainWindow?.ShowDialog();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.container.Dispose();
            base.OnExit(e);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                $"An unhandled exception occured!{Environment.NewLine}{e.Exception.Message}",
                "Exception was thrown!",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation);

            e.Handled = true;
        }
    }
}
