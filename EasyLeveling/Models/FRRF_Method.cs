using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLeveling.Models
{
  public class FRRF_Method: Measure
    {
        public int SpetialRearLevelHeight { get; set; }
        public int SpetialFrontLevelHeight { get; set; }
        public int SpetialRearPointDistance { get; set; }
        public int SpetialFrontPointDistance { get; set; }
    }
}
