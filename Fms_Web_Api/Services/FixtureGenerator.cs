using System;
using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Fms_Web_Api.Services
{
    public class FixtureGenerator : IFixtureGenerator
    {
        private readonly IMatchService _matchService;
        private readonly IConfiguration _configuration;

        public FixtureGenerator(IMatchService matchService, IConfiguration configuration)
        {
            _matchService = matchService;
            _configuration = configuration;
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

            GenerateHalfSeasonFixtures(teamSeasons, gameDetailsId, seasonId, divisionId, true);
            GenerateHalfSeasonFixtures(teamSeasons, gameDetailsId, seasonId, divisionId, false);
        }

        // Refactor this !
        private void GenerateHalfSeasonFixtures(IEnumerable<TeamSeason> teamSeasons, int gameDetailsId, int seasonId, int divisionId, bool firstHalf)
        {
            var MatchesPerRound = _configuration.GetValue<int>("TeamsInDivision") / 2;
            var RoundsPerSeason = _configuration.GetValue<int>("TeamsInDivision") - 1;

            var fixedteamid = teamSeasons.First().TeamId;
            var teamidlist = teamSeasons
                .Where(t => t.TeamId != fixedteamid)
                .Select(t => t.TeamId);

            // week one 
            for (var homeid = 1; homeid <= MatchesPerRound; homeid++)
            {
                var homeTeamId = teamSeasons.ElementAt(homeid - 1).TeamId;
                var awayTeamId = teamSeasons.ElementAt(teamSeasons.Count() - homeid).TeamId;

                _matchService.Insert(new Match
                    {
                        GameDetailsId = gameDetailsId,
                        SeasonId = seasonId,
                        Week = 1 + (!firstHalf ? RoundsPerSeason : 0),
                        DivisionId = divisionId,
                        Completed = false,
                        HomeTeamId = firstHalf ? awayTeamId : homeTeamId,
                        AwayTeamId = firstHalf ? homeTeamId : awayTeamId
                });
            }

            // weeks 2-11 
            var reverseGameOne = true;
            for (var week = 1; week < RoundsPerSeason; week++)
            {
                // first game with fixed team
                var firstgameaway = RoundsPerSeason - week - 1;
                var doReverse = reverseGameOne ? !firstHalf : firstHalf;

                var homeTeamId = doReverse ? teamidlist.ElementAt(firstgameaway) : fixedteamid;
                var awayTeamId = doReverse ? fixedteamid : teamidlist.ElementAt(firstgameaway);
           
                _matchService.Insert(new Match
                    {
                        GameDetailsId = gameDetailsId,
                        SeasonId = seasonId,
                        Week = week + 1 + (!firstHalf ? 11 : 0),
                        DivisionId = divisionId,
                        Completed = false,
                        HomeTeamId = homeTeamId,
                        AwayTeamId = awayTeamId
                    });

                reverseGameOne = !reverseGameOne;

                // other 11 games round robin
                for (var loop = 0; loop < (MatchesPerRound - 1); loop++)
                {
                    var hometeamindex = RoundsPerSeason - week + loop;
                    if (hometeamindex > (RoundsPerSeason - 1))
                    {
                        hometeamindex -= RoundsPerSeason;
                    }
                    homeTeamId = teamidlist.ElementAt(hometeamindex);

                    var awayteamindex = (RoundsPerSeason - 2) - week - loop;
                    if (awayteamindex < 0)
                    {
                        awayteamindex += RoundsPerSeason;
                    }
                    awayTeamId = teamidlist.ElementAt(awayteamindex);

                    var realHomeId = firstHalf ? homeTeamId : awayTeamId;
                    var realAwayId = firstHalf ? awayTeamId : homeTeamId;

                    _matchService.Insert(new Match
                        {
                            GameDetailsId = gameDetailsId,
                            SeasonId = seasonId,
                            DivisionId = divisionId,
                            Week = week + 1 + (!firstHalf ? RoundsPerSeason : 0),
                            Completed = false,
                            HomeTeamId = realHomeId,
                            AwayTeamId = realAwayId
                        });
                  }
            }


        }
        #endregion
    }
}
