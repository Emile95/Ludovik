using Library.Plugins.ParameterDefinition;

namespace Library.StandardImplementation.StringParameterDefinition
{
    public class StringParameterDefinition : ParameterDefinition
    {
        public StringParameterDefinition()
        {
            _className = "StringParameter";
        }

        public sealed override bool VerifyValue(string value)
        {
            return true;
        }
    }
}
