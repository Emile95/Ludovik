using Library.Plugins.ParameterDefinition;
using System.Collections.Generic;

namespace Library.Class
{
    public class Config
    {
        private List<System.Tuple<ParameterDefinition, string>> _params;

        public Config()
        {
            _params = new List<System.Tuple<ParameterDefinition, string>>();
        }

        public void AddParameter(string value, ParameterDefinition paramDef)
        {
            _params.Add(new System.Tuple<ParameterDefinition, string>(
                paramDef,
                value
            ));
        }
    }
}
