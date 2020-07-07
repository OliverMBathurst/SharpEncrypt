using FileGeneratorLibrary;
using OTPLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using SharpEncrypt.Exceptions;
using System.IO;
using System.Resources;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class OneTimePadTransformTask : SharpEncryptTask
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources.Resources));

        public override bool IsSpecial => false;

        public override TaskType TaskType => TaskType.OneTimePadTransformTask;
    
        public OneTimePadTransformTask(string filePath, string keyFilePath = "")
            : base(ResourceType.File, filePath, keyFilePath)
        {
            InnerTask = new Task(() =>
            {
                var newFileName = FileGeneratorHelper.GetValidFileNameForDirectory(filePath, ResourceManager.GetString("SharpEncryptOTPEncryptedFileExtension"));
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