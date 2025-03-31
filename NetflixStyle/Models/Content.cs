using System;

namespace CSharpLearning.NetflixStyle.Models
{
    public abstract class Content
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public string[] Genres { get; set; }
        public double Rating { get; private set; }

        public int RatingCount { get; private set; }

        //constuctor
        protected Content(string id, string title, string description, int releaseYear, string[] genres)
        {
            Id = id;
            Title = title;
            Description = description;
            ReleaseYear = releaseYear;
            Genres = genres;
            Rating = 0;
            RatingCount = 0;
        }

        public abstract string GetDuration();
        public virtual string GetInfo()
        {
            return $"{Title} ({ReleaseYear}) - {string.Join(", ", Genres)}";
        }

        //methods
        public void AddRating(double rating)
        {
            if (rating < 0 || rating > 5)
            {
                throw new ArgumentException("Rating must be between 0 and 5");
            }

            double totalRating = (Rating * RatingCount) + rating;
            RatingCount++;
            Rating = totalRating / RatingCount;
        }


    }

}