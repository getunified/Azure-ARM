using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Model
{
    public class Sensor
    {
        public int SensorID { get; set; }
        public int NodeID { get; set; }
        public int SensorType { get; set; }
        public double UpperLimit { get; set; }
        public double LowerLimit { get; set; }
        public double Value { get; set; }

        public Sensor(int sensorId, int nodeId, int sensorType, double UpperLimit, double LowerLimit, double Value)
        {
            this.SensorID = sensorId;
            this.NodeID = nodeId;
            this.SensorType = sensorType;
            this.UpperLimit = UpperLimit;
            this.LowerLimit = LowerLimit;
            this.Value = Value;
        }

        public struct Result
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public List<Sensor> Sensor { get; set; }

            public Result(string status, string message, List<Sensor> sensor) : this()
            {
                this.Status = status;
                this.Message = message;
                this.Sensor = sensor;
            }

            public Result(string status, string message) : this()
            {
                this.Status = status;
                this.Message = message;
            }
        }
    }
}
