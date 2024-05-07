#### BUILDINGS
# import json
# import xml.etree.ElementTree as ET

# # Load JSON data from file
# with open('buildings.json') as f:
#     data = json.load(f)

# # Create XML structure
# root = ET.Element("ArrayOfBuilding")

# # Map C# enum values
# building_type_mapping = {
#     "is_library": "Library",
#     "is_reshall": "ResidentialHall",
#     "other": "Other"
# }

# campus_group_mapping = {
#     "zw1TFIf7KQxMNrThdfD1": "G1",
#     "kxeYimfnOx1YnB9TVXp9": "G2",
#     "CgLht1pwcSdyDe7tJWVN": "G3",
#     "gkRTxs7OyARmxGHHPuMV": "R1",
#     "LM0MN0spXlHfd2oZSahO": "R2"
# }

# # Loop through each building
# for building_id, building_info in data["buildings"].items():
#     building_element = ET.SubElement(root, "Building")

#     # Extract building attributes
#     building_element_id = ET.SubElement(building_element, "Id")
#     building_element_id.text = building_info["id"]

#     building_name = ET.SubElement(building_element, "Name")
#     building_name.text = building_info["name"]

#     building_address = ET.SubElement(building_element, "Address")
#     address_elements = [
#         ("Street", building_info["address"]["street"]),
#         ("City", building_info["address"]["city"]),
#         ("State", "Missouri"),
#         ("StateCode", building_info["address"]["state"]),
#         ("Country", building_info["address"]["country"]),
#         ("ZipCode", building_info["address"]["zip_code"])
#     ]
#     for tag, text in address_elements:
#         sub_element = ET.SubElement(building_address, tag)
#         sub_element.text = text

#     building_longitude = ET.SubElement(building_element, "Longitude")
#     building_longitude.text = str(building_info["coordinates"]["value"]["_longitude"])

#     building_latitude = ET.SubElement(building_element, "Latitude")
#     building_latitude.text = str(building_info["coordinates"]["value"]["_latitude"])

#     building_group = ET.SubElement(building_element, "Group")
#     building_group.text = campus_group_mapping.get(building_info["site_group"], "Other")

#     building_type = ET.SubElement(building_element, "Type")
#     building_type.text = building_type_mapping.get("is_library" if building_info["is_library"] else "is_reshall", "Other")

# # Create XML tree
# tree = ET.ElementTree(root)

# # Write XML to file
# tree.write("buildings.xml", encoding="utf-8", xml_declaration=True)





#### COMPUTING_SITES
# import json
# import xml.etree.ElementTree as ET

# # Load JSON data from file
# with open('computingSites.json', 'r') as json_file:
#     data = json.load(json_file)

# # Define XML structure
# root = ET.Element("ArrayOfComputingSite")

# # Map JSON types to C# enum values
# type_mapping = {
#     "Classroom": "Classroom",
#     "Other": "Other",
#     "Printer Only": "PrinterOnly",
#     "ResHall": "ResHallLab",
#     "Library": "Library"
# }

# # Function to convert JSON data to XML
# def convert_to_xml(site_data):
#     site = ET.SubElement(root, "ComputingSite")
#     ET.SubElement(site, "Id").text = site_data["id"]
#     ET.SubElement(site, "Building").text = site_data["building"]
#     ET.SubElement(site, "Name").text = site_data["name"]
#     ET.SubElement(site, "Type").text = type_mapping.get(site_data["site_type"], "Other")
#     ET.SubElement(site, "ChairCount").text = str(site_data.get("chair_count", 0))
#     ET.SubElement(site, "HasClock").text = str(site_data.get("has_clock", False)).lower()
#     ET.SubElement(site, "HasWhiteboard").text = str(site_data.get("has_whiteboard", False)).lower()
#     ET.SubElement(site, "HasBlackboard").text = "false"  # Not provided in JSON data, defaulting to false
#     ET.SubElement(site, "HasPosterBoard").text = str(site_data.get("has_poster_board", False)).lower()
#     ET.SubElement(site, "HasInventory").text = str(site_data.get("has_inventory", False)).lower()
#     ET.SubElement(site, "NearestInventoryId").text = site_data.get("nearest_inventory", "0")
#     ET.SubElement(site, "SiteCaptainUserId").text = site_data.get("site_captain", "0")

# # Convert each site in JSON data to XML
# for site_id, site_data in data["computing_sites"].items():
#     convert_to_xml(site_data)

# # Write XML to file
# tree = ET.ElementTree(root)
# tree.write("computing_sites.xml", encoding="utf-8", xml_declaration=True)




#### INVENTORY_SITES
# import json
# import xml.etree.ElementTree as ET

# # Load JSON data from file
# with open('inventorySites.json') as f:
#     data = json.load(f)

# # Create XML structure
# root = ET.Element("ArrayOfInventorySite")

# # Map inventory type IDs to names
# inventory_type_mapping = {
#     "GbQCioDkni88xwPaY1lo": "CornellKitchenette",
#     "TNkr3dS4rBnWTn5glEw0": "Closet",
#     "XzRJMtaGOtUKCg5WJGU0": "Tall Cabinet",
#     "fRM2GZq5XgvWRYiqIv81": "Short Cabinet"
# }

# # Loop through each inventory site
# for site_id, site_info in data["inventory_sites"].items():
#     site_element = ET.SubElement(root, "InventorySite")

#     # Extract site attributes
#     site_id_element = ET.SubElement(site_element, "Id")
#     site_id_element.text = site_info["id"]

#     building_element = ET.SubElement(site_element, "Building")
#     building_element.text = site_info["building"]

#     name_element = ET.SubElement(site_element, "Name")
#     name_element.text = site_info["name"]

#     types_element = ET.SubElement(site_element, "Types")
#     for inventory_type_id in site_info.get("inventory_types", []):
#         type_element = ET.SubElement(types_element, "InventorySiteType")
#         type_element.text = inventory_type_mapping.get(inventory_type_id, "Other")

# # Create XML tree
# tree = ET.ElementTree(root)

# # Write XML to file
# tree.write("inventory_sites.xml", encoding="utf-8", xml_declaration=True)



#### EQUIPMENT ITEMS
# ##PRINTERS
# import json
# import xml.etree.ElementTree as ET
# from datetime import datetime

# # Load JSON data from file
# with open('printers.json') as f:
#     data = json.load(f)

# # Create XML structure
# root = ET.Element("ArrayOfEquipmentItem")

# # Function to determine equipment type
# def get_equipment_type(type_str):
#     if type_str == "B&W":
#         return "blackWhitePrinter"
#     elif type_str == "Color":
#         return "colorPrinter"
#     else:
#         return None

# # Loop through each printer
# for printer_id, printer_info in data["printers"].items():
#     printer_element = ET.SubElement(root, "EquipmentItem")

#     # Extract printer attributes
#     printer_id_element = ET.SubElement(printer_element, "Id")
#     printer_id_element.text = printer_info["id"]

#     # Assuming computing_site is equivalent to siteId
#     site_id_element = ET.SubElement(printer_element, "SiteId")
#     site_id_element.text = printer_info.get("computing_site", "")

#     equipment_type = get_equipment_type(printer_info.get("type", ""))
#     if equipment_type:
#         equipment_type_element = ET.SubElement(printer_element, "EquipmentType")
#         equipment_type_element.text = equipment_type

#     name_element = ET.SubElement(printer_element, "Name")
#     name_element.text = printer_info.get("name", "")

#     # Assuming lastCleaned is the current date and time
#     last_cleaned_element = ET.SubElement(printer_element, "LastCleaned")
#     last_cleaned_element.text = datetime.now().isoformat()

#     # ConnectedComputerId is only for scanners, set to 0 for printers
#     connected_computer_id_element = ET.SubElement(printer_element, "ConnectedComputerId")
#     connected_computer_id_element.text = "0"

# # Create XML tree
# tree = ET.ElementTree(root)

# # Write XML to file
# tree.write("printers.xml", encoding="utf-8", xml_declaration=True)

##COMPUTERS
# import json
# import xml.etree.ElementTree as ET
# from datetime import datetime

# # Load JSON data from file
# with open('computers.json') as f:
#     data = json.load(f)

# # Create XML structure
# root = ET.Element("ArrayOfEquipmentItem")

# # Function to determine equipment type based on OS
# def get_equipment_type(os):
#     if os == "Windows":
#         return "windowsComputer"
#     elif os == "macOS":
#         return "macComputer"
#     else:
#         return None

# # Loop through each computer
# for computer_id, computer_info in data["computers"].items():
#     computer_element = ET.SubElement(root, "EquipmentItem")

#     # Extract computer attributes
#     computer_id_element = ET.SubElement(computer_element, "Id")
#     computer_id_element.text = computer_info["id"]

#     # Assuming computing_site is equivalent to siteId
#     site_id_element = ET.SubElement(computer_element, "SiteId")
#     site_id_element.text = computer_info.get("computing_site", "")

#     equipment_type = get_equipment_type(computer_info.get("OS", ""))
#     if equipment_type:
#         equipment_type_element = ET.SubElement(computer_element, "EquipmentType")
#         equipment_type_element.text = equipment_type

#     name_element = ET.SubElement(computer_element, "Name")
#     name_element.text = computer_info.get("computer_name", "")

#     # Assuming last_cleaned is the current date and time
#     last_cleaned_element = ET.SubElement(computer_element, "LastCleaned")
#     last_cleaned_element.text = datetime.now().isoformat()

#     # ConnectedComputerId is not applicable for computers, set to 0
#     connected_computer_id_element = ET.SubElement(computer_element, "ConnectedComputerId")
#     connected_computer_id_element.text = "0"

# # Create XML tree
# tree = ET.ElementTree(root)

# # Write XML to file
# tree.write("computers.xml", encoding="utf-8", xml_declaration=True)




#### SUPPLY COUNTS
# import json
# import xml.etree.ElementTree as ET
# from enum import Enum

# # Define SupplyType enum
# class SupplyType(Enum):
#     BlackWhitePaper = 0
#     ColorPaper = 1
#     TabloidBlackWhitePaper = 2
#     TabloidColorPaper = 3
#     TableSpray = 4
#     Wipes = 5
#     PaperTowel = 6
#     SprayCan = 7
#     Stapler = 8
#     StaplerRefill = 9
#     TapeDispenser = 10
#     TapeRefill = 11
#     Scissors = 12
#     DryEraseMarker = 13
#     DryEraseEraser = 14
#     Clock = 15

# # Define SupplyCount class
# class SupplyCount:
#     def __init__(self, site_id, supply_type, count, level):
#         self.SiteId = site_id
#         self.Type = SupplyType[supply_type]
#         self.Count = count
#         self.Level = level

# # Read JSON data from file
# with open("supplyCounts.json", "r") as file:
#     data = json.load(file)

# # Create XML structure
# root = ET.Element("ArrayOfSupplyCount")
# for supply_id, supply_data in data["supplies"].items():
#     supply_type = supply_data["supply_type"]
#     supply_count = supply_data["count"]
#     supply_level = supply_data["level"]
#     supply = SupplyCount(site_id=supply_data["inventory_site"],
#                          supply_type=supply_type,
#                          count=supply_count,
#                          level=supply_level)
#     supply_element = ET.SubElement(root, "SupplyCount")
#     site_id_element = ET.SubElement(supply_element, "SiteId")
#     site_id_element.text = str(supply.SiteId)
#     type_element = ET.SubElement(supply_element, "Type")
#     type_element.text = str(supply.Type)
#     count_element = ET.SubElement(supply_element, "Count")
#     count_element.text = str(supply.Count)
#     level_element = ET.SubElement(supply_element, "Level")
#     level_element.text = str(supply.Level)

# # Write XML to file
# tree = ET.ElementTree(root)
# tree.write("supplyCounts.xml", encoding="utf-8", xml_declaration=True)

import json
import xml.etree.ElementTree as ET

supply_type_mapping = {
    "SWHMBwzJaR3EggqgWNEk": "SprayCan",
    "dpj0LV4bBdw8wRVle7aD": "BlackWhitePaper",
    "rGTzAyr1CXN2NV0sapK1": "ColorPaper",
    "B17QKJXEM3oPLaoreQWn": "TabloidBlackWhitePaper",
    "5dbQL6Jmc3ezlsqR75Pu": "TabloidColorPaper",
    "yOPDkKB4wVEB1dTK9fXy": "PaperTowel",
    "pzYHibgLjJ6yjh8T9Jno": "TableSpray",
    "w4V5uYVeF48AvfcgAFN1": "Wipes"
}

# Read JSON data
with open('supplyCounts.json', 'r') as json_file:
    data = json.load(json_file)

# Create XML structure
root = ET.Element("ArrayOfSupplyCount")

# Iterate through supplies
for key, supply in data["supplies"].items():
    supply_count = ET.SubElement(root, "SupplyCount")
    site_id = ET.SubElement(supply_count, "SiteId")
    supply_type = ET.SubElement(supply_count, "Type")
    count = ET.SubElement(supply_count, "Count")
    level = ET.SubElement(supply_count, "Level")

    site_id.text = supply["inventory_site"]
    supply_type.text = supply_type_mapping.get(supply["supply_type"], "Unknown")
    count.text = str(supply["count"])
    level.text = str(supply.get("level", 0))

# Create XML file
tree = ET.ElementTree(root)
tree.write("supplyCounts.xml", encoding='utf-8', xml_declaration=True)
