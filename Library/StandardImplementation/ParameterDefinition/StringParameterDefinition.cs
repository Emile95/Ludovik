using Library.Plugins.ParameterDefinition;

namespace Library.StandardImplementation.StringParameterDefinition
{
    public class StringParameterDefinition : ParameterDefinition
    {
        public StringParameterDefinition()
        {
            ClassName = "StringParameterDefinition";
        }

        public sealed override bool VerifyValue(string value)
        {
            return true;
        }
    }
}
