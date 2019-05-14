using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Enums;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Fms_Web_Api.Utilities;
using Microsoft.Extensions.Configuration;

namespace Fms_Web_Api.Services
{
    public class PlayerCreatorService : IPlayerCreatorService
    {
        private readonly IConfiguration _configuration;
        private IPlayerQuery _playerQuery { get; }
        private IPlayerAttributeQuery _playerAttributeQuery { get; }
        private IPlayerStatsQuery _playerStatsQuery { get; }

        public PlayerCreatorService(
            IPlayerQuery playerQuery, 
            IPlayerAttributeQuery playerAttributeQuery, 
            IPlayerStatsQuery playerStatsQuery,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _playerQuery = playerQuery;
            _playerAttributeQuery = playerAttributeQuery;
            _playerStatsQuery = playerStatsQuery;
        }

        // Primitive
        public void CreateAllPlayersForGame(IEnumerable<Team> teamList)
        {
            var gameDetailsId = teamList.First().GameDetailsId;

            foreach (var team in teamList)
            {
                2.TimesWithIndex((i) => CreatePlayer(team.Id, 1, gameDetailsId));
                5.TimesWithIndex((i) => CreatePlayer(team.Id, 2, gameDetailsId));
                5.TimesWithIndex((i) => CreatePlayer(team.Id, 3, gameDetailsId));
                5.TimesWithIndex((i) => CreatePlayer(team.Id, 4, gameDetailsId));

                RecalculateSquadValues(team);
                SetInitialTeamSelection(team);
            }

            // add transfer pool players
            25.TimesWithIndex((i) => CreatePlayer(0, Utilities.Utilities.GetRandomNumber(1, 4), gameDetailsId));
        }

        private void RecalculateSquadValues(Team team)
        {
            
        }

        // Primitive
        private void SetInitialTeamSelection(Team team)
        {
            var formations = _configuration.GetSection("FormationSection").Get<Formations>();
            var teamFormation = formations.FormationList.First(f => f.Id == team.FormationId);

            var allPlayers = _playerQuery.GetAll(new Player {TeamId = team.Id});
            foreach (var player in allPlayers)
            {
                player.TeamSelection = 0;
            }

            // GK
            var playerSelected = GetNextPlayerForPosition(allPlayers, PositionEnum.Goalkeeper);
            if (playerSelected > 0)
            {
                allPlayers.First(p => p.Id == playerSelected).TeamSelection = 1;
            }

            // Def
            for (var def = 1; def <= teamFormation.Defenders; def++)
            {
                var defenderSelected = GetNextPlayerForPosition(allPlayers, PositionEnum.Defender);
                if (defenderSelected > 0)
                {
                    allPlayers.First(p => p.Id == defenderSelected).TeamSelection = 1 + def;
                }
            }

            // Mid
            for (var mid = 1; mid <= teamFormation.Midfielders; mid++)
            {
                var midSelected = GetNextPlayerForPosition(allPlayers, PositionEnum.Midfielder);
                if (midSelected > 0)
                {
                    allPlayers.First(p => p.Id == midSelected).TeamSelection = 1 + teamFormation.Defenders + mid;
                }
            }

            // Att
            for (var att = 1; att <= teamFormation.Attackers; att++)
            {
                var attSelected = GetNextPlayerForPosition(allPlayers, PositionEnum.Striker);
                if (attSelected > 0)
                {
                    allPlayers.First(p => p.Id == attSelected).TeamSelection = 1 + teamFormation.Defenders + teamFormation.Midfielders + att;
                }
            }

            // If any positions haven't been filled....do something !

            // Update with new selections
            foreach (var player in allPlayers)
            {
                _playerQuery.Update(player);
            }

        }

        private int GetNextPlayerForPosition(IEnumerable<Player> playerList, PositionEnum position)
        {
            var playerSelected = 0;
            var playerSelectedRating = 0;
            foreach (var p in playerList.Where(p => p.Position == position.GetHashCode() && p.InjuredWeeks == 0 && p.TeamSelection == 0))
            {
                if (p.Rating > playerSelectedRating)
                {
                    playerSelected = p.Id;
                    playerSelectedRating = p.Rating;
                }
            }
            return playerSelected;
        }

        // change to decorator pattern !
        private void CreatePlayer(int teamId, int position, int gameDetailsId)
        {
            // create basic player info
            var player = new Player
            {
                TeamId = teamId,
                Age = Utilities.Utilities.GetRandomNumber(18, 38),
                Position = position,
                Name = Utilities.Utilities.GetRandomName(),
                GameDetailsId = gameDetailsId
            };
            var playerId = _playerQuery.Add(player);
            player.Id = playerId;

            // create attributes
            var attributeList = CreateNewPlayerAttributes(player);
            foreach (var attr in attributeList)
            {
                _playerAttributeQuery.Add(attr);
            }

            // create stats
            var result = _playerStatsQuery.Add(new PlayerStats { PlayerId=player.Id});

            // set player rating and value
            player.Rating = RecalculatePlayerRating(attributeList);
            player.Value = RecalculatePlayerValue(attributeList);

            _playerQuery.Update(player);


        }
        // Primitive !
        private int RecalculatePlayerRating(IEnumerable<PlayerAttribute> attributeList)
        {
            var total = attributeList.Sum(a => a.AttributeValue);
            return (total / 12);
        }

        // Primitive !
        private int RecalculatePlayerValue(IEnumerable<PlayerAttribute> attributeList)
        {
            var total = attributeList.Sum(a => a.AttributeValue);
            int mean = (int)(total / 12);

            if (mean > 80)
            {
                return mean * 1000000;
            }
            if (mean > 50)
            {
                return ((mean - 50) * 15000);
            }
            if (mean < 25)
            {
                return 0;
            }
            return (mean * 500);
        }

        private IEnumerable<PlayerAttribute> CreateNewPlayerAttributes(Player player)
        {
            var attributeList = new List<PlayerAttribute>();


            var handling = Utilities.Utilities.GetRandomNumber(1, 20);
            if (player.Position == 1)
            {
                handling += Utilities.Utilities.GetRandomNumber(30, 70);
            }
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Handling, handling));

            var defense = Utilities.Utilities.GetRandomNumber(10, 40);
            if (player.Position == 2)
            {
                defense += Utilities.Utilities.GetRandomNumber(20, 50);
            }
            if (player.Position == 3)
            {
                defense += Utilities.Utilities.GetRandomNumber(1, 30);
            }
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Defending, defense));

            var tackling = Utilities.Utilities.GetRandomNumber(10, 40);
            if (player.Position == 2)
            {
                tackling += Utilities.Utilities.GetRandomNumber(20, 50);
            }
            if (player.Position == 3)
            {
                tackling += Utilities.Utilities.GetRandomNumber(1, 30);
            }

            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Tackling, tackling));


            var shooting = Utilities.Utilities.GetRandomNumber(1, 20);
            shooting += (player.Position.Value * Utilities.Utilities.GetRandomNumber(1, 20));
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Shooting, shooting));

            var skills = Utilities.Utilities.GetRandomNumber(1, 20);
            skills += (player.Position.Value * Utilities.Utilities.GetRandomNumber(1, 20));
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Skills, skills));

            var passing = Utilities.Utilities.GetRandomNumber(1, 40);
            passing += (player.Position.Value * Utilities.Utilities.GetRandomNumber(1, 15));
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Passing, passing));

            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Form, Utilities.Utilities.GetRandomNumber(10, 90)));
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Aggression,
                Utilities.Utilities.GetRandomNumber(30, 80)));
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Crossing,
                Utilities.Utilities.GetRandomNumber(30, 80)));
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Morale,
                Utilities.Utilities.GetRandomNumber(10, 90)));
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Happiness,
                Utilities.Utilities.GetRandomNumber(30, 90)));
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Speed, Utilities.Utilities.GetRandomNumber(30, 90)));
            attributeList.Add(
                CreateAttribute(player.Id, PlayerAttributeEnum.Stamina, Utilities.Utilities.GetRandomNumber(10, 90)));
            attributeList.Add(CreateAttribute(player.Id, PlayerAttributeEnum.Strength,
                Utilities.Utilities.GetRandomNumber(10, 90)));

            return attributeList;
        }

        private PlayerAttribute CreateAttribute(int playerId, PlayerAttributeEnum attributeId, int value)
        {
            return new PlayerAttribute
                {
                    PlayerId = playerId,
                    AttributeId = attributeId.GetHashCode(),
                    AttributeValue = value
                };
        }       
    }    
}
