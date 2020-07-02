using Library.Class;
using Library.Plugins.PropertyDefinition;
using System.Linq;

namespace Library.StandardImplementation.NodePropertyDefinition
{
    public class NodePropertyDefinition : PropertyDefinition
    {
        public NodePropertyDefinition()
        {
            ClassName = "NodePropertyDefinition";
        }

        #region PropertyDefinition Implementation

        public sealed override void Apply(Environment env, Parameter[] parameters)
        {
            env.Properties.Add("node", parameters.Single(o => o.Name == "label").Value);
        }

        #endregion
    }
}
