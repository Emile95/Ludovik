using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library.Encodable
{
    [Serializable]
    public class ConsoleLog
    {
        public string log;

        public byte[] ToBinary()
        {
            using (MemoryStream memorystream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(memorystream, this);
                return memorystream.ToArray();
            }
        }
    }
}
