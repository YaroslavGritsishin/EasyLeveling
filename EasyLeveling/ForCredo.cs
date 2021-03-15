using EasyLeveling.Models;
using System;
using System.Collections.Generic;

namespace EasyLeveling
{
    static public  class ForCredo
    {
        
        static public List<string> CreateFile(List<string> data, bool IsGSI_16)
        {
            List<string> result = new List<string>();
            int currentCountLine = 0;
            int startCountLine = 0;
            string[] forCopy;
             
            foreach (var item in data)
            {
                if (currentCountLine == 0 & item.Contains("41") && item.Contains("?..."))
                {
                    currentCountLine++;
                    continue;
                }
                if (currentCountLine > 0 & item.Contains("41") && item.Contains("?..."))
                {


                    if (startCountLine > 0)
                    {
                        currentCountLine -= 1;
                        forCopy = new string[currentCountLine - startCountLine];
                        data.CopyTo(startCountLine, forCopy, 0, currentCountLine - startCountLine);
                        result.AddRange(DivideIn2Move(forCopy, IsGSI_16).Item1);
                        result.AddRange(DivideIn2Move(forCopy, IsGSI_16).Item2);
                        startCountLine = currentCountLine;
                        currentCountLine++;
                    }
                    else
                    {
                        forCopy = new string[currentCountLine - startCountLine];
                        data.CopyTo(startCountLine, forCopy, 0, currentCountLine - startCountLine);
                        result.AddRange(DivideIn2Move(forCopy, IsGSI_16).Item1);
                        result.AddRange(DivideIn2Move(forCopy, IsGSI_16).Item2);
                        startCountLine = currentCountLine;
                        currentCountLine++;
                    }

                }
                if (currentCountLine == data.Count)
                {
                    forCopy = new string[currentCountLine - startCountLine];
                    data.CopyTo(startCountLine, forCopy, 0, currentCountLine - startCountLine);
                    result.AddRange(DivideIn2Move(forCopy, IsGSI_16).Item2);
                    result.AddRange(DivideIn2Move(forCopy, IsGSI_16).Item1);
                    startCountLine = currentCountLine;
                    currentCountLine++;
                }
                currentCountLine++;
            }
            if (result.Count == 0)
            {
                forCopy = new string[currentCountLine - startCountLine];
                data.CopyTo(startCountLine, forCopy, 0, currentCountLine - startCountLine);
                result.AddRange(DivideIn2Move(forCopy, IsGSI_16).Item1);
                result.AddRange(DivideIn2Move(forCopy, IsGSI_16).Item2);
            }
            return CONVERT.ChangeNumeric(result, IsGSI_16);
        }
         
        static private Tuple<List<string>, List<string>> DivideIn2Move(string[] data, bool IsGSI_16)
        {
            int row = 2;
            List<string> FrontMove = new List<string>();
            List<string> BackMove = new List<string>();

            FrontMove.AddRange(data);
            BackMove.AddRange(data);

            for (int count = 4; count < FrontMove.Count; count += 3)
            {
                FrontMove.RemoveRange(count, 2);
            }

            for (int count = 2; count < BackMove.Count; count += 3)
            {
                BackMove.RemoveRange(count, 2);
            }
            BackMove.Reverse();
            BackMove.Insert(0, BackMove[BackMove.Count - 2]);
            BackMove.Insert(0, BackMove[BackMove.Count - 1]);
            BackMove.RemoveRange(BackMove.Count - 2, 2);
            BackMove.Insert(BackMove.Count, BackMove[2]);
            BackMove.RemoveAt(2);
            var data1 = BackMove[1].Remove(7, 8).Insert(7, BackMove[BackMove.Count - 1].Substring(7, 8));
            BackMove.RemoveAt(1);
            BackMove.Insert(1, data1);

            for (int i = 3; i < BackMove.Count; i += 3)
            {
                BackMove.Insert(row, BackMove[i]);
                BackMove.RemoveAt(i + 1);
                row += 3;
            }
            var data2 = BackMove[BackMove.Count - 1].Remove(7, 8).Insert(7, BackMove[BackMove.Count - 2].Substring(7, 8));
            BackMove.RemoveAt(BackMove.Count - 1);
            BackMove.Add(data2);
            return Tuple.Create(Recalculate(FrontMove,IsGSI_16), Recalculate(BackMove, IsGSI_16));
        }
        static private List<string> Recalculate(List<string> data, bool IsGSI_16)
        {

            int count = 1;
            List<string> result = new List<string>();
            Station station = new Station();
            foreach (var item in data)
            {
                if (item.Contains("41") && item.Contains("?..."))
                {
                    result.Add(item);
                    continue;
                }
                if (item.Contains("83..") && !item.Contains("571.08"))
                {
                    station.GroundHeight = GET.GroundHeight(item);
                    result.Add(CONVERT.ChangeValueInLine(GET.PointName(item), station.GroundHeight, IsGSI_16));
                }
                if (item.Contains("32...8"))
                {
                    if (count == 1)
                    {
                        result.Add(item.Replace(" 336.08", " 331.08"));
                        station.RearPointName = GET.PointName(item);
                        station.RearPointDistance = GET.Distance(item);
                        station.RearLevelHeight = GET.LevelHeight(item);
                        count++;
                        continue;
                    }
                    if (count == 2)
                    {
                        result.Add(item.Replace(" 335.08", " 332.08"));
                        station.FrontPointName = GET.PointName(item);
                        station.FrontPointDistance = GET.Distance(item);
                        station.FrontLevelHeight = GET.LevelHeight(item);
                        count = 1;
                        continue;
                    }
                }
                if (item.Contains("571.08") && item.Contains("83.."))
                {
                    station.AllMoveDistance += station.RearPointDistance + station.FrontPointDistance;
                    station.ComulativeSholders += station.RearPointDistance - station.FrontPointDistance;
                    station.GroundHeight += station.RearLevelHeight - station.FrontLevelHeight;
                    station.StationDifference = (int)GET.IntroductionError(7);
                    station.ComulativeStationDifference += station.StationDifference;
                    result.Add(CONVERT.ToStationStringBuilder(station, IsGSI_16, true));
                }
            }
            return result;
        }
        
    }
}
