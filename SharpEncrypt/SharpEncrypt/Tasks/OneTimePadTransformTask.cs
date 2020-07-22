using FileGeneratorLibrary;
using OTPLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Exceptions;
using SharpEncrypt.ExtensionClasses;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class OneTimePadTransformTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.OneTimePadTransformTask;

        public OneTimePadTransformTask(string filePath, string ext, string keyFilePath = "", bool encrypt = true) : base(ResourceType.File, filePath, keyFilePath)
        {
            InnerTask = new Task(() =>
            {
                string newFileName;

                if (encrypt)
                {
                    newFileName = FileGeneratorHelper.GetValidFileNameForDirectory(
                        Path.GetDirectoryName(filePath),
                        Path.GetFileName(filePath),
                        ext);
                }
                else
                {
                    if (!filePath.EndsWith(ext, StringComparison.Ordinal))
                        throw new InvalidEncryptedFileException();
                    var name = filePath.RemoveLast(ext.Length);

                    newFileName = FileGeneratorHelper.GetValidFileNameForDirectory(
                        Path.GetDirectoryName(name),
                        Path.GetFileName(name),
                        string.Empty);
                }

                if (newFileName == null)
                    throw new NoSuitableNameFoundException();

                File.Move(filePath, newFileName);

                if (string.IsNullOrEmpty(keyFilePath))
                {
                    OTPHelper.EncryptWithoutKey(newFileName);
                }
                else
                {
                    OTPHelper.Transform(newFileName, keyFilePath);
                }
            });
        }
    }
}