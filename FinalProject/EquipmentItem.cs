using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    [Serializable]
    public enum EquipmentType
    {
        blackWhitePrinter,
        colorPrinter,
        windowsComputer,
        macComputer,
        scanner,
        chair
    }

    [Serializable]
    public class EquipmentItem
    {
        string id;
        string siteId;
        EquipmentType equipmentType;
        string name;
        DateTime lastCleaned;
        string connectedComputerId; // only scanners

        public string Id { get { return id; } set { id = value; } }
        public string SiteId { get { return siteId; } set { siteId = value; } }
        public EquipmentType EquipmentType { get { return equipmentType; } set { equipmentType = value; } }
        public string Name { get { return name; } set { name = value; } }
        public DateTime LastCleaned { get { return lastCleaned; } set { lastCleaned = value; } }
        public string ConnectedComputerId { get { return connectedComputerId; } set { connectedComputerId = value; } }
    }
}
