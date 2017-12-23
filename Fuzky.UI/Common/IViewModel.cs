namespace Fuzky.UI.Common
{
    public interface IViewModel
    {
        IView View { get; set; }
        IWindowViewModel Parent { get; set; }
    }
}