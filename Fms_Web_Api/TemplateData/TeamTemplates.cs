using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.TemplateData
{
    public static class TeamTemplates
    {
        public static List<string> TeamNames = new List<string>
            {
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                "test",
                "test"
            };

        public static List<Team> TeamsTemplate = new List<Team>
            {
                new Team {Division = 1, Cash = Utilities.Utilities.GetRandomNumber(250000, 100000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(20000, 80000), Name = TeamNames[0], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 1, Cash = Utilities.Utilities.GetRandomNumber(250000, 100000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(20000, 80000), Name = TeamNames[1], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 1, Cash = Utilities.Utilities.GetRandomNumber(250000, 100000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(20000, 80000), Name = TeamNames[2], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 1, Cash = Utilities.Utilities.GetRandomNumber(250000, 100000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(20000, 80000), Name = TeamNames[3], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},

                new Team {Division = 2, Cash = Utilities.Utilities.GetRandomNumber(50000, 15000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(10000, 40000), Name = TeamNames[4], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 2, Cash = Utilities.Utilities.GetRandomNumber(50000, 15000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(10000, 40000), Name = TeamNames[5], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 2, Cash = Utilities.Utilities.GetRandomNumber(50000, 15000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(10000, 40000), Name = TeamNames[6], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 2, Cash = Utilities.Utilities.GetRandomNumber(50000, 15000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(10000, 40000), Name = TeamNames[7], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},

                new Team {Division = 3, Cash = Utilities.Utilities.GetRandomNumber(25000, 2000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(7500, 25000), Name = TeamNames[8], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 3, Cash = Utilities.Utilities.GetRandomNumber(25000, 2000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(7500, 25000), Name = TeamNames[9], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 3, Cash = Utilities.Utilities.GetRandomNumber(25000, 2000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(7500, 25000), Name = TeamNames[10], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 3, Cash = Utilities.Utilities.GetRandomNumber(25000, 2000000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(7500, 25000), Name = TeamNames[11], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},

                new Team {Division = 4, Cash = Utilities.Utilities.GetRandomNumber(5000, 500000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(2000, 8000), Name = TeamNames[12], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 4, Cash = Utilities.Utilities.GetRandomNumber(5000, 500000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(2000, 8000), Name = TeamNames[13], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 4, Cash = Utilities.Utilities.GetRandomNumber(5000, 500000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(2000, 8000), Name = TeamNames[14], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},
                new Team {Division = 4, Cash = Utilities.Utilities.GetRandomNumber(5000, 500000), StadiumCapacity = Utilities.Utilities.GetRandomNumber(2000, 8000), Name = TeamNames[15], YearFormed = Utilities.Utilities.GetRandomNumber(1880, 1960)},

            };
    }
}
