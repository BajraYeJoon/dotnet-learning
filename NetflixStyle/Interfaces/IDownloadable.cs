namespace CSharpLearning.NetflixStyle.Interfaces
{
    public interface IDownloadable
    {
        bool CanDownload { get; }
        long GetDownloadSize();
        string GetDownloadQuality();
        void StartDownload();
        void PauseDownload();
        void ResumeDownload();
    }
}
