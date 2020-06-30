using Library.Plugins.ParameterDefinition;

namespace Library.StandardImplementation.BoolParameterDefinition
{
    public class BoolParameterDefinition : ParameterDefinition
    {
        public BoolParameterDefinition()
        {
            _className = "BoolParameter";
        }

        public sealed override bool VerifyValue(string value)
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
