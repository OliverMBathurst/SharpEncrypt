using OTPLibrary;
using SharpEncrypt.AbstractClasses;
using SharpEncrypt.Enums;
using System.Threading.Tasks;

namespace SharpEncrypt.Tasks
{
    internal sealed class OneTimePadEncryptTask : SharpEncryptTask
    {
        public override TaskType TaskType => TaskType.OneTimePadTransformTask;
    
        public OneTimePadEncryptTask(string filePath, string keyFilePath = "")
        {
            InnerTask = new Task(() =>
            {
                //append the extension to file (use filegeneratorhelper to get a unique name if file already exists.1.axx)
                if (string.IsNullOrEmpty(keyFilePath))
                {
                    OTPHelper.EncryptWithoutKey(filePath);
                }
                else
                {
                    OTPHelper.Transform(filePath, keyFilePath);
                }
            });
        }
    }
}