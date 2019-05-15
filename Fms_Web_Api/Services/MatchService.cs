using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Enums;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchGoalService _matchGoalService;
        private readonly IMatchEventService _matchEventService;
        private readonly IPlayerService _playerService;
        private readonly IMatchQuery _matchQuery;
        private readonly IPlayerStatsService _playerStatsService;
        private readonly INewsService _newsService;
        private readonly ITeamService _teamService;
        

        public MatchService(IPlayerService playerService, 
            IMatchQuery matchQuery, 
            IMatchGoalService matchGoalService,
            IMatchEventService matchEventService,
            IPlayerStatsService playerStatsService,
            INewsService newsService,
            ITeamService teamService)
        {
            _matchGoalService = matchGoalService;
            _playerService = playerService;
            _matchQuery = matchQuery;
            _playerStatsService = playerStatsService;
            _matchEventService = matchEventService;
            _newsService = newsService;
            _teamService = teamService;
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

            // Set attendance
            var maxAttendance = _teamService.Get(match.HomeTeamId.Value).StadiumCapacity;

            match.Attendance = Utilities.Utilities.GetRandomNumber(maxAttendance / 3, maxAttendance);

            // Goals
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

            // Events
            var homeRedQuotient = Utilities.Utilities.GetRandomNumber(0, 100);
            var awayRedQuotient = Utilities.Utilities.GetRandomNumber(0, 100);
            var homeYellowQuotient = Utilities.Utilities.GetRandomNumber(0, 100);
            var awayYellowQuotient = Utilities.Utilities.GetRandomNumber(0, 100);
            var homeCorners = Utilities.Utilities.GetRandomNumber(2, 15);
            var awayCorners = Utilities.Utilities.GetRandomNumber(1, 12);

            var homeRed = homeRedQuotient > 90 ? (homeRedQuotient > 97 ? 2 : 1) : 0;
            var awayRed = awayRedQuotient > 90 ? (awayRedQuotient > 97 ? 2 : 1) : 0;
            var homeYellow = (int) homeYellowQuotient / 15;
            var awayYellow = (int) awayYellowQuotient / 15;

            for (var index = 1; index < homeRed; index++)
            {
                CreateMatchEvent(match, match.HomeTeamId.Value, MatchEventTypesEnum.RedCard);
            }
            for (var index = 1; index < awayRed; index++)
            {
                CreateMatchEvent(match, match.AwayTeamId.Value, MatchEventTypesEnum.RedCard);
            }
            for (var index = 1; index < homeYellow; index++)
            {
                CreateMatchEvent(match, match.HomeTeamId.Value, MatchEventTypesEnum.YellowCard);
            }
            for (var index = 1; index < awayYellow; index++)
            {
                CreateMatchEvent(match, match.AwayTeamId.Value, MatchEventTypesEnum.YellowCard);
            }
            for (var index = 1; index < homeCorners; index++)
            {
                CreateMatchEvent(match, match.HomeTeamId.Value, MatchEventTypesEnum.Corner);
            }
            for (var index = 1; index < awayCorners; index++)
            {
                CreateMatchEvent(match, match.AwayTeamId.Value, MatchEventTypesEnum.Corner);
            }

            match.Completed = true;
            Update(match);

            UpdatePlayerGameStats(match.HomeTeamId.Value, match.AwayTeamScore == 0);
            UpdatePlayerGameStats(match.AwayTeamId.Value, match.HomeTeamScore == 0);

            UpdatePlayerSuspensions(match);
            UpdatePlayerInjuries(match);

            return match;
        }

        private void UpdatePlayerSuspensions(Match match)
        {
            // red cards already handled
        }

        // Primitive
        // Adds news item
        private void UpdatePlayerInjuries(Match match)
        {
            var injuryQuotient = Utilities.Utilities.GetRandomNumber(1, 100);
            if (injuryQuotient > 50)
            {
                var numInjuries = Utilities.Utilities.GetRandomNumber(0, injuryQuotient / 30);
                for (var index = 1; index <= numInjuries; index++)
                {
                    var teamId = Utilities.Utilities.GetRandomNumber(1, 20) > 10 ? match.HomeTeamId.Value : match.AwayTeamId.Value;
                    var playerId = _playerService.GetRandomPlayerFromTeam(teamId, true, false, false);
                    var weeks = Utilities.Utilities.GetRandomNumber(1, 10);

                    var player = _playerService.Get(playerId);
                    player.InjuredWeeks = weeks;
                    _playerService.Update(player);

                    
                    var news = new News
                        {
                            GameDetailsId = match.GameDetailsId,
                            DivisionId = match.DivisionId,
                            PlayerId = player.Id,
                            SeasonId = match.SeasonId,
                            TeamId = teamId,
                            Week = match.Week
                        };
                    _newsService.CreateInjuredNewsItem(new PlayerNews
                        {
                            News = news,
                            PlayerName = _playerService.GetPlayerName(player.Id),
                            TeamName = _teamService.GetTeamName(teamId),
                            Weeks = weeks
                        });
                }

            }
        }


        // Primitive - also continuity errors! 
        // updates playerstats if card
        private void CreateMatchEvent(Match match, int teamId, MatchEventTypesEnum type)
        {
            var playerId = type==MatchEventTypesEnum.Corner ? 0 : _playerService.GetRandomPlayerFromTeam(teamId, true);

            _matchEventService.Insert(new MatchEvent
                {
                    MatchId = match.Id,
                    EventType = type.GetHashCode(),
                    Minute = Utilities.Utilities.GetRandomNumber(1,90),
                    PlayerId = playerId,
                    TeamId = teamId
                });

            if (type == MatchEventTypesEnum.RedCard)
            {
                var playerStats = _playerStatsService.Get(playerId);
                playerStats.RedCards++;
                _playerStatsService.Update(playerStats);

                var player = _playerService.Get(playerId);
                player.SuspendedWeeks = 3;
                _playerService.Update(player);

                var news = new News
                    {
                        GameDetailsId = match.GameDetailsId,
                        DivisionId = match.DivisionId,
                        PlayerId = player.Id,
                        SeasonId = match.SeasonId,
                        TeamId = teamId,
                        Week = match.Week
                    };

                _newsService.CreateSuspendedNewsItem(new PlayerNews
                {
                    News = news,
                    PlayerName = _playerService.GetPlayerName(playerId),
                    TeamName = _teamService.GetTeamName(teamId),
                    Weeks = player.SuspendedWeeks
                }, true);
            }

            if (type == MatchEventTypesEnum.YellowCard)
            {
                var playerStats = _playerStatsService.Get(playerId);
                playerStats.YellowCards++;
                _playerStatsService.Update(playerStats);
            }
        }

        private void UpdatePlayerGameStats(int teamId, bool cleanSheet)
        {
            // update 'games', cleansheets
            var playerList = _playerService.GetSelectedTeam(teamId);
            foreach (var player in playerList)
            {
                var playerStats = _playerStatsService.Get(player.Id);
                playerStats.Games++;
                playerStats.CleanSheets = cleanSheet ? playerStats.CleanSheets + 1 : playerStats.CleanSheets;
                _playerStatsService.Update(playerStats);
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
            var playerId = _playerService.GetRandomPlayerFromTeam(teamId, false);
            var assistPlayerId = _playerService.GetRandomPlayerFromTeam(teamId);

            if (playerId == assistPlayerId)
            {
                assistPlayerId = 0;
            }
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

            _matchGoalService.Insert(matchGoal);

            if (!ownGoal)
            {
                var playerStats = _playerStatsService.Get(playerId);
                playerStats.Goals++;
                _playerStatsService.Update(playerStats);
            }

            if (assistPlayerId > 0)
            {
                var playerAssistStats = _playerStatsService.Get(assistPlayerId);
                playerAssistStats.Assists++;
                _playerStatsService.Update(playerAssistStats);
            }
        }

        #endregion
    }
}
