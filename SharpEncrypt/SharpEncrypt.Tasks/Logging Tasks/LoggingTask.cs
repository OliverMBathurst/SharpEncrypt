using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using SharpEncrypt.Enums;
using SharpEncrypt.Models;

namespace SharpEncrypt.Tasks.Logging_Tasks
{
    public sealed class LoggingTask : SharpEncryptTaskModel
    {
        public override TaskType TaskType => TaskType.LoggingTask;

        public LoggingTask(string logFilePath, Exception exception) : base(ResourceType.File, logFilePath)
        {
            InnerTask = new Task(() =>
            {
                using (var fs = new FileStream(logFilePath, FileMode.Append))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        var message = $"[{DateTime.Now.ToString(CultureInfo.CurrentCulture)}] {exception.Message}";
                        if (exception.InnerException != null)
                            message += $"\n{exception.InnerException.Message}";

                        if (exception.StackTrace != null)
                            message += $"\n{exception.StackTrace}";

                        sw.WriteLine(message);
                    }
                }
            });
        }
    }
}
