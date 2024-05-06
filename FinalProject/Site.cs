using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    [Serializable]
    public class Site
    {
        string id;
        string buildingId;
        string name;

        public string Id { get { return id; } set { id = value; } }
        public string Building { get { return buildingId; } set { buildingId = value; } }
        public string Name { get { return name; } set { name = value; } }
    }
}
