using Library.Plugins.Logger;
using Library.Plugins.ParameterDefinition;
using System.IO;

namespace Library.StandardImplementation.LabelParameterDefinition
{
    public class LabelParameterDefinition : ParameterDefinition
    {
        public LabelParameterDefinition(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public sealed override bool VerifyValue(string value, Logger logger)
        {
            if(!Directory.Exists("nodes\\" + value))
            {
                logger?.Log("The Label " + value + "do not exist");
                return false;
            }
            return true;
        }
    }
}
