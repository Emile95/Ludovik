using Library.Interface.ISettable;

namespace Library.Plugins.BuildStep
{
    public abstract class BuildStep : ISettable
    {
        public abstract ISettable.ParameterDefenition[] GetSettingDefenitions();
        public abstract void Action(string[] paramValues);
    }
}
