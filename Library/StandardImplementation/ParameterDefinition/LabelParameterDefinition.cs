using Library.Plugins.ParameterDefinition;
using System.IO;

namespace Library.StandardImplementation.LabelParameterDefinition
{
    public class LabelParameterDefinition : ParameterDefinition
    {
        public LabelParameterDefinition(string name, string description)
        {
            _className = "LabelParameter";
        }

        public sealed override bool VerifyValue(string value)
        {
            if(!Directory.Exists("nodes\\" + value))
            {
                return false;
            }
            return true;
        }
    }
}
