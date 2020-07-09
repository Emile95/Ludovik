using Library.Encodable;
using Library.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;

namespace Library.Class.Node
{
    public class Node : IConvertable, IRepository
    {
        #region Properties and Constructor

        public string Name { get; set; }
        public string Host { get; set; }
        public string WorkSpace { get; set; }
        public int NbConcurrence { get; set; }

        private Socket _clientSocket;
        private List<byte[]> _buffers;

        public Node(string host, int port)
        {
            Host = host;
            WorkSpace = "C:\\LudovikWorkspace";
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(IPAddress.Parse(Host), port);

            _buffers = new List<byte[]>();
        }

        #endregion

        #region Private Methods

        private void BeingReceive()
        {
            byte[] buffer = new byte[1024];
            int indexBuffer = _buffers.Count - 1;
            _buffers.Add(buffer);
            _clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, (asyncResult) => {
                ReceiveCallBack(asyncResult, indexBuffer);
            }, _clientSocket);
        }

        private void ReceiveCallBack(IAsyncResult result, int indexBuffer)
        {
            //Nb of Byte of result
            int nbByte = _clientSocket.EndReceive(result);

            object obj = null;

            using (MemoryStream memorystream = new MemoryStream(_buffers[indexBuffer]))
            {
                BinaryFormatter bf = new BinaryFormatter();
                obj = bf.Deserialize(memorystream);
            }

            _buffers.RemoveAt(indexBuffer);
            BeingReceive();
        }

        #endregion

        #region Public Methods

        public void ConsoleLog(ConsoleLog consoleLog)
        {
            _clientSocket.Send(consoleLog.ToBinary());
        }
        
        public void RunProcess(string fileName, string args, Dictionary<string,string> environments, string workingDir = "")
        {
            ServerInstance<ProcessRunInfo> instance = new ServerInstance<ProcessRunInfo>();
            instance.obj = new ProcessRunInfo()
            {
                fileName = fileName,
                args = args,
                workingDirectory = WorkSpace + "\\" + workingDir
            };

            foreach (KeyValuePair<string, string> prop in environments)
                instance.obj.vars += prop.Key.ToUpper() + ":" + prop.Value + ",";

            instance.obj.vars = instance.obj.vars.Substring(0, instance.obj.vars.Length - 1);

            //BeingReceive();

            _clientSocket.Send(instance.ToBinary());
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
