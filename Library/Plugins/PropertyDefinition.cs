using System.Collections.Generic;

namespace Library.Plugins.PropertyDefinition
{
    public abstract class PropertyDefinition
    {
        public string ClassName { get; set; }

        public List<ParameterDefinition.ParameterDefinition> ParamDefs { get; protected set; }

        public PropertyDefinition()
        {
            ParamDefs = new List<ParameterDefinition.ParameterDefinition>();
        }

        public void AddParameterDefinition(ParameterDefinition.ParameterDefinition ParamDef)
        {
            ParamDefs.Add(ParamDef);
        }
    }
}
