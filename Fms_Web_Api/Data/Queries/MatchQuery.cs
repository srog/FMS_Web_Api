using System;
using Fms_Web_Api.Models;
using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;

namespace Fms_Web_Api.Data.Queries
{    
    public class MatchQuery : Query, IMatchQuery
    {
        private const string GET_ALL = "spGetMatches";
        private const string GET = "spGetMatchById";
        private const string INSERT = "spInsertMatch";
        private const string UPDATE = "spUpdateMatch";

        public IEnumerable<Match> GetAll(Match match)
        {
            var param = new
            {
                match.GameDetailsId,
                match.SeasonId,
                match.DivisionId,
                match.Week,
                match.HomeTeamId,
                match.AwayTeamId,
                match.Completed
            };
            return GetAll<Match>(GET_ALL, param);
        }

        public Match Get(int id)
        {
            return GetSingle<Match>(GET, id);
        }

        public int Insert(Match match)
        {
            return Add(INSERT, new Dictionary<string, object>
            {
                { "gameDetailsId", match.GameDetailsId },
                { "seasonId", match.SeasonId },
                { "divisionId", match.DivisionId },
                { "week", match.Week },
                { "homeTeamId", match.HomeTeamId },
                { "awayTeamId", match.AwayTeamId },
                { "homeTeamScore", match.HomeTeamScore },
                { "awayTeamScore", match.AwayTeamScore }
            });
        }

        public int Update(Match match)
        {
            return Update(UPDATE, new {
                match.HomeTeamScore, match.AwayTeamScore, match.Completed
            });
        }

        public void Delete(int gameDetailsId)
        {
            throw new NotImplementedException();
            //Delete(DELETE, gameDetailsId);
        }

    }
}
