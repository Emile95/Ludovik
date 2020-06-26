using Library.Plugins.ParameterDefinition;
using System.IO;

namespace Library.StandardImplementation.LabelParameterDefinition
{
    public class LabelParameterDefinition : ParameterDefinition
    {
        public sealed override bool VerifyValue(string value)
        {
            return Directory.Exists("nodes\\" + value);
        }
    }
}
