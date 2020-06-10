using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharpEncrypt
{
    public static class ToolStripExtensions
    {
        public static IEnumerable<ToolStripItem> AllItems(this ToolStrip toolStrip) => toolStrip.Items.Flatten();
        
        public static IEnumerable<ToolStripItem> Flatten(this ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripDropDownItem toolDownItem)
                    foreach (ToolStripItem subitem in toolDownItem.DropDownItems.Flatten())
                        yield return subitem;
                yield return item;
            }
        }
    }
}