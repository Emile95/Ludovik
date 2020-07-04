using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library.Class
{
    public class Node : IDisposable
    {
        public string IpAddress { get; set; }
        public string WorkSpace { get; set; }
        private Socket _clientSocket;

        public Node(string ipAddress, string workspace)
        {
            IpAddress = ipAddress;
            WorkSpace = workspace;

            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(IPAddress.Parse(IpAddress), 100);
        }

        public void Execute(Process process)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                //bf.Serialize(ms, process);
                _clientSocket.Send(new byte[] { 1, 2 });
            }

            byte[] received = new byte[1024];

            _clientSocket.BeginReceive(received, 0 , received.Length, SocketFlags.None, (result) => { 
                
            }, _clientSocket);
        }

        public void Dispose()
        {
            _clientSocket.Close();
        }
    }
}
