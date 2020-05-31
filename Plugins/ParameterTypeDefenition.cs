using Library;

namespace Plugins
{
    public abstract class ParameterTypeDefenition : IDescribable
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
    }
}
