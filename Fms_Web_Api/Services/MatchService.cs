using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchGoalQuery _matchGoalQuery;
        private readonly IPlayerService _playerService;
        private readonly IMatchQuery _matchQuery;
        private readonly IPlayerStatsQuery _playerStatsQuery;

        public MatchService(IPlayerService playerService, 
            IMatchQuery matchQuery, 
            IMatchGoalQuery matchGoalQuery, 
            IPlayerStatsQuery playerStatsQuery)
        {
            _matchGoalQuery = matchGoalQuery;
            _playerService = playerService;
            _matchQuery = matchQuery;
            _playerStatsQuery = playerStatsQuery;
        }
        #region Implementation of IMatchService

        // Primitive
        public Match PlayMatch(int id)
        {
            var match = Get(id);
            if (match.Completed.GetValueOrDefault())
            {
                return null;
            }

            match.HomeTeamScore = Utilities.Utilities.GetRandomNumber(0, 5);
            match.AwayTeamScore = Utilities.Utilities.GetRandomNumber(0, 4);

            for (var goalIndex = 1; goalIndex <= match.HomeTeamScore; goalIndex++)
            {
                CreateGoalRecord(match.Id, match.HomeTeamId.Value);
            }
            for (var goalIndex = 1; goalIndex <= match.AwayTeamScore; goalIndex++)
            {
                CreateGoalRecord(match.Id, match.AwayTeamId.Value);
            }

            match.Completed = true;
            Update(match);

            UpdatePlayerGameStats(match.HomeTeamId.Value, match.AwayTeamScore == 0);
            UpdatePlayerGameStats(match.AwayTeamId.Value, match.HomeTeamScore == 0);

            return match;
        }

        // Primitive - only selected players
        private void UpdatePlayerGameStats(int teamId, bool cleanSheet)
        {
            // update 'games', cleansheets
            var playerList = _playerService.GetTeamSquad(teamId);
            foreach (var player in playerList)
            {
                var playerStats = _playerStatsQuery.Get(player.Id);
                playerStats.Games++;
                playerStats.CleanSheets = cleanSheet ? playerStats.CleanSheets + 1 : playerStats.CleanSheets;
                _playerStatsQuery.Update(playerStats);
            }



        }

        public List<Match> GetAll(Match match)
        {
            return _matchQuery.GetAll(match).ToList();
        }

        public Match Get(int id)
        {
            return _matchQuery.Get(id);
        }

        public int Insert(Match match)
        {
            return _matchQuery.Insert(match);
        }

        public int Update(Match match)
        {
            return _matchQuery.Update(match);
        }

        // Primitive
        private void CreateGoalRecord(int matchId, int teamId)
        {
            var minute = Utilities.Utilities.GetRandomNumber(1, 90);
            var teamPlayers = _playerService.GetTeamSquad(teamId);
            var playerIndex = Utilities.Utilities.GetRandomNumber(0, teamPlayers.Count - 1);
            var assistPlayerIndex = Utilities.Utilities.GetRandomNumber(0, teamPlayers.Count - 1);
            var assistPlayerId = 0;

            if (playerIndex != assistPlayerIndex)
            {
                assistPlayerId = teamPlayers[assistPlayerIndex].Id;
            }
            var playerId = teamPlayers[playerIndex].Id;
            var ownGoal = false;

            var matchGoal = new MatchGoal
            {
                MatchId = matchId,
                TeamId = teamId,
                PlayerId = playerId,
                AssistPlayerId = assistPlayerId,
                Minute = minute,
                OwnGoal = ownGoal  // TODO - some of these
            };

            _matchGoalQuery.Insert(matchGoal);

            if (!ownGoal)
            {
                var playerStats = _playerStatsQuery.Get(playerId);
                playerStats.Goals++;
                _playerStatsQuery.Update(playerStats);
            }

            if (assistPlayerId > 0)
            {
                var playerAssistStats = _playerStatsQuery.Get(assistPlayerId);
                playerAssistStats.Assists++;
                _playerStatsQuery.Update(playerAssistStats);
            }
        }

        #endregion
    }
}
