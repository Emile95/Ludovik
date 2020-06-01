using Library.Interface;

namespace Library.Plugins.BuildStep
{
    public abstract class BuildStep : ISettable
    {
        public abstract void Action(string[] paramValues);
    }
}
