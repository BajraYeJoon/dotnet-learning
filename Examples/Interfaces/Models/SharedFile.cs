using System;

namespace CSharpLearning.Examples.Interfaces
{
    // Shared file class
    public class SharedFile
    {
        private static int _fileCounter = 3000;

        public string FileId { get; }
        public string FileName { get; }
        public long FileSize { get; }
        public string MimeType { get; }
        public User Uploader { get; }
        public DateTime UploadTime { get; }
        public string FormattedSize => FormatFileSize(FileSize);

        public SharedFile(User uploader, string fileName, long fileSize, string mimeType)
        {
            FileId = $"FILE{++_fileCounter}";
            FileName = fileName;
            FileSize = fileSize;
            MimeType = mimeType;
            Uploader = uploader;
            UploadTime = DateTime.Now;
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {sizes[order]}";
        }

        public override string ToString()
        {
            return $"{FileName} ({FormattedSize}) - Uploaded by {Uploader.Name} at {UploadTime:g}";
        }
    }
}