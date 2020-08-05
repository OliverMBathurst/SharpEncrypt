using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class GenericTextViewer : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly string[] Lines;

        public GenericTextViewer(IEnumerable<string> lines)
        {
            InitializeComponent();
            Lines = lines.ToArray();
        }

        private void GenericTextViewer_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("TextViewer");
            TextBox.Lines = Lines;
        }
    }
}
