using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Model
{
    public class Node
    {
        public int NodeId { get; set; }
        public int HouseId { get; set; }
        public int NodeType { get; set; }

        public Node(int nodeId, int houseId, int nodeType)
        {
            this.NodeId = nodeId;
            this.HouseId = houseId;
            this.NodeType = nodeType;
        }

        public struct Result
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public List<Node> Node { get; set; }

            public Result(string status, string message, List<Node> node) : this()
            {
                this.Status = status;
                this.Message = message;
                this.Node = node;
            }

            public Result(string status, string message) : this()
            {
                this.Status = status;
                this.Message = message;
            }
        }
    }
}
