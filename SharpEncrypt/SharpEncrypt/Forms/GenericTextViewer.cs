using System;
using System.ComponentModel;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class GenericTextViewer : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly string[] Lines;

        public GenericTextViewer(string[] lines)
        {
            InitializeComponent();
            Lines = lines;
        }

        private void GenericTextViewer_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("TextViewer");
            TextBox.Lines = Lines;
        }
    }
}
