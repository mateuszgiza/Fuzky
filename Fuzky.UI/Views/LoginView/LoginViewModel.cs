using Autofac;
using Fuzky.Core;
using Fuzky.Core.Utils;
using Fuzky.UI.Common;
using Fuzky.UI.Views.Message;
using System;
using System.Threading.Tasks;

namespace Fuzky.UI.Views.LoginView
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        private readonly SteamAuthentication steamAuthentication;

        public LoginViewModel(ILoginView view, IComponentContext container, SteamAuthentication steamAuthentication)
            : base(view, container)
        {
            this.steamAuthentication = steamAuthentication;
            this.steamAuthentication.TwoFactorCodeRequired.Subscribe(OnTwoFactorCodeRequired);
            this.steamAuthentication.ExceptionThrown.Subscribe(OnExceptionThrown);

            this.LoginCommand = new AsyncCommand(OnLoginCommand, CommandExecutionPolicies.OneAtATime);
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public AsyncCommand LoginCommand { get; }

        private async Task OnLoginCommand(object o)
        {
            if (this.Username == "ex")
            {
                this.OnExceptionThrown(null, null);
            }
            else if (this.Username == "2fa")
            {
                this.OnTwoFactorCodeRequired(null, null);
            }
            else
            {
                this.ShowDialog<IMessageViewModel>(d =>
                {
                    d.Title = "Login";
                    d.View.Message = "Login Command!";
                });
            }

            //var response = await this.steamAuthentication.Authenticate(Username, Password, "");
            //if (response.LoginComplete)
            //{
            //    MessageBox.Show("Successfull!");
            //}
            //else if (!string.IsNullOrEmpty(response.Message))
            //{
            //    MessageBox.Show(response.Message);
            //}
        }

        private void OnTwoFactorCodeRequired(object sender, EventArgs eventArgs)
        {
            this.ShowDialog<IMessageViewModel>(d =>
            {
                d.Title = "Steam Authentication";
                d.View.Message = "Two Factor Code Required!";
            });
        }

        private void OnExceptionThrown(object sender, EventArgs eventArgs)
        {
            this.ShowDialog<IMessageViewModel>(d =>
            {
                d.Title = "Exception";
                d.View.Message = "Unexpected expection was thrown!";
            });
        }
    }
}