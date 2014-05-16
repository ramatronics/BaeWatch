using System.Text;

namespace BW.Data.Core.Common
{
    public class Functions
    {
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in str)
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    sb.Append(c);

            return sb.ToString();
        }
    }
}