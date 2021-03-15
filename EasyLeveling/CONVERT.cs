using EasyLeveling.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EasyLeveling
{
    static public class CONVERT
    {
        static public List<string> ToBFFB(List<string> data)
        {
            List<string> result = new List<string>();
            string[] forCopy = new string[data.Count];
            data.CopyTo(forCopy);
            result.AddRange(forCopy);
            int index;
            foreach (var item in data)
            {
                if (item.Contains("32...8") && item.Contains("335.08"))
                {
                    index = data.IndexOf(item);
                    result.RemoveAt(index);
                    result.Insert(index + 2, item);
                }

            }
            return ChangeNumeric(result);
        }
        static public List<string> ToBBFF(List<string> data)
        {
            List<string> result = new List<string>();
            string[] forCopy = new string[data.Count];
            data.CopyTo(forCopy);
            result.AddRange(forCopy);
            int index;
            foreach (var item in data)
            {
                if (item.Contains("32...8") && item.Contains("335.08"))
                {
                    index = data.IndexOf(item);
                    result.RemoveAt(index);
                    result.Insert(index - 2, item);
                }
            }
            return ChangeNumeric(result);
        }
        static public List<string> ChangeNumeric(List<string> data, bool IsGSI_16)
        {
            int count = 1;
            List<string> result = new List<string>();
            foreach (var item in data)
            {
                if (IsGSI_16)
                {
                    if (!item.Contains("*"))
                        result.Add(item.Remove(2, 4).Insert(2, count.ToString().PadLeft(4, '0')).Insert(0, "*"));
                    else result.Add(item.Remove(3, 4).Insert(3, count.ToString().PadLeft(4, '0')));
                }

                else
                {
                    if (item.Contains("*"))
                        result.Add(item.Remove(0, 1).Remove(2, 4).Insert(2, count.ToString().PadLeft(4, '0')));
                    else
                        result.Add(item.Remove(2, 4).Insert(2, count.ToString().PadLeft(4, '0')));
                }

                count++;
            }
            return result;
        }
        static public List<string> ChangeNumeric(List<string> data)
        {
            int count = 1;
            List<string> result = new List<string>();
            foreach (var item in data)
            {
                if (item.StartsWith("*"))
                    result.Add(item.Remove(3, 4).Insert(3, count.ToString().PadLeft(4, '0')));
                else result.Add(item.Remove(2, 4).Insert(2, count.ToString().PadLeft(4, '0')));
                count++;
            }
            return result;
        }
        static public bool IsBFFB(List<string> data)
        {
            foreach (var item in data)
            {
                int index;
                if (item.Contains("32...8") && item.Contains("335.08"))
                {
                    index = data.IndexOf(item);
                    if (data[index - 2].Contains("32...8") && data[index - 2].Contains("332.08")) return true;
                    else break;
                }
            }
            return false;
        }
        static public string ToFormateGSI(int value, bool IsGSI_16)
        {
            if (IsGSI_16) return CreateFormatGSI_16(value);
            else return CreateFormatGSI_8(value);
        }
        static public string ToFormateGSI(string value, bool IsGSI_16)
        {
            if (IsGSI_16) return CreateFormatGSI_16(value);
            else return CreateFormatGSI_8(value);
        }
        static public string ToStationStringBuilder(string pointName, int distance, string code, int levelHeight, int measureCount, int measureSKO, bool IsGSI_16)
        {
            string result = $"110001+{CONVERT.ToFormateGSI(pointName, IsGSI_16)} 32...8{CONVERT.ToFormateGSI(distance, IsGSI_16)} {code}{CONVERT.ToFormateGSI(levelHeight, IsGSI_16)} 390...{CONVERT.ToFormateGSI(measureCount, IsGSI_16)} 391.08{CONVERT.ToFormateGSI(measureSKO, IsGSI_16)} ";
            return result;
        }
        //static public string ToStationStringBuilder(Station station, bool IsGSI_16)
        //{
        //    string result = $"110001+{CONVERT.ToFormateGSI(station.FrontPointName, IsGSI_16)} 571.08{CONVERT.ToFormateGSI(station.StationDifference, IsGSI_16)} 572.08{CONVERT.ToFormateGSI(station.ComulativeStationDifference, IsGSI_16)} 573.08{CONVERT.ToFormateGSI(station.ComulativeSholders, IsGSI_16)} 574.08{CONVERT.ToFormateGSI(station.AllMoveDistance, IsGSI_16)} 83..08{CONVERT.ToFormateGSI(station.GroundHeight, IsGSI_16)} ";
        //    return result;
        //}
        static public string ToStationStringBuilder(Station station, bool IsGSI_16, bool IsBBFF = false)
        {
            string result = string.Empty;
            if (IsBBFF)
            {
                result = $"110001+{CONVERT.ToFormateGSI(station.FrontPointName, IsGSI_16)} 573.08{CONVERT.ToFormateGSI(station.ComulativeSholders, IsGSI_16)} 574.08{CONVERT.ToFormateGSI(station.AllMoveDistance, IsGSI_16)} 83..08{CONVERT.ToFormateGSI(station.GroundHeight, IsGSI_16)} ";
            }
            else
            {
                result = $"110001+{CONVERT.ToFormateGSI(station.FrontPointName, IsGSI_16)} 571.08{CONVERT.ToFormateGSI(station.StationDifference, IsGSI_16)} 572.08{CONVERT.ToFormateGSI(station.ComulativeStationDifference, IsGSI_16)} 573.08{CONVERT.ToFormateGSI(station.ComulativeSholders, IsGSI_16)} 574.08{CONVERT.ToFormateGSI(station.AllMoveDistance, IsGSI_16)} 83..08{CONVERT.ToFormateGSI(station.GroundHeight, IsGSI_16)} ";
            }
            return result;
        }
        static public string ChangeValueInLine(string rowData, int distance, int levelHeigth, string code, bool IsGSI_16)
        {
            string result = new Regex(@"32\.\.\.8(.*?)\s").Replace(rowData, $"32...8{CONVERT.ToFormateGSI(distance, IsGSI_16)} ");
            return new Regex(@"33\d\.08(.*?)\s").Replace(result, $"{code}{CONVERT.ToFormateGSI(levelHeigth, IsGSI_16)} ");
        }
        static public string ChangeValueInLine(string pointName, int groundHeight, bool IsGSI_16)
        {
            string result = $"110001+{CONVERT.ToFormateGSI(pointName, IsGSI_16)} 83..08{CONVERT.ToFormateGSI(groundHeight, IsGSI_16)} ";
            return result;
        }


        static private string CreateFormatGSI_8(int value)
        {
            string result = Math.Abs(value).ToString().PadLeft(8, '0');
            if (value < 0) result = result.Insert(0, "-");
            else result = result.Insert(0, "+");
            return result;
        }
        static private string CreateFormatGSI_8(string value)
        {
            return value.PadLeft(8, '0');
        }
        static private string CreateFormatGSI_16(int value)
        {
            string result = Math.Abs(value).ToString().PadLeft(16, '0');
            if (value < 0) result = result.Insert(0, "-");
            else result = result.Insert(0, "+");
            return result;
        }
        static private string CreateFormatGSI_16(string value)
        {
            return value.PadLeft(16, '0');
        }

    }
}
