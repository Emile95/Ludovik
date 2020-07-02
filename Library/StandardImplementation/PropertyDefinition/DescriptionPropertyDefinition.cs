using Library.Class;
using Library.Plugins.PropertyDefinition;
using System.Linq;

namespace Library.StandardImplementation.DescriptionPropertyDefinition
{
    public class DescriptionPropertyDefinition : PropertyDefinition
    {
        public DescriptionPropertyDefinition()
        {
            ClassName = "DescriptionPropertyDefinition";
        }

        #region PropertyDefinition Implementation

        public sealed override void AddToEnvironment(Environment env, Parameter[] parameters)
        {
            env.Properties.Add("name", parameters.Single(o => o.Name == "name").Value);
        }

        #endregion
    }
}
