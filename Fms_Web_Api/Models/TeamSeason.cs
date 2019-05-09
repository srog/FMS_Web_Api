using Fms_Web_Api.Data;
using Fms_Web_Api.TemplateData;

namespace Fms_Web_Api.Models
{
    public class TeamSeason
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int GameDetailsId { get; set; }
        public int SeasonId { get; set; }
        public int DivisionId { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int Points { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Position { get; set; }

        public string TeamName => new TeamQuery().Get(TeamId).Name;
    }
}
