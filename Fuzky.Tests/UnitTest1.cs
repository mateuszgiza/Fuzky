using Fuzky.Core;
using Fuzky.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Fuzky.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var auth = new SteamAuthentication();

            const string login = "";
            const string password = "";
            const string twoFactorCode = "";

            void OnR(object sender, EventArgs args)
            {
                auth.Authenticate(login, password, twoFactorCode);
            }

            auth.TwoFactorCodeRequired.Subscribe(OnR);

            auth.Authenticate(login, password, "");
        }
    }
}
