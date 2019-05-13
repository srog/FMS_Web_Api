using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Fms_Web_Api.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly ISeasonQuery _seasonQuery;
        private IConfiguration Configuration { get; }

        public SeasonService(ISeasonQuery seasonQuery, IConfiguration configuration)
        {
            _seasonQuery = seasonQuery;
            Configuration = configuration;
        }
        #region Implementation of ISeasonService

        public List<Season> GetByGame(int gameDetailsId)
        {
            return _seasonQuery.GetByGame(gameDetailsId).ToList();
        }

        public Season Get(int id)
        {
            return _seasonQuery.Get(id);
        }

        public int Add(Season season)
        {
            return _seasonQuery.Add(season);
        }

        public int AddNew(int gameDetailsId)
        {
            return _seasonQuery.Add(new Season
                {
                    GameDetailsId = gameDetailsId,
                    StartYear = Configuration.GetValue<int>("GameStartYear"),
                    Completed = false
                });
        }

        public int Update(Season season)
        {
            return _seasonQuery.Update(season);
        }

        public int Delete(int gameDetailsid)
        {
            return _seasonQuery.Delete(gameDetailsid);
        }

        #endregion
    }
}
