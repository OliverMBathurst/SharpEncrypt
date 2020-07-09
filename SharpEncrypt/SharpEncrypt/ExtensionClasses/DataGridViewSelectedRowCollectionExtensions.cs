using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharpEncrypt.ExtensionClasses
{
    public static class DataGridViewSelectedRowCollectionExtensions
    {

        public static IEnumerable<T> Select<T>(this DataGridViewSelectedRowCollection rows, Func<object, T> func)
        {
            if (rows == null)
                throw new ArgumentNullException(nameof(rows));

            var list = new List<T>();
            foreach (var row in rows)
                list.Add(func(row));
            return list;
        }
    }
}
