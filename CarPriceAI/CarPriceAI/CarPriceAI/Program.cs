using System;
using System.Linq;

namespace CarPriceAI
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = new DataGatherer().GatherData()
                 .GetAwaiter()
                 .GetResult()
                 .ToList();
        }
    }
}
