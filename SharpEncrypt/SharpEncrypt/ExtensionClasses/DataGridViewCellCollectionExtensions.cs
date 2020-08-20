using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharpEncrypt.ExtensionClasses
{
    public static class DataGridViewCellCollectionExtensions
    {
        public static void AddRange(this DataGridViewCellCollection collection, IEnumerable<DataGridViewCell> cells)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (cells == null)
                throw new ArgumentNullException(nameof(cells));

            foreach (var cell in cells)
            {
                collection.Add(cell);
            }
        }
    }
}
