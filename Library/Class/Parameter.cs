﻿using Library.Interface;
using Library.Plugins.ParameterDefinition;

namespace Library.Class
{
    public class Parameter : IConvertable
    {
        #region Properties 

        public ParameterDefinition Definition { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        #endregion

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
            jsonStr += depthTab + "\t" + "\"name\":" + "\"" + Name + "\"," + "\n";
            jsonStr += depthTab + "\t" + "\"value\":" + "\"" + Value + "\"" + "\n";

            jsonStr += depthTab+"}";

            return jsonStr;
        }

        #endregion
    }
}
