using Library.Plugins.Logger;
using Library.Plugins.ParameterDefinition;

namespace Library.StandardImplementation.StringParameterDefinition
{
    public class StringParameterDefinition : ParameterDefinition
    {
        public StringParameterDefinition(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public sealed override bool VerifyValue(string value)
        {
            return true;
        }
    }
}
