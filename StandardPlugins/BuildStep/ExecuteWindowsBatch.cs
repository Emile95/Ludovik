using Library.Interface.ISettable;
using Library.Plugins.BuildStep;

namespace StandardPlugins
{
    public class ExecuteWindowsBatch : BuildStep
    {
        public ExecuteWindowsBatch()
        {
        }
        public override ISettable.ParameterDefenition[] GetSettingDefenitions()
        {
            return null;
        }
        public override void Action(string[] paramValues)
        {
            System.Diagnostics.Process.Start("CMD.exe", paramValues[0]);
        }
    }
}
