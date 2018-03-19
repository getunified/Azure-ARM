using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestAPI.Model;

namespace RestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/HouseUser")]
    public class HouseUserController : Controller
    {
        [HttpPost("getHouseUser")]
        public string GetHouseUser(string uid)
        {
            List<HouseUser> result = QueryHouseUser(Convert.ToInt32(uid));
            if (result.Count>0)
            {
                return JsonConvert.SerializeObject(new HouseUser.Result("1", "Query Success", (List<HouseUser>)result));
            }
            return JsonConvert.SerializeObject(new HouseUser.Result("0", "Query Fail"));
        }

        [HttpPost("getHouseUserJson")]
        public String GetHouseUserJSON([FromBody]JObject data)
        {
            Debug.WriteLine("QueryUserHouseJson");
            //return "username: "+data["username"].ToString() + "  password: "+ data["password"].ToString();
            List<HouseUser> result = QueryHouseUser(Convert.ToInt32(data["uid"].ToString()));
            if (result.Count > 0)
            {
                return JsonConvert.SerializeObject(new HouseUser.Result("1", "Query Success", (List<HouseUser>)result));
            }
            return JsonConvert.SerializeObject(new HouseUser.Result("0", "Query Fail"));
        }

        private List<HouseUser> QueryHouseUser(int userId)
        {
            List<HouseUser> houseUser = new List<HouseUser>();
            try
            {
                SqlConnector cntr = new SqlConnector();

                using (SqlConnection connection = cntr.CloudConnect())
                {
                    Debug.WriteLine("\nQuery data example:");
                    Debug.WriteLine("=========================================\n");

                    connection.Open();
                    Debug.WriteLine("User ID is: " + userId);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT *");
                    sb.Append("FROM HouseUserTable ");
                    sb.Append($"where \"UserID\"={userId};");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Debug.WriteLine("{0} {1} {2} {3} {4} {5} {6}", reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetBoolean(5), reader.GetBoolean(6));
                                houseUser.Add(new HouseUser(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetBoolean(5), reader.GetBoolean(6)));
                            }
                            return houseUser;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return houseUser;
        }
    }
}