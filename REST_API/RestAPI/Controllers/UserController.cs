using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestAPI.Model;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace RestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        // api/user/userReg?username=test&password=123456
        [HttpPost("userLogin")]
        public string UserLogin(string username, string password)
        {
            Debug.WriteLine("Test: {0}, {1}", username, password);
            
            User result = QueryLogin(username, password);
            if (result.Error == "")
            {
                return JsonConvert.SerializeObject(new User.Result("1", "Login Success", (User)result));
            }
            return JsonConvert.SerializeObject(new User.Result("0", "Login Fail: "+result.Error));
        }

        [HttpPost("userLoginJson")]
        public String UserLoginJSON([FromBody]JObject data)
        {
            //return "username: "+data["username"].ToString() + "  password: "+ data["password"].ToString();
            Debug.WriteLine("{0}, {1}", data["username"].ToString(), data["password"].ToString());
            User result = QueryLogin(data["username"].ToString(), data["password"].ToString());
            if (result.Error == "")
            {
                return JsonConvert.SerializeObject(new User.Result("1", "Login Success", (User)result));
            }
            return JsonConvert.SerializeObject(new User.Result("0", "Login Fail: " + result.Error));
        }

        [HttpPost("userReg")]
        public string UserReg(string username, string password)
        {
            string result = QueryRegister(username, password);
            if ((result+"").Length==0)
            {
                return JsonConvert.SerializeObject(new User.Result("1", "Register Success", new Model.User(username,password)));              
            }
            return JsonConvert.SerializeObject(new User.Result("0", "Register Fail: "+result));
        }

        [HttpPost("userRegJson")]
        public String UserRegJSON([FromBody]JObject data)
        {
            //return "username: "+data["username"].ToString() + "  password: "+ data["password"].ToString();         
            string result = QueryRegister(data["username"].ToString(), data["password"].ToString());
            Debug.WriteLine("Result is  "+result);
            if ((result + "").Length == 0)
            {
                return JsonConvert.SerializeObject(new User.Result("1", "Register Success", new Model.User(data["username"].ToString(), data["password"].ToString())));
            }
            return JsonConvert.SerializeObject(new User.Result("0", "Register Fail: " + result));
        }

        private User QueryLogin(String username, string password)
        {
            try
            {
                SqlConnector cntr = new SqlConnector();

                using (SqlConnection connection = cntr.CloudConnect())
                {
                    Debug.WriteLine("\nQuery data example:");
                    Debug.WriteLine("=========================================\n");

                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT *");
                    sb.Append("FROM UserTable ");
                    sb.Append($"where \"Username\"='{username}' AND \"Password\"='{password}';");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Debug.WriteLine("{0} {1} {2} {3} {4}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                                return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
                User user1 = new User();
                user1.Error = e.Message;
                return user1;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                User user1 = new User();
                user1.Error = e.Message;
                return user1;
            }
            Debug.WriteLine("BROKEN");
            User user = new User();
            user.Error = "Broken";
            return user;

        }

        private String QueryRegister(String username, string password)
        {
            try
            {
                SqlConnector cntr = new SqlConnector();

                using (SqlConnection connection = cntr.CloudConnect())
                {
                    Debug.WriteLine("\nQuery data example:");
                    Debug.WriteLine("=========================================\n");

                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Insert into UserTable (username, password, email, phone) ");
                    sb.Append($"VALUES ('{username}', '{password}', '', '')");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //while (reader.Read())
                            //{
                            //    Debug.WriteLine(reader.ToString());
                            //    Debug.WriteLine("{0} {1} {2} {3} {4}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                            //    return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                            //}
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
                return e.Message;
            }

            return null;
        }
    }
}