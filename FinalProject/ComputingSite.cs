using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    [Serializable]
    public enum ComputingSiteType
    {
        Classroom,
        PrinterOnly,
        ResHallLab,
        Library,
        Other
    }

    [Serializable]
    public class ComputingSite : Site
    {
        ComputingSiteType type;
        int chairCount = 0;
        bool hasClock = false;
        bool hasWhiteboard = false;
        bool hasBlackboard = false;
        bool hasPosterboard = false;
        bool hasInventory = false;
        string nearestInventoryId;
        string siteCaptainUserId;

        public ComputingSiteType Type { get { return type; } set { type = value; } }
        public int ChairCount { get { return chairCount; } set { chairCount = value; } }
        public bool HasClock { get { return hasClock; } set { hasClock = value; } }
        public bool HasWhiteboard { get { return hasWhiteboard; } set { hasWhiteboard = value; } }
        public bool HasBlackboard { get { return hasBlackboard; } set { hasBlackboard = value; } }
        public bool HasPosterBoard { get { return hasPosterboard; } set { hasPosterboard = value; } }
        public bool HasInventory { get { return hasInventory; } set { hasInventory = value; } }
        public string NearestInventoryId { get { return nearestInventoryId; } set { nearestInventoryId = value; } }
        public string SiteCaptainUserId { get { return siteCaptainUserId; } set { siteCaptainUserId = value; } }
    }
}
