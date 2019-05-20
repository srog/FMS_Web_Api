using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Enums;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Fms_Web_Api.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IConfiguration _configuration;
        private readonly IPlayerQuery _playerQuery;
        private readonly IPlayerAttributeService _playerAttributeService;
        private readonly ITeamService _teamService;

        private static Dictionary<int, List<int>> SquadCache = new Dictionary<int, List<int>>();
        public static Dictionary<int, string> PlayerNames = new Dictionary<int, string>();

        public PlayerService(IPlayerQuery playerQuery, 
            IPlayerAttributeService playerAttributeService, 
            IConfiguration configuration,
            ITeamService teamService)
        {
            _playerQuery = playerQuery;
            _playerAttributeService = playerAttributeService;
            _configuration = configuration;
            _teamService = teamService;
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
        
        // Primitive
        public void RecalculateRatingAndValue(int playerId)
        {
            var player = Get(playerId);
            var attributes = _playerAttributeService.GetByPlayer(playerId);
            var total = attributes.Sum(a => a.AttributeValue);
            player.Rating = total / attributes.Count;
            
            int mean = (int)(total / 12);

            if (mean > 80)
            {
                player.Value = mean * 1000000;
            }
            if (mean > 50)
            {
                player.Value = (mean - 50) * 15000;
            }
            if (mean < 25)
            {
                player.Value = 0;
            }
            player.Value = (mean * 500);

            Update(player);
        }

        public int Retire(int id)
        {
            return _playerQuery.RetirePlayer(id);
        }

        public int AdvanceSeason(int gameDetailsId)
        {
            return AdvanceAllAges(gameDetailsId);
        }

        public int AdvanceAllAges(int gameDetailsId)
        {
            return _playerQuery.AdvanceAllAges(gameDetailsId);
        }

        // advance injuries and suspensions. Recalculate team selections.
        // TODO - training, recalculate ratings, values
        public int AdvanceWeek(GameDetails gameDetails)
        {
            var teamsChangedList = new List<int>();
            var injuredPlayerList = GetAllPlayersInGame(gameDetails.Id).Where(p => p.InjuredWeeks > 0);
            foreach (var player in injuredPlayerList)
            {
                player.InjuredWeeks--;
                Update(player);
                if (!teamsChangedList.Contains(player.TeamId.Value))
                {
                    teamsChangedList.Add(player.TeamId.Value);
                }
            }
            var suspendedPlayerList = GetAllPlayersInGame(gameDetails.Id).Where(p => p.SuspendedWeeks > 0);
            foreach (var player in suspendedPlayerList)
            {
                player.SuspendedWeeks--;
                Update(player);
                if (!teamsChangedList.Contains(player.TeamId.Value))
                {
                    teamsChangedList.Add(player.TeamId.Value);
                }
            }

            foreach(var team in teamsChangedList)
            {
                if (team != gameDetails.TeamId)
                {
                    SetTeamSelection(_teamService.Get(team));
                }
            }
            return 1;
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

        // Primitive
        public void SetTeamSelection(Team team)
        {
            var formations = _configuration.GetSection("FormationSection").Get<Formations>();
            var teamFormation = formations.FormationList.First(f => f.Id == team.FormationId);

            var allPlayers = GetTeamSquad(team.Id);
            foreach (var player in allPlayers)
            {
                player.TeamSelection = 0;
            }

            // GK
            var playerSelected = GetNextAvailablePlayerForPosition(allPlayers, PositionEnum.Goalkeeper);
            if (playerSelected > 0)
            {
                allPlayers.First(p => p.Id == playerSelected).TeamSelection = 1;
            }

            // Def
            for (var def = 1; def <= teamFormation.Defenders; def++)
            {
                var defenderSelected = GetNextAvailablePlayerForPosition(allPlayers, PositionEnum.Defender);
                if (defenderSelected > 0)
                {
                    allPlayers.First(p => p.Id == defenderSelected).TeamSelection = 1 + def;
                }
            }

            // Mid
            for (var mid = 1; mid <= teamFormation.Midfielders; mid++)
            {
                var midSelected = GetNextAvailablePlayerForPosition(allPlayers, PositionEnum.Midfielder);
                if (midSelected > 0)
                {
                    allPlayers.First(p => p.Id == midSelected).TeamSelection = 1 + teamFormation.Defenders + mid;
                }
            }

            // Att
            for (var att = 1; att <= teamFormation.Attackers; att++)
            {
                var attSelected = GetNextAvailablePlayerForPosition(allPlayers, PositionEnum.Striker);
                if (attSelected > 0)
                {
                    allPlayers.First(p => p.Id == attSelected).TeamSelection = 1 + teamFormation.Defenders + teamFormation.Midfielders + att;
                }
            }

            // If any positions haven't been filled....do something !

            // Update with new selections
            foreach (var player in allPlayers)
            {
                Update(player);
            }

        }

        private int GetNextAvailablePlayerForPosition(IEnumerable<Player> playerList, PositionEnum position)
        {
            var playerSelected = 0;
            var playerSelectedRating = 0;
            foreach (var p in playerList.Where(p => p.Position == position.GetHashCode() && p.IsAvailable && p.TeamSelection == 0))
            {
                if (p.Rating > playerSelectedRating)
                {
                    playerSelected = p.Id;
                    playerSelectedRating = p.Rating;
                }
            }
            return playerSelected;
        }

        public void Delete(int gameDetailsId)
        {
            _playerQuery.Delete(gameDetailsId);
        }

        #endregion
    }
}
