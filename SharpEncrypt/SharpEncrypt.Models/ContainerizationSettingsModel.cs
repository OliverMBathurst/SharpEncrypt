using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class ContainerizationSettingsModel
    {
        public ContainerizationSettingsModel(string password, string extension)
        {
            Password = password;
            Extension = extension;
        }

        public string Password { get; }

        public string Extension { get; }
    }
}
