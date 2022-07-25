using System.Collections.Generic;
using System.Linq;


namespace HRMath.Infraestructure
{
    public class Utils
    {
        public static string BuildSearchPattern(string queryString)
        {
            string[] queries = queryString.Split('+');
            List<string> queryPatterns = new List<string>();
            foreach (var q in queries)
            {
                var words = q.Split(' ').Where(s => s.Length > 0);
                queryPatterns.Add($"({string.Join("|", words)})");
            }
            return string.Join(".*", queryPatterns);
        }

    }


}