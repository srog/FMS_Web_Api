
namespace Fms_Web_Api.Models
{
    public class Match
    {   
        public int Id { get; set; }
        public int? GameDetailsId { get; set; }
        public int? SeasonId { get; set; }
        public int? DivisionId { get; set; }
        public int? Week { get; set; }
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public bool Completed { get; set; }
    }
}
