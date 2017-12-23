namespace Fuzky.UI.Common
{
    public class CommandExecutionPolicies
    {
        public static ICommandExecutionPolicy OneAtATime
            => new OneAtATimeCommandPolicy();
    }
}