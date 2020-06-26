using Library.Plugins.Logger;

namespace Library.Interface
{
    public interface IRunnable
    {
        void Run(Logger logger = null);
    }
}
