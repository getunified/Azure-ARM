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
    [Route("api/Node")]
    public class NodeController : Controller
    {
        [HttpPost("getNode")]
        public string GetNode(int id)
        {
            List<Node> result = QueryNode(id);
            if (result.Count >0)
            {
                return JsonConvert.SerializeObject(new Node.Result("1", "Success", result));
            }
            return JsonConvert.SerializeObject(new Node.Result("0", "No Nodes for house"));
        }
        [HttpPost("getNodeJson")]
        public string GetNodeJson([FromBody]JObject data)
        {
            int id = Convert.ToInt32(data["houseid"].ToString());
            List<Node> result = QueryNode(id);
            if (result.Count > 0)
            {
                return JsonConvert.SerializeObject(new Node.Result("1", "Success", result));
            }
            return JsonConvert.SerializeObject(new Node.Result("0", "No Nodes for house"));
        }

        private List<Node> QueryNode(int id)
        {
            string error = "";
            List<Node> output = new List<Node>();
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
                    sb.Append("FROM NodeTable ");
                    sb.Append($"where \"houseID\"={id};");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Debug.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                                output.Add(new Node(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
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