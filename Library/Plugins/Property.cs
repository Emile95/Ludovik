using Library.Interface;

namespace Library.Plugins
{
    public class Property : IConvertable
    {
        public string Name { get; set; }

        #region IConvertable

        public string ToJson(bool beautify, int nbTab = 0)
        {
            string depthTab = "";
            for (int i = 0; i < nbTab; i++)
            {
                depthTab += "\t";
            }

            string jsonStr = depthTab + "{\n";
            jsonStr += depthTab + "\t" + "\"_class\":" + "\"" + GetType().ToString() + "\"," + "\n";
            jsonStr += depthTab + "\t" + "\"name\":" + "\"" + Name + "\"," + "\n";
            jsonStr += depthTab + "}";
            return jsonStr;
        }

        #endregion
    }
}
