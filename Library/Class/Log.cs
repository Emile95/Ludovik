namespace Library.Class
{
    public class Log
    {
        public enum Type
        {
            Info,
            Error,
            Success,
            Warning
        }

        public Type LogType { get; private set; }

        public string Message { get; private set; }

        public Log(string message, Type logType = Type.Info)
        {
            Message = message;
            LogType = logType;
        }
    }
}
