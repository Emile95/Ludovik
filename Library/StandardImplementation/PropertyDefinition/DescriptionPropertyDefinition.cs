using Library.Class;
using Library.Class.Node;
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

        public sealed override void Apply(Environment env, Parameter[] parameters, Node node, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers)
        {
            env.Properties.Add("jobName", parameters.Single(o => o.Name == "name").Value);
        }

        #endregion
    }
}
