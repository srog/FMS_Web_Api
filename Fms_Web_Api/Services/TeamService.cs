using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Fms_Web_Api.TemplateData;

namespace Fms_Web_Api.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamQuery _teamQuery;

        public TeamService(ITeamQuery teamQuery)
        {
            _teamQuery = teamQuery;
            
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
            foreach (var team in TeamTemplates.TeamsTemplate)
            {
                team.GameDetailsId = gameId;
                _teamQuery.Add(team);
            }
        }

        public List<Team> GetTeamsForGame(int gameId)
        {
            return _teamQuery.GetByGame(gameId).ToList();
        }
    }
}
