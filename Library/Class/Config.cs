using Library.Plugins.Logger;
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

        public bool ValidateParams(Logger logger = null)
        {
            foreach(System.Tuple<ParameterDefinition, string> param in _params )
            {
                if (!param.Item1.VerifyValue(param.Item2, logger))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
