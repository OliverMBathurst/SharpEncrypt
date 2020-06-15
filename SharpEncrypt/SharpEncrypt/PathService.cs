using System;
using System.IO;
using System.Resources;

namespace SharpEncrypt
{
    internal sealed class PathService
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources));

        public string AppDirectory
        {
            get
            {
                var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ResourceManager.GetString("AppName"));
                CreateDirs(dir);
                return dir;
            }            
        }

        public string UserKeysDirectory
        {
            get
            {
                var dir = Path.Combine(AppDirectory, ResourceManager.GetString("UserKeys"));
                CreateDirs(dir);
                return dir;
            }            
        }

        public string ImportedKeysDirectory
        {
            get
            {
                var dir = Path.Combine(AppDirectory, ResourceManager.GetString("ImportedKeysDir"));
                CreateDirs(dir);
                return dir;
            }            
        }

        public (string pubKey, string privKey) KeyPairPaths
        {
            get
            {
                var dir = UserKeysDirectory;
                return (Path.Combine(dir, ResourceManager.GetString("RSAPubKeyFile")),
                    Path.Combine(dir, ResourceManager.GetString("RSAPrivKeyFile")));
            }            
        }

        public string AppSettingsPath => Path.Combine(AppDirectory, ResourceManager.GetString("SharpEncryptSettingsFileName"));

        private void CreateDirs(params string[] paths)
        {
            foreach (var path in paths)
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
        }
    }
}
