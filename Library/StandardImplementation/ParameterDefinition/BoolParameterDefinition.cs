using Library.Plugins.Logger;
using Library.Plugins.ParameterDefinition;

namespace Library.StandardImplementation.BoolParameterDefinition
{
    public class BoolParameterDefinition : ParameterDefinition
    {
        public sealed override bool VerifyValue(string value, Logger logger)
        {
            switch(value)
            {
                case "0":
                case "1":
                case "true":
                case "false":
                    return true;
            }
            return false;
        }
    }
}
