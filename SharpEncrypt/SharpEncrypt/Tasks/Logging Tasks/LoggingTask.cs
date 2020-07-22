using System;
using System.IO;
using System.Threading.Tasks;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;

namespace SharpEncrypt.Tasks.Logging_Tasks
{
    internal sealed class LoggingTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.LoggingTask;

        public LoggingTask(string logFilePath, Exception exception)
            : base(ResourceType.File, logFilePath)
        {
            InnerTask = new Task(() =>
            {
                using (var fs = new FileStream(logFilePath, FileMode.Append))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(exception.StackTrace ?? exception.Message);     
                    }
                }
            });
        }
    }
}
