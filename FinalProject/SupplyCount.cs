using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    [Serializable]
    public enum SupplyType
    {
        BlackWhitePaper,
        ColorPaper,
        TabloidBlackWhitePaper,
        TabloidColorPaper,
        TableSpray,
        Wipes,
        PaperTowel,
        SprayCan,
        Stapler,
        StaplerRefill,
        TapeDispenser,
        TapeRefill,
        Scissors,
        DryEraseMarker,
        DryEraseEraser,
        Clock
    }

    [Serializable]
    public class SupplyCount
    {
        string siteId;
        SupplyType type;
        int count;
        int level; // 0 to 100
        int threshold;

        public string SiteId { get { return siteId; } set { siteId = value; } }
        public SupplyType Type { get { return type; } set { type = value; } }
        public int Count { get { return count; } set { count = value; } }
        public int Level { get { return level; } set { level = value; } }
        public int Threshold { get { return threshold; } set { threshold = value; } }

        public SupplyCount() { }
        public SupplyCount(string siteId, SupplyType type, int count, int level)
        {
            siteId = siteId;
            type = type;
            count = count;
            level = level;
        }
    }
}
