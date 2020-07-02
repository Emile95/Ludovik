using Library.Plugins.ParameterDefinition;
using System.IO;

namespace Library.StandardImplementation.LabelParameterDefinition
{
    public class LabelParameterDefinition : ParameterDefinition
    {
        public LabelParameterDefinition()
        {
            ClassName = "LabelParameterDefinition";
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
