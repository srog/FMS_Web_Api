namespace Fms_Web_Api.Models
{
    public class MatchEvent
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int Minute { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int EventType { get; set; }
    }
}
