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
                var appDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (string.IsNullOrEmpty(appDir))
                    throw new DirectoryNotFoundException();

                var dir = Path.Combine(appDir, GetString("AppName"));
                CreateDirs(dir);
                return dir;
            }
        }

        public string ExcludedFilesFile => Path.Combine(AppDirectory, GetString("ExcludedFilesFile"));

        public string ExcludedFoldersFile => Path.Combine(AppDirectory, GetString("ExcludedFoldersFile"));

        public string SecuredFilesListFile => Path.Combine(AppDirectory, GetString("SecuredFilesListFileName"));

        public string SecuredFoldersListFile => Path.Combine(AppDirectory, GetString("SecuredFoldersListFileName"));

        public string AppSettingsPath => Path.Combine(AppDirectory, GetString("SharpEncryptSettingsFileName"));

        public string UncontainerizedFoldersLoggingFilePath => Path.Combine(LoggingDir, GetString("UncontainerizedFilesLogFileName"));

        public string LoggingFilePath => Path.Combine(LoggingDir, GetString("LogFileName"));

        public string OtpPasswordStoreFile => Path.Combine(PasswordStoresDirectory, GetString("OTPPasswordStoreFile"));

        public string AesPasswordStoreFile => Path.Combine(PasswordStoresDirectory, GetString("AESPasswordStoreFile"));

        public string OtherUsersPubKeyFile => Path.Combine(ImportedKeysDirectory, GetString("PubKeysFile"));

        public string LoggingDir
        {
            get
            {
                var dir = Path.Combine(AppDirectory, GetString("LoggingDir"));
                CreateDirs(dir);
                return dir;
            }
        }

        public string UserKeysDirectory
        {
            get
            {
                var dir = Path.Combine(AppDirectory, GetString("UserKeys"));
                CreateDirs(dir);
                return dir;
            }            
        }

       
        public string PasswordStoresDirectory
        {
            get
            {
                var dir = Path.Combine(AppDirectory, GetString("PasswordStoresDir"));
                CreateDirs(dir);
                return dir;
            }
        }

        public string ImportedKeysDirectory
        {
            get
            {
                var dir = Path.Combine(AppDirectory, GetString("ImportedKeysDir"));
                CreateDirs(dir);
                return dir;
            }            
        }

        public (string PublicKey, string PrivateKey) KeyPairPaths
        {
            get
            {
                var dir = UserKeysDirectory;
                return (Path.Combine(dir, GetString("RSAPubKeyFile")),
                    Path.Combine(dir, GetString("RSAPrivKeyFile")));
            }            
        }

        private string GetString(string key)
        {
            var str = ResourceManager.GetString(key);
            if (str == null)
                throw new MissingManifestResourceException();
            return str;
        }


        private static void CreateDirs(params string[] paths)
        {
            foreach (var path in paths)
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
        }
    }
}
