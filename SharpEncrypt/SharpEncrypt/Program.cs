using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SharpEncrypt
{
    internal static class Program
    {
        private static readonly Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "The embedded assemblies will always end with the extension .dll")]
        private static readonly string[] EmbeddedLibraries = ExecutingAssembly.GetManifestResourceNames().Where(x => x.EndsWith(".dll")).ToArray();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            using (var mainForm = new Forms.MainForm())
            {
                Application.Run(mainForm);
            }                
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "The embedded assemblies will always end with the extension .dll")]
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyName = $"{new AssemblyName(args.Name).Name}.dll";

            var resourceName = EmbeddedLibraries.FirstOrDefault(x => x.EndsWith(assemblyName));
            if (resourceName == null)
                return null;

            using (var stream = ExecutingAssembly.GetManifestResourceStream(resourceName))
            {
                if(stream == null)
                    throw new ArgumentNullException(nameof(stream));

                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return Assembly.Load(bytes);
            }
        }
    }
}
