using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Queries
{
    public class NewsQuery : Query, INewsQuery
    {
        private const string GET_ALL = "spGetNews";
        private const string GET = "spGetNewsById";
        private const string INSERT = "spInsertNews";

        public IEnumerable<News> GetAll(News news)
        {
            var param = new
                {
                    news.GameDetailsId,
                    news.SeasonId,
                    news.DivisionId,
                    news.Week,
                    news.TeamId,
                    news.PlayerId
                };
            return GetAll<News>(GET_ALL, param);
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

        public void Delete(int gameDetailsId)
        {
            throw new System.NotImplementedException();
        }
    }
}
