using Library.Class;
using Library.Plugins.Node;
using System.IO;

namespace Library.StandardImplementation.StandardNode
{
    public class StandardNode : Node
    {
        #region IConfigurable Implementation

        public override void LoadConfig(Config config)
        {
            Name = config.GetParameterValue<StringParameterDefinition.StringParameterDefinition>("name");
            Description = config.GetParameterValue<StringParameterDefinition.StringParameterDefinition>("description");
            Labels = config.GetParameterValue<StringParameterDefinition.StringParameterDefinition>("labels");
        }

        public override void SaveConfig(Config config)
        {
            
        }

        #endregion

        #region IRepository Implementation

        public sealed override void CreateRepository(string path)
        {
            base.CreateRepository(path);

            string dirPath = path + "\\" + Name;

            //Create Config File
            string jsonStr = "{\n";
            jsonStr += "\t" + "\"description\":" + "\"" + Description + "\"," + "\n";
            jsonStr += "\t" + "\"label\":" + "\"" + Labels + "\"" + "\n";
            jsonStr += "}";

            string configFile = dirPath + "\\config.json";

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(File.Create(configFile)))
            {
                file.WriteLine(jsonStr);
            }
        }

        #endregion
    }
}
