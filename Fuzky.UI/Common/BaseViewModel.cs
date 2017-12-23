using Autofac;
using System;
using System.Windows;
using System.Windows.Media.Effects;

namespace Fuzky.UI.Common
{
    public class BaseViewModel : BaseNotification, IViewModel
    {
        public BaseViewModel(IView view, IComponentContext container)
        {
            this.View = view;
            this.View.DataContext = this;

            this.Container = container;
        }

        public IView View { get; set; }
        public IWindowViewModel Parent { get; set; }
        public IComponentContext Container { get; set; }

        protected void ShowDialog<TDialog>(Action<TDialog> beforeShow = null)
            where TDialog : IWindowViewModel
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                var dialogModel = this.Container.Resolve<TDialog>();
                beforeShow?.Invoke(dialogModel);


                var parentWindow = this.Parent.Window as Window;
                var dialogWindow = dialogModel.Window as Window;
                dialogWindow.Owner = parentWindow;

                this.BlurWindow(parentWindow);
                var result = dialogWindow.ShowDialog();
                this.ClearEffect(parentWindow);
            });
        }

        private void BlurWindow(Window window)
        {
            var effect = new BlurEffect();

            effect.Radius = 10;

            window.Effect = effect;
        }

        private void ClearEffect(Window window)
        {
            window.Effect = null;
        }
    }
}