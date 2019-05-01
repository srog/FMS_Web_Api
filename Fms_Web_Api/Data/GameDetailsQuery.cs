using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data
{
    public class GameDetailsQuery : Query
    {
        private const string GET_ALL = "spGetAllGameDetails";
        private const string GET = "spGetGameDetails";
        private const string INSERT = "spInsertGameDetails";
        private const string UPDATE = "spUpdateGameDetails";

        public IEnumerable<GameDetails> GetAll()
        {
            return GetAll<GameDetails>(GET_ALL);
        }
        public GameDetails Get(int id)
        {
            return GetSingle<GameDetails>(GET, id);
        }
        public int Add(GameDetails gameDetails)
        {
            return Add<GameDetails>(INSERT, new { gameDetails.ManagerName, gameDetails.TeamId, gameDetails.CurrentYear, gameDetails.CurrentWeek});
        }
        public int Update(GameDetails gameDetails)
        {
            return Update<GameDetails>(UPDATE, new { gameDetails.Id, gameDetails.ManagerName, gameDetails.CurrentYear, gameDetails.TeamId, gameDetails.CurrentWeek });
        }
        public int Delete(int id)
        {
            return Delete(id);
        }

    }


}