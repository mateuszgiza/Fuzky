using Fuzky.UI.Common;
using System.Windows;
using System.Windows.Media.Effects;

namespace Fuzky.UI.Windows
{
    public abstract class WindowBase : Window, IWindow
    {
        IWindow IWindow.Owner
        {
            get => base.Owner as IWindow;
            set => base.Owner = value as Window;
        }

        object IWindow.Effect
        {
            get => base.Effect as object;
            set => base.Effect = value as Effect;
        }
    }
}