namespace Fuzky.UI.Common
{
    public interface IWindow
    {
        object DataContext { get; set; }
        IWindow Owner { get; set; }

        object Effect { get; set; }

        bool? ShowDialog();
    }
}