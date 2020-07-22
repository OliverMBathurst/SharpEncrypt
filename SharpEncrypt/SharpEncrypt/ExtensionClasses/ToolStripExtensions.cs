using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharpEncrypt.ExtensionClasses
{
    public static class ToolStripExtensions
    {
        public static IEnumerable<ToolStripItem> AllItems(this ToolStrip toolStrip)
        {
            if (toolStrip == null)
                throw new ArgumentNullException(nameof(toolStrip));
            return toolStrip.Items.Flatten();
        }

        public static IEnumerable<ToolStripItem> Flatten(this ToolStripItemCollection items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripDropDownItem toolDownItem)
                    foreach (var subItem in toolDownItem.DropDownItems.Flatten())
                        yield return subItem;
                yield return item;
            }
        }
    }
}