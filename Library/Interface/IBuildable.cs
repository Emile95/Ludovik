using Library.Class;

namespace Library.Interface
{
    public interface IBuildable
    {
        void PreBuild(Build build, LoggerList loggers);
        void Build(Build build, LoggerList loggers);
        void AfterBuild(Build build, LoggerList loggers);
    }
}
