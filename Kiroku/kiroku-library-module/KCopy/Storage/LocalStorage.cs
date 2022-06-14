namespace KCopy.Storage
{
    using System;
    using System.IO;
    using System.Text;
    using KCopy.Model;

    class LocalStorage
    {
        public static bool MarkToArchiveLog(FileModel fileModel)
        {
            try
            {
                var currentLogName = fileModel.FullPath;

                var newLogName = fileModel.Path + @"\KLOG_A_" + fileModel.FileGuid.ToString() + ".txt";

                File.Move(currentLogName, newLogName);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"MarkToArchiveLog Exception: {ex}"); ;
            }
        }

        public static bool MarkToSendLog(FileModel fileModel)
        {
            try
            {
                var currentLogName = fileModel.FullPath;

                var newLogName = fileModel.Path + @"\KLOG_S_" + fileModel.FileGuid.ToString() + ".txt";

                File.Move(currentLogName, newLogName);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"MarkToSendLog Exception: {ex}");
            }
        }

        public static string[] GetDirectories(string filePath)
        {
            return Directory.GetDirectories(filePath);
        }

        public static DirectoryInfo GetDirectoryInfo(string directory)
        {
            return new DirectoryInfo(directory);
        }

        public static bool DeleteLog(string filepath)
        {
            File.Delete(filepath);

            return true;
        }

        public static byte[] ReadLog(FileModel fileModel)
        {
            byte[] document = null;

            try
            {
                string stringDocument = File.ReadAllText(fileModel.FullPath);

                document = Encoding.UTF8.GetBytes(stringDocument);
            }
            catch(Exception ex)
            {
                throw new Exception($"ReadLog Exception: {ex}");
            }

            return document;
        }
    }
}
