using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;

namespace SharpEncrypt.Forms
{
    public partial class GenericTextInput<T> : Form
    {
        private readonly ResourceManager ResourceManager = new ComponentResourceManager(typeof(Resources.Resources));
        private readonly string LabelText;
        private readonly T DefaultValue;

        public T Result { get; private set; }

        public GenericTextInput(string labelText, T defaultValue)
        {
            InitializeComponent();
            LabelText = labelText;
            DefaultValue = defaultValue;
        }

        private void GenericTextInput_Load(object sender, EventArgs e)
        {
            Text = ResourceManager.GetString("TextInput");
            InputGroupBox.Text = ResourceManager.GetString("Input");
            InputLabel.Text = LabelText;
            OK.Text = ResourceManager.GetString("OK");
        }

        private void OK_Click(object sender, EventArgs e)
        {
            string input;
            if (!string.IsNullOrEmpty(input = InputBox.Text))
            {
                try
                {
                    Result = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (NotSupportedException ex)
                {
                    MessageBox.Show(
                        string.Format(CultureInfo.CurrentCulture,
                        ResourceManager.GetString("AnExceptionHasOccurred"),
                        ex.Message),
                        ResourceManager.GetString("Error"),
                        MessageBoxButtons.OK);
                }
            } 
            else
            {
                Result = DefaultValue;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}