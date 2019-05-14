namespace Fms_Web_Api.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearFormed { get; set; }
        public int DivisionId { get; set; }
        public int Cash { get; set; }
        public int StadiumCapacity { get; set; }
        public int GameDetailsId { get; set; }

        public int FormationId { get; set; }

        public int TotalRating { get; set; }
        public int TotalDefenseRating { get; set; }
        public int TotalMidfieldRating { get; set; }
        public int TotalAttackRating { get; set; }

    }
}
