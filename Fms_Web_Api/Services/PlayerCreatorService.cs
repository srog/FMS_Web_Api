﻿using System.Collections.Generic;
using System.Linq;
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
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;
        private readonly IPlayerAttributeService _playerAttributeService;
        private readonly IPlayerStatsService _playerStatsService;
        private readonly INewsService _newsService;

        public PlayerCreatorService(
            IPlayerService playerService, 
            ITeamService teamService,
            IPlayerAttributeService playerAttributeService, 
            IPlayerStatsService playerStatsService,
            INewsService newsService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _playerService = playerService;
            _teamService = teamService;
            _newsService = newsService;
            _playerAttributeService = playerAttributeService;
            _playerStatsService = playerStatsService;
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
                _playerService.SetTeamSelection(team);
            }

            SetInitialInjuries(teamList.ToList());

            // add transfer pool players
            25.TimesWithIndex((i) => CreatePlayer(0, Utilities.Utilities.GetRandomNumber(1, 4), gameDetailsId));
        }

        private void SetInitialInjuries(List<Team> teamList)
        {
            var numInjuries = Utilities.Utilities.GetRandomNumber(1, teamList.Count);
            for (var index = 1; index <= numInjuries; index++)
            {
                var teamIndex = Utilities.Utilities.GetRandomNumber(1, teamList.Count);
                var team = teamList[teamIndex - 1];
                var player = _playerService.Get(_playerService.GetRandomPlayerFromTeam(team.Id));

                player.InjuredWeeks += Utilities.Utilities.GetRandomNumber(1, 10); // Could extend an existing injury !
                _playerService.Update(player);

                // adjust team selection
                if (player.IsSelected)
                {
                    _playerService.SetTeamSelection(_teamService.Get(player.TeamId.Value));
                }

                // Add news item
                var newsDescription = _playerService.GetPlayerName(player.Id) + "(" + _teamService.GetTeamName(team.Id)
                                      + ") out injured for " + player.InjuredWeeks + " weeks";
                _newsService.Add(new News
                    {
                        GameDetailsId = teamList[0].GameDetailsId,
                        DivisionId = team.DivisionId,
                        PlayerId = player.Id,
                        SeasonId = 0,
                        TeamId = team.Id,
                        Week = 0,
                        NewsText = newsDescription
                    });
            }
        }

        private void RecalculateSquadValues(Team team)
        {
            
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
            var playerId = _playerService.Add(player);
            player.Id = playerId;

            // create attributes
            var attributeList = CreateNewPlayerAttributes(player);
            foreach (var attr in attributeList)
            {
                _playerAttributeService.Add(attr);
            }

            // create stats
            _playerStatsService.Add(new PlayerStats { PlayerId=playerId});

            _playerService.RecalculateRatingAndValue(playerId);
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
