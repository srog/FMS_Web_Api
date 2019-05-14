using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Fms_Web_Api.TemplateData;
using Microsoft.Extensions.Configuration;

namespace Fms_Web_Api.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamQuery _teamQuery;
        private readonly IConfiguration _configuration;

        public TeamService(ITeamQuery teamQuery, IConfiguration config)
        {
            _teamQuery = teamQuery;
            _configuration = config;
        }

        public Team Get(int id)
        {
            return _teamQuery.Get(id);
        }

        public int Add(Team team)
        {
            return _teamQuery.Add(team);
        }

        public int Update(Team team)
        {
            return _teamQuery.Update(team);
        }

        public void CreateAllTeamsForGame(int gameId)
        {
            var numberOfDivisions = _configuration.GetValue<int>("NumberOfDivisions");
            var numberOfTeamsInDivision = _configuration.GetValue<int>("TeamsInDivision");

            for (var division = 1; division <= numberOfDivisions; division++)
            {
                for (var teamIndex = 1; teamIndex <= numberOfTeamsInDivision; teamIndex++)
                {
                    var cash = Utilities.Utilities.GetRandomNumber(((5 - division) * 20000), (5 - division) * 1000000);
                    if (division == 1)
                    {
                        cash += Utilities.Utilities.GetRandomNumber(1, 100000000);
                    }
                    var stadiumCapacity = Utilities.Utilities.GetRandomNumber((5 - division) * 1000, (5 - division) * 20000);

                    var newTeam = new Team
                        {
                            DivisionId = division,
                            GameDetailsId = gameId,
                            Cash = cash,
                            FormationId = Utilities.Utilities.GetRandomNumber(1, 5),
                            Name = TeamTemplates.TeamNames[(division - 1) * numberOfTeamsInDivision + (teamIndex - 1)],
                            YearFormed = Utilities.Utilities.GetRandomNumber(1870, 1950),
                            StadiumCapacity = stadiumCapacity
                    };
                    _teamQuery.Add(newTeam);
                }
            }
        }

        public List<Team> GetTeamsForGame(int gameId)
        {
            return _teamQuery.GetByGame(gameId).ToList();
        }
    }
}
