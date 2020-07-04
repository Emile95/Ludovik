using Library.Encodable;
using Library.Interface;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library.Class.Node
{
    public class Node : IConvertable, IRepository
    {
        #region Properties and Constructor

        public string Name { get; set; }
        public string Host { get; set; }
        public string WorkSpace { get; set; }

        private Socket _clientSocket;

        public Node(string host)
        {
            Host = host;
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(IPAddress.Parse(Host), 100);
        }

        #endregion

        #region Public Methods

        public void ConsoleLog(ConsoleLog consoleLog)
        {
            using (MemoryStream memorystream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(memorystream, consoleLog);
                _clientSocket.Send(memorystream.ToArray());
            }
            

            /*_clientSocket.BeginReceive(received, 0 , received.Length, SocketFlags.None, (result) => { 
                
            }, _clientSocket);*/
        }

        #endregion

        #region IConvertable Implementations

        public string ToJson(bool beautify, int nbTab = 0)
        {
            string depthTab = "";
            for (int i = 0; i < nbTab; i++)
            {
                depthTab += "\t";
            }

            string jsonStr = depthTab + "{\n";
            jsonStr += depthTab + "\t" + "\"host\":" + "\"" + Host + "\"," + "\n";
            jsonStr += depthTab + "\t" + "\"workspace\":" + "\"" + WorkSpace + "\"," + "\n";
            jsonStr += "}";

            return jsonStr;
        }

        #endregion

        #region IRepository Implementations

        public void CreateRepository(string path)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IRepository Implementations

        public void Close()
        {
            _clientSocket.Close();
        }

        #endregion
    }
}
