using System;

namespace Fms_Web_Api.Utilities
{
    public class Utilities
    {
        public static int GetRandomNumber(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        public static string GetRandomName()
        {
            var nameGen = RandomNameGenerator.NameGenerator.Generate(RandomNameGenerator.Gender.Male);

            return nameGen;
        }
    }

    public static class IntegerExtensions
    {
        public static void TimesWithIndex(this int count, Action<int> action)
        {
            for (int i = 0; i < count; i++)
                action(i);
        }
    }
}
