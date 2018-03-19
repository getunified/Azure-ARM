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
    [Route("api/House")]
    public class HouseController : Controller
    {
        [HttpPost("getHouse")]
        public string GetHouse(string address)
        {
            House result = QueryGetHouse(address);
            if (result.Error == "")
            {
                return JsonConvert.SerializeObject(new House.Result("1", "Success", result));
            }
            return JsonConvert.SerializeObject(new House.Result("0", result.Error));
        }

        [HttpPost("getHouseJson")]
        public string GetHouseJson([FromBody]JObject data)
        {
            House result = QueryGetHouse(data["address"].ToString());
            if (result.Error == "")
            {
                return JsonConvert.SerializeObject(new House.Result("1", "Success", result));
            }
            return JsonConvert.SerializeObject(new House.Result("0", result.Error));
        }

        [HttpPost("getHouseID")]
        public string GetHouseID(int id)
        {
            Debug.WriteLine("id: {0}", id);
            House result = QueryGetHouseID(id);
            if (result.Error == "")
            {
                return JsonConvert.SerializeObject(new House.Result("1", "Success", result));
            }
            return JsonConvert.SerializeObject(new House.Result("0", result.Error));
        }

        [HttpPost("getHouseIDJson")]
        public string GetHouseIDJson([FromBody]JObject data)
        {
            int id = Convert.ToInt32(data["houseid"].ToString());
            Debug.WriteLine("id: {0}", id);
            House result = QueryGetHouseID(id);
            if (result.Error == "")
            {
                return JsonConvert.SerializeObject(new House.Result("1", "Success", result));
            }
            return JsonConvert.SerializeObject(new House.Result("0", result.Error));
        }

        private House QueryGetHouse(string address)
        {
            string error = "";
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
                    sb.Append("FROM HouseTable ");
                    sb.Append($"where \"HouseAddress\"='{address}';");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Debug.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                                return new House(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                error = e.Message;
            }
            House output = new House("HouseAddress");
            output.Error = error;
            return output;
        }

        private House QueryGetHouseID(int id)
        {
            string error = "";
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
                    sb.Append("FROM HouseTable ");
                    sb.Append($"where \"houseID\"={id};");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Debug.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                                return new House(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                error = e.Message;
            }
            House output = new House("HouseAddress");
            output.Error = error;
            return output;
        }
    }
}