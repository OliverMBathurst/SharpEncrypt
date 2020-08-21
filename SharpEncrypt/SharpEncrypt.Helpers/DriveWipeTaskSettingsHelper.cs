using SharpEncrypt.Enums;
using SharpEncrypt.Models;
using System;
using System.Collections.Generic;

namespace SharpEncrypt.Helpers
{
    public static class DriveWipeTaskSettingsHelper
    {
        public static IEnumerable<DriveWipeTaskSettingsModel> GetOptions()
        {
            foreach (var name in Enum.GetNames(typeof(DriveWipeType)))
            {
                if (Enum.TryParse<DriveWipeType>(name, out var result))
                {
                    yield return GetSettings(result);
                }
            }
        }

        public static DriveWipeTaskSettingsModel GetSettings(DriveWipeType type)
        {
            switch (type)
            {
                case DriveWipeType.Gutmann:
                    return GutmannSettingsModel;
                case DriveWipeType.RandomWrite:
                    return RandomWriteSettingsModel;
                case DriveWipeType.SDelete:
                    return SDeleteSettingsModel;
                case DriveWipeType.WriteZeros:
                    return WriteZerosSettingsModel;
                case DriveWipeType.WriteTwoFiveFives:
                    return Write255SettingsModel;
                case DriveWipeType.Default:
                    return DefaultWipeSettingsModel;
                default:
                    return DefaultSettingsModel;
            }
        }

        private static DriveWipeTaskSettingsModel GutmannSettingsModel =>
            new DriveWipeTaskSettingsModel
            {
                Type = DriveWipeType.Gutmann,
                NameObfuscation = true,
                PropertyObfuscation = true,
                WipeRounds = false
            };

        private static DriveWipeTaskSettingsModel SDeleteSettingsModel =>
            new DriveWipeTaskSettingsModel
            {
                Type = DriveWipeType.SDelete,
                NameObfuscation = false,
                PropertyObfuscation = false,
                WipeRounds = false
            };

        private static DriveWipeTaskSettingsModel RandomWriteSettingsModel => DefaultSettingsModel;

        private static DriveWipeTaskSettingsModel WriteZerosSettingsModel
        {
            get
            {
                var @default = DefaultSettingsModel;
                @default.Type = DriveWipeType.WriteZeros;
                return @default;
            }
        }

        private static DriveWipeTaskSettingsModel Write255SettingsModel
        {
            get
            {
                var @default = DefaultSettingsModel;
                @default.Type = DriveWipeType.WriteTwoFiveFives;
                return @default;
            }
        }

        private static DriveWipeTaskSettingsModel DefaultWipeSettingsModel
        {
            get
            {
                var @default = DefaultSettingsModel;
                @default.Type = DriveWipeType.Default;
                return @default;
            }
        }

        private static DriveWipeTaskSettingsModel DefaultSettingsModel =>
            new DriveWipeTaskSettingsModel
            {
                Type = DriveWipeType.RandomWrite,
                NameObfuscation = true,
                PropertyObfuscation = true,
                WipeRounds = true
            };
    }
}