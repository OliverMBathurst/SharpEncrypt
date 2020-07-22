using System;
using System.IO;
using System.Resources;

namespace SharpEncrypt.Helpers
{
    internal sealed class PathHelper
    {
        private readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resources.Resources));

        public string AppDirectory
        {
            get
            {
                var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ResourceManager.GetString("AppName") ?? string.Empty);
                CreateDirs(dir);
                return dir;
            }            
        }

        public string ExcludedFilesFile => Path.Combine(AppDirectory, ResourceManager.GetString("ExcludedFilesFile") ?? string.Empty);

        public string ExcludedFoldersFile => Path.Combine(AppDirectory, ResourceManager.GetString("ExcludedFoldersFile") ?? string.Empty);

        public string SecuredFilesListFile => Path.Combine(AppDirectory, ResourceManager.GetString("SecuredFilesListFileName") ?? string.Empty);

        public string SecuredFoldersListFile => Path.Combine(AppDirectory, ResourceManager.GetString("SecuredFoldersListFileName") ?? string.Empty);

        public string LoggingFilePath
        {
            get
            {
                var dir = Path.Combine(AppDirectory, ResourceManager.GetString("LoggingDir") ?? string.Empty);
                CreateDirs(dir);
                return Path.Combine(dir, ResourceManager.GetString("LogFileName") ?? string.Empty);
            }
        }

        public string UserKeysDirectory
        {
            get
            {
                var dir = Path.Combine(AppDirectory, ResourceManager.GetString("UserKeys") ?? string.Empty);
                CreateDirs(dir);
                return dir;
            }            
        }

        public string OtpPasswordStoreFile => Path.Combine(PasswordStoresDirectory, ResourceManager.GetString("OTPPasswordStoreFile") ?? string.Empty);

        public string AesPasswordStoreFile => Path.Combine(PasswordStoresDirectory, ResourceManager.GetString("AESPasswordStoreFile") ?? string.Empty);

        public string PasswordStoresDirectory
        {
            get
            {
                var dir = Path.Combine(AppDirectory, ResourceManager.GetString("PasswordStoresDir") ?? string.Empty);
                CreateDirs(dir);
                return dir;
            }
        }

        public string ImportedKeysDirectory
        {
            get
            {
                var dir = Path.Combine(AppDirectory, ResourceManager.GetString("ImportedKeysDir") ?? string.Empty);
                CreateDirs(dir);
                return dir;
            }            
        }

        public string PubKeyFile => Path.Combine(ImportedKeysDirectory, ResourceManager.GetString("PubKeysFile") ?? string.Empty);

        public (string pubKey, string privKey) KeyPairPaths
        {
            get
            {
                var dir = UserKeysDirectory;
                return (Path.Combine(dir, ResourceManager.GetString("RSAPubKeyFile") ?? string.Empty),
                    Path.Combine(dir, ResourceManager.GetString("RSAPrivKeyFile") ?? string.Empty));
            }            
        }

        public string AppSettingsPath => Path.Combine(AppDirectory, ResourceManager.GetString("SharpEncryptSettingsFileName") ?? string.Empty);

        private static void CreateDirs(params string[] paths)
        {
            foreach (var path in paths)
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
        }
    }
}
