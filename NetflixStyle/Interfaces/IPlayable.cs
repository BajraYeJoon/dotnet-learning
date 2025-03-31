namespace CSharpLearning.NetflixStyle.Interfaces
{
    public interface IPlayable
    {
        void Play();
        void Pause();
        void Stop();
        bool IsPlaying { get; }
    }
}
