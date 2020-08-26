using SharpEncrypt.Models;
using SharpEncrypt.Enums;
using System.Threading.Tasks;
using SharpEncrypt.Helpers;
using SharpEncrypt.Models.Result_Models;

namespace SharpEncrypt.Tasks.Folder_Tasks
{
    public class ResetApplicationTask : SharpEncryptTaskModel
    {
        public ResetApplicationTask(string appPath) : base(ResourceType.Folder, appPath) 
            => InnerTask = new Task(() =>
                {
                    DirectoryHelper.Remove(appPath);
                    Result.Value = new FinalizableTaskResultModel { ExitAfter = true };
                });
    }
}
