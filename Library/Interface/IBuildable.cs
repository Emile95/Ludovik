using Library.Class;
using Library.Plugins.Logger;

namespace Library.Interface
{
    public interface IBuildable
    {
        void PreBuild(Build build, Logger logger);
        void Build(Build build, Logger logger);
        void AfterBuild(Build build, Logger logger);
    }
}
