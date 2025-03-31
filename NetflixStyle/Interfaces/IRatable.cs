namespace CSharpLearning.NetflixStyle.Interfaces
{
    public interface IRatable
    {
        double GetAverageRating();
        void AddUserRating(int userId, double rating);
        bool HasUserRated(int userId);
        string[] GetUserReviews();
    }
}