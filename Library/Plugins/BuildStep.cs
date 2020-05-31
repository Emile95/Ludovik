namespace Library.Plugins
{
    public abstract class BuildStep
    {
        public ParameterSetting[] ParamSettings { get; protected set; }
        public abstract void Action(string[] paramValues);
    }
}
