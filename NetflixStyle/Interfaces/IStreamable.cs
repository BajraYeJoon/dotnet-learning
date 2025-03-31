namespace CSharpLearning.NetflixStyle.Interfaces
{
    public interface IStreamable
    {
        double GetStreamQuality();
        bool IsAvailableInRegion(string region);
        string[] GetAvailableSubtitles();
    }
}