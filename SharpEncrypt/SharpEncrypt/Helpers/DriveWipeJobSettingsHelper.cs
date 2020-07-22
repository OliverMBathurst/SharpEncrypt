using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System;
using System.Collections.Generic;

namespace SharpEncrypt.Helpers
{
    internal static class DriveWipeJobSettingsHelper
    {
        public static IEnumerable<DriveWipeJobSettings> GetOptions()
        {
            foreach (var name in Enum.GetNames(typeof(DriveWipeType)))
            {
                if (Enum.TryParse<DriveWipeType>(name, out var result))
                {
                    yield return GetSettings(result);
                }
            }
        }

        public static DriveWipeJobSettings GetSettings(DriveWipeType type)
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

        private static DriveWipeJobSettings GutmannSettings =>
            new DriveWipeJobSettings
            {
                Type = DriveWipeType.Gutmann,
                NameObfuscation = true,
                PropertyObfuscation = true,
                WipeRounds = false
            };

        private static DriveWipeJobSettings SDeleteSettings =>
            new DriveWipeJobSettings
            {
                Type = DriveWipeType.SDelete,
                NameObfuscation = false,
                PropertyObfuscation = false,
                WipeRounds = false
            };

        private static DriveWipeJobSettings RandomWriteSettings => DefaultSettings;

        private static DriveWipeJobSettings WriteZerosSettings
        {
            get
            {
                var @default = DefaultSettings;
                @default.Type = DriveWipeType.WriteZeros;
                return @default;
            }
        }

        private static DriveWipeJobSettings Write255Settings
        {
            get
            {
                var @default = DefaultSettings;
                @default.Type = DriveWipeType.WriteTwoFiveFives;
                return @default;
            }
        }

        private static DriveWipeJobSettings DefaultWipeSettings
        {
            get
            {
                var @default = DefaultSettings;
                @default.Type = DriveWipeType.Default;
                return @default;
            }
        }

        private static DriveWipeJobSettings DefaultSettings =>
            new DriveWipeJobSettings
            {
                Type = DriveWipeType.RandomWrite,
                NameObfuscation = true,
                PropertyObfuscation = true,
                WipeRounds = true
            };
    }
}