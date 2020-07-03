using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library.Class
{
    public class Node
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string WorkSpace { get; set; }

        public void Execute(Process process)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(IPAddress.Loopback, 100);

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                //bf.Serialize(ms, process);
                clientSocket.Send(new byte[] { 1, 2 });
            }

            byte[] received = new byte[1024];

            clientSocket.BeginReceive(received, 0 , received.Length, SocketFlags.None, (result) => { 
                
            }, clientSocket);

            clientSocket.Disconnect(false);
        }
    }
}
