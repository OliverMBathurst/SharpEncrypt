using SharpEncrypt.Enums;
using System;

namespace SharpEncrypt.Models
{
    [Serializable]
    public sealed class SharpEncryptSettingsModel
    {
        public string LanguageCode { get; set; } = "en-GB";

        public bool OTPDisclaimerHide { get; set; } = false;

        public bool PasswordStartupPromptHide { get; set; } = false;

        public bool ForceExitDisclaimerHide { get; set; } = false;

        public bool DebugEnabled { get; set; } = false;

        public bool IncludeSubfolders { get; set; } = false;

        public bool UseADifferentPasswordForEachFile { get; set; } = false;

        public bool WipeFreeSpaceAfterSecureDelete { get; set; } = false;

        public bool ReencryptOnLock { get; set; } = false;

        public bool Logging { get; set; } = false;

        public StoreType StoreType { get; set; } = StoreType.OTP;

        public string OTPStoreKeyFilePath { get; set; } = string.Empty;

        public int InactivityTimeout { get; set; } = -1;
    }
}
