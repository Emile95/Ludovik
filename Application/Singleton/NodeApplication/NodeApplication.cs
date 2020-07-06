using System.Collections.Generic;
using Application.Singleton.NodeApplication.SendedModel;
using Library.Class.Node;
using Library.Encodable;

namespace Application.NodeApplication
{
    public class NodeApplication
    {
        #region Properties and Constructor

        private readonly Dictionary<string,Node> _connectedNodes;

        public NodeApplication()
        {
            _connectedNodes = new Dictionary<string, Node>();
        }

        #endregion

        #region Public Methods

        public void ConnectNode(NodeConnectionModel model) 
        {
            Node node = new Node("127.0.0.1", 300);
            _connectedNodes.Add(model.Name, node);
           
            node.ConsoleLog(new ConsoleLog()
            {
                log = "Server connteced with " + model.Name + " has node name"
            });
        }

        public void DisconnectNode(NodeDisconnectionModel model)
        {
            _connectedNodes[model.Name].Close();
            _connectedNodes.Remove(model.Name);
        }

        public Node GetNode(string name)
        {
            return _connectedNodes[name];
        }

        #endregion
    }
}
