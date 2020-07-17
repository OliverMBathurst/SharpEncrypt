using FileGeneratorLibrary;
using OTPLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Exceptions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class OneTimePadTransformTask : SharpEncryptTask
    {
        public override bool IsSpecial => false;

        public override TaskType TaskType => TaskType.OneTimePadTransformTask;
    
        public OneTimePadTransformTask(string filePath, string ext, string keyFilePath = "", bool encrypt = true)
            : base(ResourceType.File, filePath, keyFilePath)
        {
            InnerTask = new Task(() =>
            {
                string newFileName;

                if (encrypt)
                {
                    newFileName = FileGeneratorHelper.GetValidFileNameForDirectory(filePath, ext);
                    if (newFileName == null)
                        throw new NoSuitableNameFoundException();
                }
                else
                {
                    if (!filePath.EndsWith(ext, StringComparison.Ordinal))
                        throw new InvalidEncryptedFileException();
                    newFileName = filePath.Replace(ext, string.Empty);
                }

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