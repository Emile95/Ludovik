using System;

namespace Library.Encodable
{
    [Serializable]
    public class Log
    {
        [Serializable]
        public enum Type
        {
            Info,
            Error
        }

        public Type type;
        public string message;
    }
}
