using Library.Interface;
using System.Collections.Generic;

namespace Library.Plugins
{
    public abstract class PropertyDefinition : IConvertable
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

        public abstract bool VerifyIntegrity(string[] values);

        #region IConvertable

        public string ToJson(bool beautify, int nbTab = 0)
        {
            string depthTab = "";
            for (int i = 0; i < nbTab; i++)
            {
                depthTab += "\t";
            }

            string jsonStr = depthTab + "{\n";
            jsonStr += depthTab + "\t" + "\"_class\":" + "\"" + ClassName + "\"," + "\n";
            jsonStr += depthTab + "}";
            return jsonStr;
        }

        #endregion
    }
}
