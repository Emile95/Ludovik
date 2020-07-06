using Library.Class;
using Library.Class.Node;

namespace Library.Plugins.BuildStepDefinition
{
    public abstract class BuildStepDefinition : PropertyDefinition.PropertyDefinition
    {
        public override abstract void Apply(Environment env, Parameter[] parameters, Node node, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers);
    }
}
