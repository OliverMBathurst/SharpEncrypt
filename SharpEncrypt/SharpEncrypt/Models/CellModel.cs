using System;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class CellModel
    {
        public CellType CellType { get; set; } = CellType.TextBox;

        public object Value { get; set; }
    }
}
