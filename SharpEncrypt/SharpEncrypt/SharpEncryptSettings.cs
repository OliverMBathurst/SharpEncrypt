using System;

namespace SharpEncrypt
{
    [Serializable]
    public sealed class SharpEncryptSettings
    {
        public string LanguageCode { get; set; } = "en-GB";

        public bool OTPDisclaimerHide { get; set; } = false;

        public bool ForceExitDisclaimerHide { get; set; } = false;

        public bool DebugEnabled { get; set; } = false;

        public bool IncludeSubfolders { get; set; } = false;

        public bool UseADifferentPasswordForEachFile { get; set; } = false;

        public bool WipeFreeSpaceAfterSecureDelete { get; set; } = false;

        public bool Logging { get; set; } = false;
    }
}
