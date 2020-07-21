using AESLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class ReencryptTempFilesTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.ReencryptTempFilesTask;

        public override bool IsSpecial => false;

        public ReencryptTempFilesTask(IEnumerable<string> paths, string password, bool exitAfter = false) : base(ResourceType.File, paths)
        {
            InnerTask = new Task(() =>
            {
                var uncontainerized = new List<string>();
                foreach(var path in paths)
                {
                    if (File.Exists(path))
                    {
                        try
                        {
                            ContainerHelper.ContainerizeFile(path, AESHelper.GetNewAESKey(), password);
                        }
                        catch (Exception)
                        {
                            uncontainerized.Add(path);
                        }
                    }
                }

                Result.Value = new ReencryptTempFilesTaskResult { UncontainerizedFiles = uncontainerized, ExitAfter = exitAfter };
            });
        }
    }
}
