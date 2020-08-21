using System;
using System.Windows.Forms;

namespace SharpEncrypt
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var mainForm = new Forms.MainForm())
            {
                Application.Run(mainForm);
            }                
        }
    }
}
