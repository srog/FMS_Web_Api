﻿using Fms_Web_Api.Models;
using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;

namespace Fms_Web_Api.Data.Queries
{
    public class TeamQuery : Query, ITeamQuery
    {
        private const string GET_ALL = "spGetAllTeams";
        private const string GET_ALL_BY_DIVISION = "spGetTeamsByDivision";
        private const string GET_ALL_BY_GAME = "spGetTeamsByGame";
        private const string GET = "spGetTeamById";
        private const string INSERT = "spInsertTeam";
        private const string UPDATE = "spUpdateTeam";
        private const string DELETE = "spDeleteTeam";
        
        public IEnumerable<Team> GetAll()
        {
            return GetAll<Team>(GET_ALL);
        }

        public IEnumerable<Team> GetByGame(int gameDetailsId)
        {
            return GetAllById<Team>(GET_ALL_BY_GAME, "gameDetailsId", gameDetailsId);
        }
        public Team Get(int id)
        {
            return GetSingle<Team>(GET, id);
        }
        public int Add(Team team)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    { "cash", team.Cash },
                    { "divisionId", team.DivisionId },
                    { "name", team.Name },
                    { "stadiumCapacity", team.StadiumCapacity },
                    { "yearFormed", team.YearFormed },
                    { "gameDetailsId", team.GameDetailsId },
                    { "formationId", team.FormationId }
                });
        }
        public int Update(Team team)
        {
            return Update(UPDATE, new { team.Id, team.Cash, team.DivisionId, team.Name, team.StadiumCapacity, team.YearFormed, team.FormationId });
        }
        public int Delete(int id)
        {
            return Delete(DELETE,id);
        }

    }


}