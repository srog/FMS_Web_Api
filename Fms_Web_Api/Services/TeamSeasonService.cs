using System;
using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class TeamSeasonService : ITeamSeasonService
    {
        private readonly ITeamSeasonQuery _teamSeasonQuery;
        private readonly IFixtureGenerator _fixtureGenerator;
        private readonly INewsQuery _newsQuery;
        private readonly ITeamQuery _teamQuery;

        public TeamSeasonService(
            ITeamSeasonQuery teamSeasonQuery, 
            IFixtureGenerator fixtureGenerator,
            INewsQuery newsQuery,
            ITeamQuery teamQuery)
        {
            _teamSeasonQuery = teamSeasonQuery;
            _fixtureGenerator = fixtureGenerator;
            _newsQuery = newsQuery;
            _teamQuery = teamQuery;
        }
        #region Implementation of ITeamSeasonService

        public List<TeamSeason> GetByGame(int gameDetailsId)
        {
            return _teamSeasonQuery.GetByGame(gameDetailsId).ToList();
        }

        public List<TeamSeason> GetByGameAndDivision(int gameDetailsId, int divisionId)
        {
            return _teamSeasonQuery.GetByGameAndDivision(gameDetailsId, divisionId).ToList();
        }

        public List<TeamSeason> GetByGameSeasonAndDivision(int gameDetailsId, int divisionId, int seasonId)
        {
            return _teamSeasonQuery.GetByGameSeasonAndDivision(gameDetailsId, divisionId, seasonId).ToList();
        }

        public List<TeamSeason> GetBySeasonAndDivision(int seasonId, int divisionId)
        {
            return _teamSeasonQuery.GetBySeasonAndDivision(divisionId, seasonId).ToList();
        }

        public List<TeamSeason> GetByGameAndSeason(int gameDetailsId, int seasonId)
        {
            return _teamSeasonQuery.GetByGameAndSeason(gameDetailsId, seasonId).ToList();
        }

        public TeamSeason GetCurrentForTeam(int teamId)
        {
            return _teamSeasonQuery.GetCurrentForTeam(teamId);
        }

        public TeamSeason Get(int id)
        {
            return _teamSeasonQuery.Get(id);
        }

        public int Add(TeamSeason teamSeason)
        {
            return _teamSeasonQuery.Add(teamSeason);
        }

        public void CreateForNewGame(IEnumerable<Team> teamList, int seasonId, int gameDetailsId)
        {
            var index = 0;
            var teamSeasons = new List<TeamSeason>();

            foreach (var team in teamList)
            {
                index++;
                if (index > 12)
                    index = 1;

                var id = Add(new TeamSeason
                    {
                        DivisionId = team.DivisionId,
                        SeasonId = seasonId,
                        TeamId = team.Id,
                        GameDetailsId = gameDetailsId,
                        Position = index
                    });
                teamSeasons.Add(Get(id));
            }
            _fixtureGenerator.GenerateForSeason(teamSeasons);
        }

        // Create for a new season
        // Does promotion / relegation
        public int CreateForNewSeason(int gameDetailsId, int oldSeasonId, int newSeasonId)
        {
            var currentTeamSeasons = GetByGameAndSeason(gameDetailsId, oldSeasonId);

            var teamSeasons = new List<TeamSeason>();

            foreach (var teamSeason in currentTeamSeasons)
            {
                var newPosition = teamSeason.Position;

                //Recalculate(teamSeason.Id); ??
                var newDivision = teamSeason.DivisionId;
                if ((teamSeason.Position < 3)
                    && (teamSeason.DivisionId > 1))
                {
                    newDivision--;
                    newPosition = 10 + newPosition;

                    // Add news item for promotion
                    _newsQuery.Add(new News
                    {
                        GameDetailsId = gameDetailsId,
                        TeamId = teamSeason.TeamId,
                        SeasonId = oldSeasonId,
                        Week = 23,
                        DivisionId = teamSeason.DivisionId,
                        NewsText = _teamQuery.Get(teamSeason.TeamId).Name + " promoted to division " + newDivision
                    });
                }
                if ((teamSeason.Position > 10)
                    && (teamSeason.DivisionId < 4))
                {
                    newDivision++;
                    newPosition = newPosition - 10;
                    // add news item for relegation
                    _newsQuery.Add(new News
                    {
                        GameDetailsId = gameDetailsId,
                        TeamId = teamSeason.TeamId,
                        SeasonId = oldSeasonId,
                        Week = 23,
                        DivisionId = teamSeason.DivisionId,
                        NewsText = _teamQuery.Get(teamSeason.TeamId).Name + " relegated to division " + newDivision
                    });
                }

                var id = Add(new TeamSeason
                {
                    DivisionId = newDivision,
                    GameDetailsId = gameDetailsId,
                    SeasonId = newSeasonId,
                    TeamId = teamSeason.TeamId,
                    Played = 0,
                    Won = 0,
                    Drawn = 0,
                    Lost = 0,
                    GoalsFor = 0,
                    GoalsAgainst = 0,
                    Points = 0,
                    Position = newPosition
                });

                teamSeasons.Add(Get(id));
            }

            _fixtureGenerator.GenerateForSeason(teamSeasons);

            return 0;
        }

        public int Recalculate(int id)
        {
            var result = _teamSeasonQuery.Recalculate(id);

            return result;
        }

        public void RecalculateDivisionPositions(int seasonId, int divisionId)
        {
            _teamSeasonQuery.RecalculateDivisionPositions(seasonId, divisionId);
        }

        public void RecalculateAll(int seasonId, int divisionId)
        {
            var teamSeasons = GetBySeasonAndDivision(seasonId, divisionId);
            foreach (var teamSeason in teamSeasons)
            {
                Recalculate(teamSeason.Id);
            }
            RecalculateDivisionPositions(seasonId, divisionId);
        }

        #endregion
    }
}
