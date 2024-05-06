using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    [Serializable]
    public class Address
    {
        String street;
        String city;
        String state;
        String stateCode;
        String country;
        String zipCode;

        public string Street { get { return street; } set { street = value; } }
        public string City { get { return city; } set { city = value; } }
        public string State { get { return state; } set { state = value; } }
        public string StateCode { get { return stateCode; } set { stateCode = value; } }
        public string Country { get { return country; } set { country = value; } }
        public string ZipCode { get { return zipCode; } set { zipCode = value; } }

        public override string ToString()
        {
            return $"{Street}, {City}, {StateCode} {ZipCode}, {Country}";
        }
    }
}
