
namespace Fms_Web_Api.Models
{
    public class GameDetails
    {
        public int Id { get; set; }
        public string ManagerName { get; set; }
        public int CurrentSeasonId { get; set; }
        public int CurrentWeek { get; set; }
        public int TeamId { get; set; }
    }
}
