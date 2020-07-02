using System.Collections.Generic;

namespace Library.Class
{
    public class Environment
    {
        #region Properties and Constructor

        public Dictionary<string, string> Properties { get; private set; }

        public Environment()
        {
            Properties = new Dictionary<string, string>();
        }

        #endregion
    }
}
