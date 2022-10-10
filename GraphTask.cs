using System;
using ZedGraph;

namespace Names
{
    public class GraphTask
    {
        public static GraphData GetGraphData(NameData[] names, string name)
        {
            var minYear = int.MaxValue;
            var maxYear = int.MinValue;
            foreach (var item in names)
            {
                minYear = Math.Min(minYear, item.BirthDate.Year);
                maxYear = Math.Max(maxYear, item.BirthDate.Year);
            }

            var years = new string[maxYear - minYear + 1];
            for (var i = 0; i < years.Length; i++)
                years[i] = (minYear + i).ToString();

            var data = new double[maxYear - minYear + 1];
            foreach (var item in names)
            {
                if (item.Name == name)
                {
                    data[item.BirthDate.Year - minYear]++;
                }
            }

            return new GraphData(name, years, data);
        }
        
        public static int GetData(NameData[] names, string name)
        {
            var birthCount = 0;
            foreach (var item in names)
            {
                if (item.Name == name)
                    birthCount++;
            }

            return birthCount;
        }
    }
}