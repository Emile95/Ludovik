﻿using Library.Encodable;
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
        private byte[] _buffer;

        public Node(string host, int port)
        {
            Host = host;
            WorkSpace = "C:\\LudovikWorkspace";
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(IPAddress.Parse(Host), port);

            _buffer = new byte[1024];
            _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallBack, _clientSocket);
        }

        #endregion

        #region Private Methods

        private void ReceiveCallBack(IAsyncResult result)
        {
            //Nb of Byte of result
            int nbByte = _clientSocket.EndReceive(result);
            //Result Data
            byte[] buffer = new byte[nbByte];
            Array.Copy(
                _buffer,
                buffer,
                nbByte
            );

            _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallBack, _clientSocket);
        }

        private void SendData<T>(T data)
        {
            using (MemoryStream memorystream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(memorystream, data);
                _clientSocket.Send(memorystream.ToArray());
            }
        }

        #endregion

        #region Public Methods

        public void ConsoleLog(ConsoleLog consoleLog)
        {
            SendData(consoleLog);
        }

        public void RunProcess(ProcessRunInfo processRunInfo)
        {
            processRunInfo.workingDirectory = WorkSpace + "\\" + processRunInfo.workingDirectory;
            SendData(processRunInfo);
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
