using Library.Class;

namespace Library.Plugins.BuildStepDefinition
{
    public abstract class BuildStepDefinition : PropertyDefinition.PropertyDefinition
    {
        public override abstract void Apply(Environment env, Parameter[] parameters, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers);
    }
}
