using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    [Serializable]
    public enum InventorySiteType
    {
        Closet,
        CornellKitchenette,
        CabinetFR359,
        CabinetC415A
    }

    [Serializable]
    public class InventorySite: Site
    {
        InventorySiteType[] types;

        public InventorySiteType[] Types { get { return types; } set { types = value; } }
    }
}
