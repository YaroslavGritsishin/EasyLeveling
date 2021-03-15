using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EasyLeveling
{
    static public class GET
    {
        static private Random random = new Random();
        static public string PointName(string dataLine)
        {
            Match match = Regex.Match(dataLine, @"[+](.*?)\s");
            return match.Groups[1].Value.TrimStart('0');
        }
        static public int Distance(string dataLine)
        {
            Match match = Regex.Match(dataLine, @"32\.\.\.8(.*?)\s");
            string res = match.Groups[1].Value;
            if (res.Contains("-"))
            {
                res = res.Remove(0, 1).TrimStart('0');
                return (int.Parse(res) * -1);
            }
            else
            {
                res = res.Remove(0, 1).TrimStart('0');
                return int.Parse(res);
            }
        }
        static public string StringDistance(string dataLine)
        {
            Match match = Regex.Match(dataLine, @"32\.\.\.8(.*?)\s");
            return match.Groups[1].Value;
        }
        static public int LevelHeight(string dataLine)
        {
            Match match = Regex.Match(dataLine, @"33\d\.\d{2}(.*?)\s");
            string res = match.Groups[1].Value;
            if (res.Contains("-"))
            {
                res = res.Remove(0, 1).TrimStart('0');
                return (int.Parse(res) * -1);
            }
            else
            {
                res = res.Remove(0, 1).TrimStart('0');
                return int.Parse(res);
            }
        }
        static public string StringLevelHeight(string dataLine)
        {
            Match match = Regex.Match(dataLine, @"33\d\.\d{2}(.*?)\s");
            return match.Groups[1].Value;
        }
        static public int GroundHeight(string dataLine)
        {
            Match match = Regex.Match(dataLine, @"83\.\.\d{2}(.*?)\s");
            string res = match.Groups[1].Value;
            if (res.Contains("-"))
            {
                res = res.Remove(0, 1).TrimStart('0');
                return (int.Parse(res) * -1);
            }
            else
            {
                res = res.Remove(0, 1).TrimStart('0');
                if (res == string.Empty) return 0;
                else return int.Parse(res);
            }
        }
        static public int CorrectValue(List<string[]> data, string pointName)
        {
            double value = 0;

            foreach (var item in data)
            {
                if (item.Contains(pointName))
                {
                    value = Convert.ToDouble(item[1]);
                    break;
                }
            }
            value *= 1000;
            return Convert.ToInt32(value);
        }
        static public double IntroductionError(double sigma)
        {
            int value1 = random.Next(1, 1000);
            int value2 = random.Next(1, 1000);
            double result = (Math.Cos(Convert.ToDouble(2 * Math.PI) * Convert.ToDouble(value1) / 1000) * Math.Sqrt(-2 * Math.Log(Convert.ToDouble(value2) / 1000))) * sigma;
            return result;
        }
    }
}
