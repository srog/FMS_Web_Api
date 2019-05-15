﻿using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerQuery _playerQuery;

        private static Dictionary<int, List<int>> SquadCache = new Dictionary<int, List<int>>();
        public static Dictionary<int, string> PlayerNames = new Dictionary<int, string>();

        public PlayerService(IPlayerQuery playerQuery)
        {
            _playerQuery = playerQuery;
        }
        #region Implementation of IPlayerService

        public List<Player> GetAllPlayersInGame(int gameDetailsId)
        {
            return _playerQuery.GetAll(new Player { GameDetailsId = gameDetailsId }).ToList();
        }
        public List<Player> GetTeamSquad(int teamId)
        {
            return _playerQuery.GetAll(new Player { TeamId = teamId}).ToList();
        }

        public List<Player> GetSelectedTeam(int teamId)
        {
            return _playerQuery.GetAll(new Player { TeamId = teamId }).Where(p => p.IsSelected).ToList();
        }

        public Player Get(int id)
        {
            return _playerQuery.Get(id);
        }

        public string GetPlayerName(int playerId)
        {
            if (!PlayerNames.ContainsKey(playerId))
            {
                PlayerNames.Add(playerId, Get(playerId).Name);
            }
            return PlayerNames.GetValueOrDefault(playerId);
        }

        public int Add(Player player)
        {
            return _playerQuery.Add(player);
        }

        public int Update(Player player)
        {
            return _playerQuery.Update(player);
        }

        public int Retire(int id)
        {
            return _playerQuery.RetirePlayer(id);
        }

        public int AdvanceAllAges(int gameDetailsId)
        {
            return _playerQuery.AdvanceAllAges(gameDetailsId);
        }

        public int GetRandomPlayerFromTeam(int teamId, bool includeKeeper = true, bool includeInjured = true, bool includeSuspended = true)
        {
            if (!SquadCache.ContainsKey(teamId))
            {
                var players = GetSelectedTeam(teamId);
                SquadCache.Add(teamId, players.Select(p => p.Id).ToList());
            }
            var playerList = SquadCache.GetValueOrDefault(teamId);

            var playerIndex = Utilities.Utilities.GetRandomNumber(0, playerList.Count - 1);
            return playerList[playerIndex];
        }

        public void Delete(int gameDetailsId)
        {
            _playerQuery.Delete(gameDetailsId);
        }

        #endregion
    }
}
