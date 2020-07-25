using System;
using System.IO;
using System.Threading.Tasks;
using FileGeneratorLibrary;
using OtpLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Exceptions;
using SharpEncrypt.ExtensionClasses;
using SharpEncrypt.Helpers;

namespace SharpEncrypt.Tasks.Otp_Tasks
{
    internal sealed class OtpTransformTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.OneTimePadTransformTask;

        public OtpTransformTask(string filePath, string ext, string keyFilePath = "", bool encrypt = true) : base(ResourceType.File, filePath, keyFilePath)
        {
            InnerTask = new Task(() =>
            {
                string newFileName;

                if (encrypt)
                {
                    newFileName = FileGeneratorHelper.GetValidFileNameForDirectory(
                        DirectoryHelper.GetDirectory(filePath),
                        Path.GetFileName(filePath),
                        ext);
                }
                else
                {
                    if (!filePath.EndsWith(ext, StringComparison.Ordinal))
                        throw new InvalidEncryptedFileException();
                    var name = filePath.RemoveLast(ext.Length);

                    newFileName = FileGeneratorHelper.GetValidFileNameForDirectory(
                        DirectoryHelper.GetDirectory(name),
                        Path.GetFileName(name),
                        string.Empty);
                }

                if (newFileName == null)
                    throw new NoSuitableNameFoundException();

                File.Move(filePath, newFileName);

                if (string.IsNullOrEmpty(keyFilePath))
                {
                    OtpHelper.EncryptWithoutKey(newFileName);
                }
                else
                {
                    OtpHelper.Transform(newFileName, keyFilePath);
                }
            });
        }
    }
}