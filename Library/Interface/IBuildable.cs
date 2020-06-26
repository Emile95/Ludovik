using Library.Plugins.Logger;

namespace Library.Interface
{
    public interface IBuildable
    {
        void PreBuild(Logger logger);
        void Build(Logger logger);
        void AfterBuild(Logger logger);
    }
}
