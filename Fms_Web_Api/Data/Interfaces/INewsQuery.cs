using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface INewsQuery
    {
        //IEnumerable<News> GetLatest(int gameDetailsId);
        //IEnumerable<News> GetBySeasonAndDivision(int divisionId, int seasonId);
        //IEnumerable<News> GetTeamNews(int teamId);
        //IEnumerable<News> GetAllForSeason(int seasonId);

        IEnumerable<News> GetAll(News news);
        News Get(int id);
        int Add(News news);
        void Delete(int gameDetailsId);
    }
}