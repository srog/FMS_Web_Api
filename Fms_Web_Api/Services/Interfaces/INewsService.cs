﻿using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface INewsService
    {
        List<News> GetAll(News news);
        News Get(int id);
        int Add(News news);
        void Delete(int gameDetailsId);
    }
}
