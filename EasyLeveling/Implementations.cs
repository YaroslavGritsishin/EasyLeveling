using EasyLeveling.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EasyLeveling
{
    public class Implementations
    {
        public List<string> RecaculateStation(List<string> DataStationFromFile, List<string[]> HeightCorrect, Station station, double sigma, bool IsGSI_16)
        {
            List<string> result = new List<string>();
            List<string> middle = new List<string>();
            int row = 1;
            int correctRearValue;
            int correctFrontValue;
            foreach (var item in DataStationFromFile)
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
                    if (row == 1)
                    {

                        station.RearPointName = GET.PointName(item);
                        station.RearPointDistance = GET.Distance(item);
                        correctRearValue = GET.CorrectValue(HeightCorrect, station.RearPointName);
                        station.StationHeight = GetStationHeight(GET.LevelHeight(item), GET.LevelHeight(DataStationFromFile[DataStationFromFile.IndexOf(item) + 1]));
                        station.MeasureError = (int)(GET.IntroductionError(sigma) * 100);
                        var SpecialRearH = GET.LevelHeight(DataStationFromFile[DataStationFromFile.IndexOf(item) + 3]);
                        station.RearLevelHeight = (GET.LevelHeight(item) + SpecialRearH) / 2 - correctRearValue + station.StationHeight + station.MeasureError;
                        station.MeasureSKO = (int)(GET.IntroductionError(0.1) * 100);
                        middle.Add(CONVERT.ToStationStringBuilder(station.RearPointName, station.RearPointDistance, "331.08", station.RearLevelHeight, 3, station.MeasureSKO, IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 2)
                    {
                        station.FrontPointName = GET.PointName(item);
                        station.FrontPointDistance = GET.Distance(item);

                        correctFrontValue = GET.CorrectValue(HeightCorrect, station.FrontPointName);
                        int SpecialFrontH = GET.LevelHeight(DataStationFromFile[DataStationFromFile.IndexOf(item) + 1]);
                        station.MeasureError = (int)(GET.IntroductionError(sigma) * 100);
                        station.FrontLevelHeight = (GET.LevelHeight(item) + SpecialFrontH) / 2 - correctFrontValue + station.MeasureError + station.StationHeight;
                        station.MeasureSKO = (int)(GET.IntroductionError(0.1) * 100);
                        middle.Add(CONVERT.ToStationStringBuilder(station.FrontPointName, station.FrontPointDistance, "332.08", station.FrontLevelHeight, 3, station.MeasureSKO, IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 3)
                    {
                        station.SpetialFrontPointDistance = GET.Distance(item);
                        var rnd = new Random();
                        int value = rnd.Next(-45, 45);
                        station.SpetialFrontLevelHeight = station.FrontLevelHeight + value;
                        station.MeasureSKO = (int)(GET.IntroductionError(0.1) * 100);
                        middle.Add(CONVERT.ToStationStringBuilder(station.FrontPointName, station.SpetialFrontPointDistance, "336.08", station.SpetialFrontLevelHeight, 3, station.MeasureSKO, IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 4)
                    {
                        station.SpetialRearPointDistance = GET.Distance(item);
                        var rnd = new Random();
                        int value = rnd.Next(-45, 45);
                        station.SpetialRearLevelHeight = station.RearLevelHeight + value;
                        station.StationHeight = 0;
                        station.MeasureSKO = (int)(GET.IntroductionError(0.1) * 100);
                        middle.Add(CONVERT.ToStationStringBuilder(station.RearPointName, station.SpetialRearPointDistance, "335.08", station.SpetialRearLevelHeight, 3, station.MeasureSKO, IsGSI_16));
                        row = 1;
                        continue;
                    }
                }
                middle.Add(item);
            }

            int NewDistance = ((station.RearPointDistance + station.FrontPointDistance + station.SpetialFrontPointDistance + station.SpetialRearPointDistance) / 2) + (int)(GET.IntroductionError(900) * 1000);
            int SholderError = (int)(GET.IntroductionError(900) * 1000);
            station.RearPointDistance = NewDistance / 2 + SholderError;
            station.FrontPointDistance = NewDistance / 2 - SholderError;
            station.SpetialFrontPointDistance = station.FrontPointDistance + (int)(GET.IntroductionError(3) * 50);
            station.SpetialRearPointDistance = station.RearPointDistance + (int)(GET.IntroductionError(3) * 50);
            station.AllMoveDistance += (station.RearPointDistance + station.FrontPointDistance + station.SpetialRearPointDistance + station.SpetialFrontPointDistance) / 2;
            station.ComulativeSholders += ((station.RearPointDistance - station.FrontPointDistance) + (station.SpetialRearPointDistance - station.SpetialFrontPointDistance)) / 2;
            station.GroundHeight += ((station.RearLevelHeight - station.FrontLevelHeight) + (station.SpetialRearLevelHeight - station.SpetialFrontLevelHeight)) / 2;
            station.StationDifference = (int)GET.IntroductionError(7);
            station.ComulativeStationDifference += station.StationDifference;
            foreach (var item in middle)
            {
                if (item.Contains("32...8"))
                {
                    if (row == 1)
                    {
                        result.Add(CONVERT.ChangeValueInLine(item, station.RearPointDistance, station.RearLevelHeight, "331.08", IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 2)
                    {
                        result.Add(CONVERT.ChangeValueInLine(item, station.FrontPointDistance, station.FrontLevelHeight, "332.08", IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 3)
                    {
                        result.Add(CONVERT.ChangeValueInLine(item, station.SpetialFrontPointDistance, station.SpetialFrontLevelHeight, "336.08", IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 4)
                    {
                        result.Add(CONVERT.ChangeValueInLine(item, station.SpetialRearPointDistance, station.SpetialRearLevelHeight, "335.08", IsGSI_16));
                        row = 1;
                        continue;
                    }
                }
                if (item.Contains("571.08") && item.Contains("83..08"))
                {
                    result.Add(CONVERT.ToStationStringBuilder(station, IsGSI_16));
                }

            }
            return result;
        }
        public List<string> MainAction(List<string> dataFromFile, List<string[]> HeightCorrect, double sigma, bool IsGSI_16)
        {
            List<string> result = new List<string>();
            List<string> middle = new List<string>();
            Station station = new Station();
            foreach (var item in dataFromFile)
            {
                if (item.Contains("41") & item.Contains("?..."))
                {
                    station.AllMoveDistance = 0;
                    station.ComulativeSholders = 0;
                    station.ComulativeStationDifference = 0;
                    station.GroundHeight = 0;
                    middle.Clear();
                }
                if (item.Contains("571.08") && item.Contains("83..08"))
                {
                    middle.Add(item);
                    result.AddRange(RecaculateStation(middle, HeightCorrect, station, sigma, IsGSI_16));
                    middle.Clear();
                    continue;
                }
                middle.Add(item);
            }
            return CONVERT.ChangeNumeric(result, IsGSI_16);
        }


        public List<string> RecaculateStation(List<string> DataStationFromFile, List<string[]> HeightCorrect, Station station, bool IsGSI_16)
        {
            List<string> result = new List<string>();
            List<string> middle = new List<string>();
            int row = 1;
            int correctRearValue;
            int correctFrontValue;
            foreach (var item in DataStationFromFile)
            {

                if (item.Contains("41") && item.Contains("?..."))
                {
                    result.Add(item);
                    continue;
                }
                if (item.Contains("83..") && !item.Contains("571.08"))
                {
                    station.GroundHeight = GET.GroundHeight(item);
                    result.Add(item);
                }
                if (item.Contains("32...8"))
                {
                    if (row == 1)
                    {

                        station.RearPointName = GET.PointName(item);
                        station.RearPointDistance = GET.Distance(item);
                        correctRearValue = GET.CorrectValue(HeightCorrect, station.RearPointName);
                        station.StationHeight = GetStationHeight(GET.LevelHeight(item), GET.LevelHeight(DataStationFromFile[DataStationFromFile.IndexOf(item) + 1]));
                        var SpecialRearH = GET.LevelHeight(DataStationFromFile[DataStationFromFile.IndexOf(item) + 3]);
                        station.RearLevelHeight = (GET.LevelHeight(item) + SpecialRearH) / 2 - correctRearValue + station.StationHeight;
                        station.MeasureSKO = GET.SKO(item);

                        middle.Add(CONVERT.ToStationStringBuilder(station.RearPointName, station.RearPointDistance, "331.08", station.RearLevelHeight, 3, station.MeasureSKO, IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 2)
                    {
                        station.FrontPointName = GET.PointName(item);
                        station.FrontPointDistance = GET.Distance(item);
                        station.MeasureSKO = GET.SKO(item);
                        correctFrontValue = GET.CorrectValue(HeightCorrect, station.FrontPointName);
                        int SpecialFrontH = GET.LevelHeight(DataStationFromFile[DataStationFromFile.IndexOf(item) + 1]);
                        station.FrontLevelHeight = (GET.LevelHeight(item) + SpecialFrontH) / 2 - correctFrontValue + station.StationHeight;
                        middle.Add(CONVERT.ToStationStringBuilder(station.FrontPointName, station.FrontPointDistance, "332.08", station.FrontLevelHeight, 3, station.MeasureSKO, IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 3)
                    {
                        station.SpetialFrontPointDistance = GET.Distance(item);
                        station.MeasureSKO = GET.SKO(item);
                        station.SpetialFrontLevelHeight = station.FrontLevelHeight;
                        middle.Add(CONVERT.ToStationStringBuilder(station.FrontPointName, station.SpetialFrontPointDistance, "336.08", station.SpetialFrontLevelHeight, 3, station.MeasureSKO, IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 4)
                    {
                        station.SpetialRearPointDistance = GET.Distance(item);
                        station.MeasureSKO = GET.SKO(item);
                        station.SpetialRearLevelHeight = station.RearLevelHeight;
                        station.StationHeight = 0;
                        middle.Add(CONVERT.ToStationStringBuilder(station.RearPointName, station.SpetialRearPointDistance, "335.08", station.SpetialRearLevelHeight, 3, station.MeasureSKO, IsGSI_16));
                        row = 1;
                        continue;
                    }
                }
                middle.Add(item);
            }
            station.GroundHeight += ((station.RearLevelHeight - station.FrontLevelHeight) + (station.SpetialRearLevelHeight - station.SpetialFrontLevelHeight)) / 2;
            
            foreach (var item in middle)
            {
                if (item.Contains("32...8"))
                {
                    if (row == 1)
                    {
                        result.Add(CONVERT.ChangeValueInLine(item, station.RearPointDistance, station.RearLevelHeight, "331.08", IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 2)
                    {
                        result.Add(CONVERT.ChangeValueInLine(item, station.FrontPointDistance, station.FrontLevelHeight, "332.08", IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 3)
                    {
                        result.Add(CONVERT.ChangeValueInLine(item, station.SpetialFrontPointDistance, station.SpetialFrontLevelHeight, "336.08", IsGSI_16));
                        row++;
                        continue;
                    }
                    if (row == 4)
                    {
                        result.Add(CONVERT.ChangeValueInLine(item, station.SpetialRearPointDistance, station.SpetialRearLevelHeight, "335.08", IsGSI_16));
                        row = 1;
                        continue;
                    }
                }
                if (item.Contains("571.08") && item.Contains("83..08"))
                {
                    result.Add(CONVERT.ToStationStringBuilder(item,station.GroundHeight, IsGSI_16));
                }

            }
            return result;
        }
        public List<string> MainAction(List<string> dataFromFile, List<string[]> HeightCorrect, bool IsGSI_16)
        {
            List<string> result = new List<string>();
            List<string> middle = new List<string>();
            Station station = new Station();
            foreach (var item in dataFromFile)
            {
                if (item.Contains("41") & item.Contains("?..."))
                {
                    station.AllMoveDistance = 0;
                    station.ComulativeSholders = 0;
                    station.ComulativeStationDifference = 0;
                    station.GroundHeight = 0;
                    middle.Clear();
                }
                if (item.Contains("571.08") && item.Contains("83..08"))
                {
                    middle.Add(item);
                    result.AddRange(RecaculateStation(middle, HeightCorrect, station, IsGSI_16));
                    middle.Clear();
                    continue;
                }
                middle.Add(item);
            }
            return CONVERT.ChangeNumeric(result, IsGSI_16);
        }



        private int GetStationHeight(int RearLevelHeight, int FrontLevelHeight)
        {
            int result;
            if (RearLevelHeight > FrontLevelHeight)
            {
                result = (20000 - RearLevelHeight) / 3;
            }
            else result = (20000 - FrontLevelHeight) / 3;

            return result;
        }
        public void SaveFile(List<string> data, OpenFileDialog openFile, SaveFileType saveFileType)
        {
            int index = openFile.FileName.ToUpper().IndexOf("GSI") - 1;
            using (StreamWriter streamWriter = new StreamWriter(openFile.FileName.Insert(index, $"[{saveFileType}]"), false))
            {
                foreach (var item in data)
                {
                    streamWriter.WriteLine(item);
                }
                streamWriter.Close();
            }
        }
        public enum SaveFileType
        {
           CHANGED,
           CD31
        }
    }
}
