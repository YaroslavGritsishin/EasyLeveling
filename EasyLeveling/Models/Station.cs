namespace EasyLeveling.Models
{
 public  class Station : FRRF_Method
    {
        public int StationDifference { get; set; }
        public int ComulativeStationDifference { get; set; }
        public int ComulativeSholders { get; set; }
        public int AllMoveDistance  { get; set; }
        public int GroundHeight { get; set; }
        public int StationHeight { get; set; }
        public int MeasureError { get; set; }



    }
}
