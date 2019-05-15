namespace Fms_Web_Api.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int? GameDetailsId { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Age { get; set; }
        public int? Position { get; set; }
        public int? TeamId { get; set; }
        public int Value { get; set; }
        public bool Retired { get; set; }
        public int InjuredWeeks { get; set; }
        public int SuspendedWeeks { get; set; }
        public int TeamSelection { get; set; }
        public bool IsSelected => (TeamSelection > 0);
        public bool IsAvailable => (InjuredWeeks == 0) && (SuspendedWeeks == 0);
    }
}
