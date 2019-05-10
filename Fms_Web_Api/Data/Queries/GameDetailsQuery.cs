using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Queries
{
    public class GameDetailsQuery : Query, IGameDetailsQuery
    {
        private const string GET_ALL = "spGetAllGameDetails";
        private const string GET = "spGetGameDetailsById";
        private const string INSERT = "spInsertGameDetails";
        private const string UPDATE = "spUpdateGameDetails";
        private const string DELETE = "spDeleteGameDetails";

        public IEnumerable<GameDetails> GetAll()
        {
            return GetAll<GameDetails>(GET_ALL);
        }
        public GameDetails Get(int id)
        {
            return GetSingle<GameDetails>(GET, id);
        }
        public int Insert(GameDetails gameDetails)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    { "managerName", gameDetails.ManagerName }
                });
        }
        public int Update(GameDetails gameDetails)
        {
            return Update(UPDATE, new { gameDetails.Id, gameDetails.ManagerName, gameDetails.CurrentSeasonId, gameDetails.TeamId, gameDetails.CurrentWeek });
        }
        public int Delete(int id)
        {
            return Delete(DELETE, id);
        }

    }


}