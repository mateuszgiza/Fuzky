using System;

namespace Fuzky.Core.Utils
{
    public class SetField
    {
        public static void NotNull<T>(T item, ref T outField, string name)
        {
            outField = item != null ? item : throw new ArgumentNullException(name);
        }
    }
}