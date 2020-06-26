using Library.Plugins.Logger;
using System;
using System.Collections.Generic;

namespace Library.Class
{
    public class LoggerList
    {
        private readonly List<Logger> _loggers;

        public LoggerList()
        {
            _loggers = new List<Logger>();
        }

        public void AddLogger(Logger logger)
        {
            Type type = logger.GetType();
            foreach (Logger item in _loggers)
            {
                if (item.GetType() == type)
                    throw new System.Exception("error");
            }
            _loggers.Add(logger);
        }

        public Logger GetLogger<T>()
        {
            foreach(Logger logger in _loggers)
            {
                if (logger is T)
                    return logger;
            }
            return null;
        }
    }
}
