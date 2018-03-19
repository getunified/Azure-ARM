using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Model
{
    public class User
    {
        public int Uid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Error { get; set; }


        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.Error = "";
        }

        public User(int uid, string username, string password, string email, string phone)
        {
            this.Uid = uid;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Phone = phone;
            this.Error = "";
        }

        public User()
        {
        }

        public struct Result
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public User User { get; set; }

            public Result(string statu, string message, User user) : this()
            {
                this.Status = statu;
                this.Message = message;
                this.User = user;
            }

            public Result(string statu, string message) : this()
            {
                this.Status = statu;
                this.Message = message;
            }
        }
    }
}
