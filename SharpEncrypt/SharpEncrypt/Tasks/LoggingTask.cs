﻿using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class LoggingTask : SharpEncryptTask
    {
        public override bool IsSpecial => false;

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
