using Library.Interface;
using Library.Plugins.PropertyDefinition;
using System.Collections.Generic;

namespace Library.Class
{
    public class Property : IConvertable
    {
        public Property()
        {
            Parameters = new List<Parameter>();
        }

        public PropertyDefinition Definition { get; set; }
        public List<Parameter> Parameters { get; set; }

        #region IConvertable Implementations

        public string ToJson(bool beautify, int nbTab = 0)
        {
            string depthTab = "";
            for (int i = 0; i < nbTab; i++)
            {
                depthTab += "\t";
            }

            string jsonStr = depthTab + "{\n";
            jsonStr += depthTab + "\t" + "\"_class\":" + "\"" + Definition.ClassName + "\"," + "\n";

            jsonStr += depthTab + "\t" + "\"parameters\":" + "[\n";

            for (int i = 0; i < Parameters.Count; i++)
            {
                jsonStr += Parameters[i].ToJson(true, nbTab + 2);
                jsonStr += (i < Parameters.Count - 1 ? "," : "") + "\n";
            }

            jsonStr += depthTab + "\t]" + "\n";

            jsonStr += depthTab+"}";

            return jsonStr;
        }

        #endregion
    }
}
