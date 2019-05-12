using System;
using Fms_Web_Api.Models;
using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;
using System.Linq;

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
                { "completed", match.Completed },
                { "homeTeamId", match.HomeTeamId },
                { "awayTeamId", match.AwayTeamId },
                { "homeTeamScore", match.HomeTeamScore },
                { "awayTeamScore", match.AwayTeamScore }
            });
        }

        public int Update(Match match)
        {
            return Update(UPDATE, new {
                match.Id, match.HomeTeamScore, match.AwayTeamScore, match.Completed
            });
        }

        public void Delete(int gameDetailsId)
        {
            throw new NotImplementedException();
            //Delete(DELETE, gameDetailsId);
        }

        public void GenerateFixtures(IEnumerable<TeamSeason> teamSeasons)
        {
            GenerateFixturesForDivision(teamSeasons.Where(ts => ts.DivisionId == 1));
            GenerateFixturesForDivision(teamSeasons.Where(ts => ts.DivisionId == 2));
            GenerateFixturesForDivision(teamSeasons.Where(ts => ts.DivisionId == 3));
            GenerateFixturesForDivision(teamSeasons.Where(ts => ts.DivisionId == 4));

        }
        public void GenerateFixturesForDivision(IEnumerable<TeamSeason> teamSeasons)
        {
            var seasonId = teamSeasons.FirstOrDefault().SeasonId;
            var gameDetailsId = teamSeasons.FirstOrDefault().GameDetailsId;
            var divisionId = teamSeasons.FirstOrDefault().DivisionId;

            var homeHalf = true;

            var week = 1;

            var MatchesPerRound = teamSeasons.Count() / 2;
            var RoundsPerSeason = 1;

            var fixedteamid = teamSeasons.First().TeamId;

            var allteamidlist = teamSeasons.Select(t => t.TeamId);

            var teamidlist = teamSeasons
                .Where(t => t.TeamId != fixedteamid)
                .Select(t => t.TeamId);

            // week one 
            for (var homeid = 1; homeid <= MatchesPerRound; homeid++)
            {
                AddFixture(gameDetailsId, seasonId, 1 + (!homeHalf ? (teamSeasons.Count() - 1) : 0), 
                        divisionId, homeHalf,
                    allteamidlist.ElementAt(homeid - 1),
                    allteamidlist.ElementAt(teamSeasons.Count() - homeid));
            }


        }
        private void AddFixture(int gameDetailsId, int seasonId, int week, int divisionId, 
            bool reverseTeams, int homeTeamId, int awayTeamId)
        {
            Add(INSERT, new Match
            {
                GameDetailsId = gameDetailsId,
                SeasonId = seasonId,
                DivisionId = divisionId,
                Week = week,
                Completed = false,
                HomeTeamId = reverseTeams ? awayTeamId : homeTeamId,
                AwayTeamId = reverseTeams ? homeTeamId : awayTeamId
            });
        }

        public int PlayMatch(int id)
        {
            var match = Get(id);
            if (match.Completed.Value)
            {
                return 0;
            }

            match.HomeTeamScore = Utilities.Utilities.GetRandomNumber(0,5);
            match.AwayTeamScore = Utilities.Utilities.GetRandomNumber(0,4);
            match.Completed = true;

            return Update(match);


        }

    }
}
