using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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
    public class EquipmentItem : INotifyPropertyChanged
    {
        private string id;
        private string siteId;
        private EquipmentType equipmentType;
        private string name;
        private DateTime lastCleaned;
        private string connectedComputerId; // only for scanners

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string SiteId
        {
            get => siteId;
            set
            {
                if (siteId != value)
                {
                    siteId = value;
                    OnPropertyChanged(nameof(SiteId));
                }
            }
        }

        public EquipmentType EquipmentType
        {
            get => equipmentType;
            set
            {
                if (equipmentType != value)
                {
                    equipmentType = value;
                    OnPropertyChanged(nameof(EquipmentType));
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public DateTime LastCleaned
        {
            get => lastCleaned;
            set
            {
                if (lastCleaned != value)
                {
                    lastCleaned = value;
                    OnPropertyChanged(nameof(LastCleaned));
                }
            }
        }

        public string ConnectedComputerId
        {
            get => connectedComputerId;
            set
            {
                if (connectedComputerId != value)
                {
                    connectedComputerId = value;
                    OnPropertyChanged(nameof(ConnectedComputerId));
                }
            }
        }
        //string id;
        //string siteId;
        //EquipmentType equipmentType;
        //string name;
        //DateTime lastCleaned;
        //string connectedComputerId; // only scanners

        //public string Id { get { return id; } set { id = value; } }
        //public string SiteId { get { return siteId; } set { siteId = value; } }
        //public EquipmentType EquipmentType { get { return equipmentType; } set { equipmentType = value; } }
        //public string Name { get { return name; } set { name = value; } }
        //public DateTime LastCleaned {
        //    get => lastCleaned;
        //    set
        //    {
        //        if (lastCleaned != value)
        //        {
        //            lastCleaned = value;
        //            OnPropertyChanged(nameof(LastCleaned));
        //        }
        //    }
        //}
        //public string ConnectedComputerId { get { return connectedComputerId; } set { connectedComputerId = value; } }
    }
}
