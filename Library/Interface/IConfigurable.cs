using Library.Class;

namespace Library.Interface
{
    public interface IConfigurable
    {
        Config GetConfig();

        void SaveConfig(Config config);

        void LoadFromConfig(Config config);
    }
}
