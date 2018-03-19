using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Model
{
    public class House
    {
        public int HouseID { get; set; }
        public string MacAddress { get; set; }
        public string HouseAddress { get; set; }
        public string Error { get; set; }

        public House(string houseAddress)
        {
            this.HouseAddress = houseAddress;
        }

        public House(int houseID, string macAddress, string houseAddress)
        {
            this.HouseID = houseID;
            this.MacAddress = macAddress;
            this.HouseAddress = houseAddress;
        }

        public struct Result
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public House House { get; set; }

            public Result(string status, string message, House house) : this()
            {
                this.Status = status;
                this.Message = message;
                this.House = house;
            }

            public Result(string status, string message) : this()
            {
                this.Status = status;
                this.Message = message;
            }
        }
    }
}
