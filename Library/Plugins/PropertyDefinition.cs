using System.Collections.Generic;

namespace Library.Plugins.PropertyDefinition
{
    public abstract class PropertyDefinition
    {
        #region Properties and Constructor

        public string ClassName { get; set; }

        public List<ParameterDefinition.ParameterDefinition> ParamDefs { get; protected set; }

        public PropertyDefinition()
        {
            ParamDefs = new List<ParameterDefinition.ParameterDefinition>();
        }

        #endregion

        #region Abstract Methods



        #endregion

    }
}
