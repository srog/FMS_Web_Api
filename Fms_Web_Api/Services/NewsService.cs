﻿using System;
using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Enums;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsQuery _newsQuery;

        public NewsService(INewsQuery newsQuery)
        {
            _newsQuery = newsQuery;
        }
        #region Implementation of INewsService

        public List<News> GetAll(News news)
        {
            return _newsQuery.GetAll(news).ToList();
        }

        public News Get(int id)
        {
            return _newsQuery.Get(id);
        }

        public int Add(News news)
        {
            return _newsQuery.Add(news);
        }

        public void Delete(int gameDetailsId)
        {
            _newsQuery.Delete(gameDetailsId);
        }

        public void CreateSuspendedNewsItem(PlayerNews news, bool redCard = false)
        {
            news.News.NewsText = news.PlayerName + "(" + news.TeamName + ")";
            news.News.NewsText += (redCard ? " Sent off and" : "");
            news.News.NewsText += " suspended for " + news.Weeks + " weeks";
            Add(news.News);
        }

        public void CreateInjuredNewsItem(PlayerNews news)
        {
            news.News.NewsText = news.PlayerName + "(" + news.TeamName + ") out injured for " + news.Weeks + " weeks";
            Add(news.News);
        }

        public void CreateProRelNewsItem(PromotionNews news, bool promoted = true)
        {
            news.News.NewsText = news.TeamName = (promoted ? " promoted " : " relegated ") + "to " + Enum.GetName(typeof(DivisionEnum), news.Division);
            Add(news.News);
        }
        
        #endregion
    }
}
