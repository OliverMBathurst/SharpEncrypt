using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharpEncrypt.ExtensionClasses
{
    public static class DataGridViewColumnCollectionExtensions
    {
        public static void AddRange(this DataGridViewColumnCollection collection,
            IEnumerable<DataGridViewColumn> columns)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (columns == null)
                throw new ArgumentNullException(nameof(columns));

            foreach (var column in columns)
            {
                collection.Add(column);
            }
        }
    }
}