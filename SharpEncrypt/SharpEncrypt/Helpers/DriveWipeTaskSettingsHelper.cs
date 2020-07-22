using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System;
using System.Collections.Generic;

namespace SharpEncrypt.Helpers
{
    internal static class DriveWipeTaskSettingsHelper
    {
        public static IEnumerable<DriveWipeTaskSettings> GetOptions()
        {
            foreach (var name in Enum.GetNames(typeof(DriveWipeType)))
            {
                if (Enum.TryParse<DriveWipeType>(name, out var result))
                {
                    yield return GetSettings(result);
                }
            }
        }

        public static DriveWipeTaskSettings GetSettings(DriveWipeType type)
        {
            switch (type)
            {
                case DriveWipeType.Gutmann:
                    return GutmannSettings;
                case DriveWipeType.RandomWrite:
                    return RandomWriteSettings;
                case DriveWipeType.SDelete:
                    return SDeleteSettings;
                case DriveWipeType.WriteZeros:
                    return WriteZerosSettings;
                case DriveWipeType.WriteTwoFiveFives:
                    return Write255Settings;
                case DriveWipeType.Default:
                    return DefaultWipeSettings;
                default:
                    return DefaultSettings;
            }
        }

        private static DriveWipeTaskSettings GutmannSettings =>
            new DriveWipeTaskSettings
            {
                Type = DriveWipeType.Gutmann,
                NameObfuscation = true,
                PropertyObfuscation = true,
                WipeRounds = false
            };

        private static DriveWipeTaskSettings SDeleteSettings =>
            new DriveWipeTaskSettings
            {
                Type = DriveWipeType.SDelete,
                NameObfuscation = false,
                PropertyObfuscation = false,
                WipeRounds = false
            };

        private static DriveWipeTaskSettings RandomWriteSettings => DefaultSettings;

        private static DriveWipeTaskSettings WriteZerosSettings
        {
            get
            {
                var @default = DefaultSettings;
                @default.Type = DriveWipeType.WriteZeros;
                return @default;
            }
        }

        private static DriveWipeTaskSettings Write255Settings
        {
            get
            {
                var @default = DefaultSettings;
                @default.Type = DriveWipeType.WriteTwoFiveFives;
                return @default;
            }
        }

        private static DriveWipeTaskSettings DefaultWipeSettings
        {
            get
            {
                var @default = DefaultSettings;
                @default.Type = DriveWipeType.Default;
                return @default;
            }
        }

        private static DriveWipeTaskSettings DefaultSettings =>
            new DriveWipeTaskSettings
            {
                Type = DriveWipeType.RandomWrite,
                NameObfuscation = true,
                PropertyObfuscation = true,
                WipeRounds = true
            };
    }
}