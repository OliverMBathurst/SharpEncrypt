using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharpEncrypt.ExtensionClasses
{
    public static class DataGridViewRowCollectionExtensions
    {
        public static void RemoveAll(this DataGridViewRowCollection collection, IEnumerable<DataGridViewRow> rows)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (rows == null)
                throw new ArgumentNullException(nameof(rows));

            foreach (var row in rows)
                collection.Remove(row);
        }

        public static void RemoveAll(this DataGridViewRowCollection collection, DataGridViewSelectedRowCollection rowCollection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (rowCollection == null)
                throw new ArgumentNullException(nameof(rowCollection));

            foreach (DataGridViewRow row in rowCollection)
                collection.Remove(row);
        }
    }
}
