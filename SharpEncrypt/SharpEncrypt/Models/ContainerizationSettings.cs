using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    internal sealed class ContainerizationSettings
    {
        public ContainerizationSettings(string password, string extension)
        {
            Password = password;
            Extension = extension;
        }

        public string Password { get; }

        public string Extension { get; }
    }
}
