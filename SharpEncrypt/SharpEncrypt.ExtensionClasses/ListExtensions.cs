using System.Collections.Generic;
using System.Linq;

namespace SharpEncrypt.ExtensionClasses
{
    public static class ListExtensionMethods
    {
        public static int RemoveAll<T>(this List<T> list, IEnumerable<T> items) => items.Count(list.Remove);
    }
}