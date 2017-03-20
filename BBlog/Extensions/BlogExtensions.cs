using BBlog.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace BBlog.Extensions
{
    public static class BlogExtensions
    {
        public static string GetSummary(this Blog b)
        {
            var MAXPARAGRAPHS = 2;
            var regex = new Regex("(<p[^>]*>.*?</p>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var result = regex.Matches(b.Body);
            StringBuilder bldr = new StringBuilder();
            var x = 0;
            foreach (Match m in result)
            {
                x++;
                bldr.Append(m.Value);
                if (x == MAXPARAGRAPHS) break;
            }
            return bldr.ToString();
        }
    }
}
