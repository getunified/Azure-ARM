using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    [Route("api/Sensor")]
    public class SensorController : Controller
    {
        [HttpPost("getSensor")]
        public string GetSensor(int id)
        {
            List<Sensor> result = QuerySensor(id);
            if (result.Count > 0)
            {
                return JsonConvert.SerializeObject(new Sensor.Result("1", "Success", result));
            }
            return JsonConvert.SerializeObject(new Sensor.Result("0", "No Nodes for house"));
        }
        [HttpPost("getSensorJson")]
        public string GetSensorJson([FromBody]JObject data)
        {
            int id = Convert.ToInt32(data["nodeid"].ToString());
            List<Sensor> result = QuerySensor(id);
            if (result.Count > 0)
            {
                return JsonConvert.SerializeObject(new Sensor.Result("1", "Success", result));
            }
            return JsonConvert.SerializeObject(new Sensor.Result("0", "No Nodes for house"));
        }

        [HttpPost("SetSensorLimits")]
        public HttpResponseMessage SetSensorLimits(string sensorID, string upperLimit, string lowerLimit)
        {
            List<Sensor> output = new List<Sensor>();
            SqlConnector cntr = new SqlConnector();


            using (SqlConnection connection = cntr.CloudConnect())
            {
                SqlCommand cmd = new SqlCommand("UPDATE [SensorTable] SET UpperLimit=@upperLimit,LowerLimit=@lowerLimit WHERE SensorID=@sensorID", connection);
                SqlParameter param = new SqlParameter();

                cmd.Parameters.AddWithValue("@sensorID", Convert.ToInt32(sensorID));
                cmd.Parameters.AddWithValue("@upperLimit", Convert.ToDouble(upperLimit));
                cmd.Parameters.AddWithValue("@lowerLimit", Convert.ToDouble(lowerLimit));
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch (SqlException e)
                {
                    connection.Close();
                    return new HttpResponseMessage(HttpStatusCode.NotModified);
                }
            }
        }

        [HttpPost("SetSensorLimitsJSON")]
        public HttpResponseMessage SetSensorLimitsJSON([FromBody]JObject data)
        {
            List<Sensor> output = new List<Sensor>();
            SqlConnector cntr = new SqlConnector();


            using (SqlConnection connection = cntr.CloudConnect())
            {
                SqlCommand cmd = new SqlCommand("UPDATE [SensorTable] SET UpperLimit=@upperLimit,LowerLimit=@lowerLimit WHERE SensorID=@sensorID", connection);
                SqlParameter param = new SqlParameter();

                cmd.Parameters.AddWithValue("@sensorID", Convert.ToInt32(data["sensorID"].ToString()));
                cmd.Parameters.AddWithValue("@upperLimit", Convert.ToDouble(data["upperLimit"].ToString()));
                cmd.Parameters.AddWithValue("@lowerLimit", Convert.ToDouble(data["lowerLimit"].ToString()));
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch (SqlException e)
                {
                    connection.Close();
                    return new HttpResponseMessage(HttpStatusCode.NotModified);
                }
            }
        }


        [HttpGet("GetSensorValue")]
        public List<Sensor> GetSensorValue(int sensorID)
        {
            List<Sensor> output = new List<Sensor>();
            
            SqlConnector cntr = new SqlConnector();

            using (SqlConnection connection = cntr.CloudConnect())
            {
                SqlParameter param = new SqlParameter();
                try
                {
                        connection.Open();
                        StringBuilder sb = new StringBuilder();
                        using (SqlCommand command = new SqlCommand("SELECT * FROM [SensorTable] WHERE \"SensorID\"=@sensorID; ", connection))
                        {
                            command.Parameters.AddWithValue("@sensorID", sensorID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                Random randomNumber = new Random();
                                double sensorValue = 25.0 + (randomNumber.NextDouble()/10);
                                //Debug.WriteLine("{0} {1} {2} {3} {4} {5}", reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetDouble(5));
                                output.Add(new Sensor(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetDouble(3), reader.GetDouble(4), sensorValue));

                                //Debug.WriteLine(output.ToString());
                            }
                            }
                        }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    var error = e.Message;
                }
                return output;
            }
        }




        private List<Sensor> QuerySensor(int id)
        {
            string error = "";
            List<Sensor> output = new List<Sensor>();
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
                    sb.Append("FROM SensorTable ");
                    sb.Append($"where \"NodeID\"={id};");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Debug.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                                output.Add(new Sensor(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetDouble(5)));
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
            return output;
        }
    }
}