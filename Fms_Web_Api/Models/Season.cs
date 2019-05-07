namespace Fms_Web_Api.Models
{
    public class Season
    {
        public int Id { get; set; }
        public int GameDetailsId { get; set; }
        public int StartYear { get; set; }
        public bool Completed { get; set; }
    }
}
