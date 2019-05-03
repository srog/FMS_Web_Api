using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data
{
    public class SeasonQuery : Query
    {
        private const string GET_ALL = "spGetAllSeasons";
        private const string GET_ALL_BY_GAME = "spGetSeasonsByGameId";
        private const string GET = "spGetSeasonById";
        private const string INSERT = "spInsertSeason";
        private const string UPDATE = "spUpdateSeason";
        private const string DELETE = "spDeleteSeason";

        public IEnumerable<Season> GetAll()
        {
            return GetAll<Season>(GET_ALL);
        }

        public IEnumerable<Season> GetByGame(int gameDetailsId)
        {
            return GetAllById<Season>(GET_ALL_BY_GAME, "gameDetailsId", gameDetailsId);
        }
        public Season Get(int id)
        {
            return GetSingle<Season>(GET, id);
        }
        public int Add(Season season)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    { "completed", season.Completed },
                    { "gameDetailsId", season.GameDetailsId },
                    { "startYear", season.StartYear }
                });
        }
        public int Update(Season season)
        {
            return Update(UPDATE, new { season.Id, season.Completed, season.GameDetailsId, season.StartYear });
        }
        public int Delete(int id)
        {
            return Delete(DELETE, id);
        }



    }
}
