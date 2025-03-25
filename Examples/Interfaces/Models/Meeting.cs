
namespace CSharpLearning.Examples.Interfaces
{
    // Meeting class
    public class Meeting
    {
        private static int _meetingCounter = 2000;
        private List<User> _participants = new List<User>();

        public string MeetingId { get; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public User Organizer { get; }
        public bool IsCancelled { get; private set; }

        public Meeting(string title, DateTime startTime, TimeSpan duration, User organizer)
        {
            MeetingId = $"MTG{++_meetingCounter}";
            Title = title;
            StartTime = startTime;
            Duration = duration;
            Organizer = organizer;
            IsCancelled = false;

            // Add organizer as participant
            _participants.Add(organizer);
        }

        public void AddParticipant(User user)
        {
            if (!_participants.Exists(p => p.UserId == user.UserId))
            {
                _participants.Add(user);
            }
        }

        public void RemoveParticipant(User user)
        {
            _participants.RemoveAll(p => p.UserId == user.UserId);
        }

        public List<User> GetParticipants()
        {
            return [.. _participants];
        }

        public void Cancel()
        {
            IsCancelled = true;
        }

        public override string ToString()
        {
            return $"{Title} - {StartTime:g} ({Duration.TotalMinutes} min)" + (IsCancelled ? " [CANCELLED]" : "");
        }
    }
}