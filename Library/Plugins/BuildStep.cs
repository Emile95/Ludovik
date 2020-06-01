using Library.Interface.ISettable;

namespace Library.Plugins.BuildStep
{
    public abstract class BuildStep : ISettable
    {
        public virtual Parameter.Parameter[] GetSettingDefenitions() { return new Parameter.Parameter[] { }; }
        public virtual Parameter.Parameter[] GetAdvancedSettingDefenitions() { return new Parameter.Parameter[] { }; }
        public abstract void Action(string[] paramValues);
    }
}
