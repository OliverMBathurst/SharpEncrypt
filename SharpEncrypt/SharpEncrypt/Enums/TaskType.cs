namespace SharpEncrypt.Enums
{
    public enum TaskType
    {
        WriteSettingsFileTask,
        ReadSettingsFileTask,
        WriteSecuredFileListTask,
        ReadSecuredFilesListTask,
        SecureFolderTask,
        ReadSecuredFoldersListTask,
        WriteSecuredFoldersListTask,
        OneTimePadTransformTask,
        LoggingTask,
        OTPSaveKeyOfFileTask,
        SecureDeleteFileTask,
        WriteFileExclusionListTask,
        WriteFolderExclusionListTask,
        ReadFileExclusionListTask,
        ReadFolderExclusionListTask,
        GenericDeleteTask,
        Undefined
    }
}
