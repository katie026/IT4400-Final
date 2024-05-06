using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    [Serializable]
    public enum BuildingType
    {
        Library,
        ResidentialHall,
        Other
    }

    [Serializable]
    public enum CampusGroup
    {
        All,
        G1,
        G2,
        G3,
        R1,
        R2
    }

    [Serializable]
    public class Building
    {
        string id;
        String name;
        Address address;
        float longitude;
        float latitude;
        CampusGroup group;
        BuildingType type;

        public string Id { get { return id; } set { id = value; } }
        public String Name { get { return name; } set { name = value; } }
        public Address Address { get { return address; } set { address = value; } }
        public float Longitude { get { return longitude; } set { longitude = value; } }
        public float Latitude { get { return latitude; } set { latitude = value; } }
        public CampusGroup Group { get { return group; } set { group = value;} }
        public BuildingType Type { get { return type; } set { type = value; } }
        public String Coordinates {
            get
            {
                return $"{longitude.ToString()}, {latitude.ToString()}";
            }
        }
    }
}
