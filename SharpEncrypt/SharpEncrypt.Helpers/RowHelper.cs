using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Helpers
{
    public static class RowHelper
    {
        public static IEnumerable<RowModel> GetDriveSelectionRows()
        {
           return DriveInfo.GetDrives().Select(x => new RowModel
           {
               DataSource = x,
               Cells = new List<CellModel>
               {
                   new CellModel
                   {
                       CellType = CellType.CheckBox,
                       InitialValue = false
                   },
                   new CellModel
                   {
                       InitialValue = x.Name
                   },
                   new CellModel
                   {
                       InitialValue = x.TotalSize / 1024 / 1024 / 1024
                   },
                   new CellModel
                   {
                       InitialValue = x.AvailableFreeSpace / 1024 / 1024 / 1024
                   },
                   new CellModel
                   {
                       InitialValue = x.DriveType
                   },
                   new CellModel
                   {
                       InitialValue = x.DriveFormat
                   }
               }
           });
        }
    }
}
