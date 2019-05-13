using System;
using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class FixtureGenerator : IFixtureGenerator
    {
        private readonly IMatchService _matchService;

        public FixtureGenerator(IMatchService matchService)
        {
            _matchService = matchService;
        }


        #region Implementation of IFixtureGenerator

        public void GenerateForSeason(IEnumerable<TeamSeason> teamSeasons)
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
                var homeTeamId = allteamidlist.ElementAt(homeid - 1);
                var awayTeamId = allteamidlist.ElementAt(teamSeasons.Count() - homeid);

                _matchService.Insert(new Match
                    {
                        GameDetailsId = gameDetailsId,
                        SeasonId = seasonId,
                        Week = 1 + (!homeHalf ? (teamSeasons.Count() - 1) : 0),
                        DivisionId = divisionId,
                        Completed = false,
                        HomeTeamId = homeHalf ? awayTeamId : homeTeamId,
                        AwayTeamId = homeHalf ? homeTeamId : awayTeamId
                });
            }
        }
        #endregion
    }
}
