using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data
{
    public class NewsQuery : Query
    {
        private const string GET_ALL = "spGetNews";
        private const string GET = "spGetNewsById";
        private const string INSERT = "spInsertNews";
 

        public IEnumerable<News> GetLatest(int seasonId, int week)
        {
            var param = new { seasonId, week };
            return GetAll<News>(GET_ALL, param);
        }

        public IEnumerable<News> GetBySeasonAndDivision(int divisionId, int seasonId)
        {
            var param = new { divisionId, seasonId };
            return GetAll<News>(GET_ALL, param);
        }

        public IEnumerable<News> GetAllForSeason(int seasonId)
        {
            return GetAllById<News>(GET_ALL, "seasonId", seasonId);
        }

        public News Get(int id)
        {
            return GetSingle<News>(GET, id);
        }
        public int Add(News news)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    { "divisionId", news.DivisionId },
                    { "gameDetailsId", news.GameDetailsId },
                    { "seasonId", news.SeasonId },
                    { "teamId", news.TeamId },
                    { "playerId", news.PlayerId },
                    { "week", news.Week },
                    { "newsText", news.NewsText }
                });
        }
     
    }
}
