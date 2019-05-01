using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Models;
using Fms_Web_Api.Utilities;

namespace Fms_Web_Api.Data
{
    public interface IPlayerCreator
    {
        void CreateAllPlayers();
        void CreatePlayer(int teamId, int position);

    }
    
    public class PlayerCreator 
    {
        private static PlayerQuery _playerQuery;
        private static TeamQuery _teamQuery;
        private static PlayerAttributesQuery _playerAttributesQuery;
        private static PlayerStatsQuery _playerStatsQuery;

        public PlayerCreator()
        {
            _playerQuery = new PlayerQuery();
            _teamQuery = new TeamQuery();
            _playerAttributesQuery = new PlayerAttributesQuery();
            _playerStatsQuery = new PlayerStatsQuery();
        }

        // change to decorator pattern !
        public void CreatePlayer(int teamId, int position, int gameDetailsId)
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
            var playerAttributes = CreateNewPlayerAttributes(player);
            var attr = _playerAttributesQuery.Add(playerAttributes);

            // create stats
            var result = _playerStatsQuery.Add(new PlayerStats(player.Id));

            // set player rating and value
            player.Rating = RecalculatePlayerRating(playerAttributes);
            player.Value = RecalculatePlayerValue(playerAttributes);

            _playerQuery.Update(player);


        }
        private int RecalculatePlayerRating(PlayerAttributes attributes)
        {
            var total = attributes.Aggression +
                            attributes.Crossing +
                            attributes.Defending +
                            attributes.Form +
                            attributes.Handling +
                            attributes.Passing +
                            attributes.Shooting +
                            attributes.Skills +
                            attributes.Speed +
                            attributes.Stamina +
                            attributes.Strength +
                            attributes.Tackling;

            return (total / 12);
        }
        private int RecalculatePlayerValue(PlayerAttributes attributes)
        {
            var total = attributes.Aggression +
                attributes.Crossing +
                attributes.Defending +
                attributes.Form +
                attributes.Handling +
                attributes.Passing +
                attributes.Shooting +
                attributes.Skills +
                attributes.Speed +
                attributes.Stamina +
                attributes.Strength +
                attributes.Tackling;

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

        private PlayerAttributes CreateNewPlayerAttributes(Player player)
        {
            var handling = Utilities.Utilities.GetRandomNumber(1, 20);
            if (player.Position == 1)
            {
                handling += Utilities.Utilities.GetRandomNumber(30, 70);
            }

            var defense = Utilities.Utilities.GetRandomNumber(10, 40);
            if (player.Position == 2)
            {
                defense += Utilities.Utilities.GetRandomNumber(20, 50);
            }
            if (player.Position == 3)
            {
                defense += Utilities.Utilities.GetRandomNumber(1, 30);
            }

            var tackling = Utilities.Utilities.GetRandomNumber(10, 40);
            if (player.Position == 2)
            {
                tackling += Utilities.Utilities.GetRandomNumber(20, 50);
            }
            if (player.Position == 3)
            {
                tackling += Utilities.Utilities.GetRandomNumber(1, 30);
            }

            var shooting = Utilities.Utilities.GetRandomNumber(1, 20);
            shooting += (player.Position.Value * Utilities.Utilities.GetRandomNumber(1, 20));

            var skills = Utilities.Utilities.GetRandomNumber(1, 20);
            skills += (player.Position.Value * Utilities.Utilities.GetRandomNumber(1, 20));

            var passing = Utilities.Utilities.GetRandomNumber(1, 40);
            passing += (player.Position.Value * Utilities.Utilities.GetRandomNumber(1, 15));


            var playerAttributes = new PlayerAttributes
            {
                PlayerId = player.Id,
                Form = Utilities.Utilities.GetRandomNumber(10, 90),
                Aggression = Utilities.Utilities.GetRandomNumber(30, 80),
                Crossing = Utilities.Utilities.GetRandomNumber(30, 80),
                Defending = defense,
                Handling = handling,
                Morale = Utilities.Utilities.GetRandomNumber(10, 90),
                Happiness = Utilities.Utilities.GetRandomNumber(30, 80),
                Passing = passing,
                Shooting = shooting,
                Speed = Utilities.Utilities.GetRandomNumber(30, 90),
                Tackling = tackling,
                Skills = skills,
                Stamina = Utilities.Utilities.GetRandomNumber(10, 90),
                Strength = Utilities.Utilities.GetRandomNumber(10, 90),
            };

            return playerAttributes;
        }

        public void CreateAllPlayersForGame(IEnumerable<Team> teamList)
        {
            var gameDetailsId = teamList.First().GameDetailsId;

            foreach (var team in teamList)
            {
                2.TimesWithIndex((i) => CreatePlayer(team.Id, 1, gameDetailsId));
                5.TimesWithIndex((i) => CreatePlayer(team.Id, 2, gameDetailsId));
                5.TimesWithIndex((i) => CreatePlayer(team.Id, 3, gameDetailsId));
                5.TimesWithIndex((i) => CreatePlayer(team.Id, 4, gameDetailsId));
            }

            // add transfer pool players
            25.TimesWithIndex((i) => CreatePlayer(0, Utilities.Utilities.GetRandomNumber(1, 4), gameDetailsId));
        }
    }
    
}
