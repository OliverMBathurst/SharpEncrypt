using System;

namespace SharpEncrypt
{
    [Serializable]
    internal sealed class SharpEncryptSettings
    {
        public string LanguageCode { get; set; } = "en-GB";

        public bool OTPDisclaimerHide { get; set; } = false;
    }
}
