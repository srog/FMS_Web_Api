namespace Fms_Web_Api.Models
{
    public class News
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
        public int? GameDetailsId { get; set; }
        public int? SeasonId { get; set; }
        public int? DivisionId { get; set; }
        public int? PlayerId { get; set; }
        public int? Week { get; set; }
        public string NewsText { get; set; }
    }

    public class PlayerNews
    {
        public News News { get; set; }
        public string PlayerName { get; set; }
        public string TeamName { get; set; }
        public int Weeks { get; set; }
    }

    public class PromotionNews
    {
        public News News { get; set; }
        public string TeamName { get; set; }
        public int Division { get; set; }
    }
}
