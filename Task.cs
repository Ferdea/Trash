

namespace Names
{
    public class Task
    {
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