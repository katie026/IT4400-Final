using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // add this
using System.Xml.Serialization; // add this


namespace FinalProject
{
    internal class DataSerializer
    {
        public void SerializeData(string filePath, List<Building> buildings, List<ComputingSite> computingSites, List<InventorySite> inventorySites, List<EquipmentItem> equipmentItems, List<SupplyCount> supplyCounts)
        {
            XmlSerializer serializer;

            // Serialize buildings
            serializer = new XmlSerializer(typeof(List<Building>), new Type[] { typeof(Address), typeof(BuildingType), typeof(CampusGroup) });
            using (TextWriter writer = new StreamWriter(Path.Combine(filePath, "buildings.xml")))
            {
                serializer.Serialize(writer, buildings);
            }

            // Serialize computing sites
            serializer = new XmlSerializer(typeof(List<ComputingSite>), new Type[] { typeof(ComputingSiteType) });
            using (TextWriter writer = new StreamWriter(Path.Combine(filePath, "computingSites.xml")))
            {
                serializer.Serialize(writer, computingSites);
            }

            // Serialize inventory sites
            serializer = new XmlSerializer(typeof(List<InventorySite>), new Type[] { typeof(InventorySiteType) });
            using (TextWriter writer = new StreamWriter(Path.Combine(filePath, "inventorySites.xml")))
            {
                serializer.Serialize(writer, inventorySites);
            }

            // Serialize equipment items
            serializer = new XmlSerializer(typeof(List<EquipmentItem>), new Type[] { typeof(EquipmentType) });
            using (TextWriter writer = new StreamWriter(Path.Combine(filePath, "equipmentItems.xml")))
            {
                serializer.Serialize(writer, equipmentItems);
            }

            // Serialize supply counts
            serializer = new XmlSerializer(typeof(List<SupplyCount>), new Type[] { typeof(SupplyType) });
            using (TextWriter writer = new StreamWriter(Path.Combine(filePath, "supplyCounts.xml")))
            {
                serializer.Serialize(writer, supplyCounts);
            }
        }


        public (List<Building>, List<ComputingSite>, List<InventorySite>, List<EquipmentItem>, List<SupplyCount>) DeserializeData(string filePath)
        {
            List<Building> buildings;
            List<ComputingSite> computingSites;
            List<InventorySite> inventorySites;
            List<EquipmentItem> equipmentItems;
            List<SupplyCount> supplyCounts;

            XmlSerializer serializer;

            // Deserialize buildings
            serializer = new XmlSerializer(typeof(List<Building>), new Type[] { typeof(Address), typeof(BuildingType), typeof(CampusGroup) });
            using (TextReader reader = new StreamReader(Path.Combine(filePath, "buildings.xml")))
            {
                buildings = (List<Building>)serializer.Deserialize(reader);
            }

            // Deserialize computing sites
            serializer = new XmlSerializer(typeof(List<ComputingSite>), new Type[] { typeof(ComputingSiteType) });
            using (TextReader reader = new StreamReader(Path.Combine(filePath, "computingSites.xml")))
            {
                computingSites = (List<ComputingSite>)serializer.Deserialize(reader);
            }

            // Deserialize inventory sites
            serializer = new XmlSerializer(typeof(List<InventorySite>), new Type[] { typeof(InventorySiteType) });
            using (TextReader reader = new StreamReader(Path.Combine(filePath, "inventorySites.xml")))
            {
                inventorySites = (List<InventorySite>)serializer.Deserialize(reader);
            }

            // Deserialize equipment items
            serializer = new XmlSerializer(typeof(List<EquipmentItem>), new Type[] { typeof(EquipmentType) });
            using (TextReader reader = new StreamReader(Path.Combine(filePath, "equipmentItems.xml")))
            {
                equipmentItems = (List<EquipmentItem>)serializer.Deserialize(reader);
            }

            // Deserialize supply counts
            serializer = new XmlSerializer(typeof(List<SupplyCount>), new Type[] { typeof(SupplyType) });
            using (TextReader reader = new StreamReader(Path.Combine(filePath, "supplyCounts.xml")))
            {
                supplyCounts = (List<SupplyCount>)serializer.Deserialize(reader);
            }

            return (buildings, computingSites, inventorySites, equipmentItems, supplyCounts);
        }
    }
}
