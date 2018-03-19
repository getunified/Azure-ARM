using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Model
{
    public class HouseUser
    {
        public int HouseUserId { get; set; }
        public int UserId { get; set; }
        public int HouseId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }


        public HouseUser(int houseUserId, int userId, int houseId, string startDate, string endDate, bool isActive, bool isAdmin)
        {
            this.HouseUserId = houseUserId;
            this.UserId = userId;
            this.HouseId = houseId;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.IsActive = isActive;
            this.IsAdmin = isAdmin;
        }
        public struct Result
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public List<HouseUser> HouseUser { get; set; }

            public Result(string status, string message, List<HouseUser> houseUser) : this()
            {
                this.Status = status;
                this.Message = message;
                this.HouseUser = houseUser;
            }

            public Result(string status, string message) : this()
            {
                this.Status = status;
                this.Message = message;
            }
        }
    }
}
