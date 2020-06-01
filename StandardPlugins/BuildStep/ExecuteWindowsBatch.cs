using Library.Plugins.BuildStep;
using Library.Plugins.ParameterType;

namespace StandardPlugins
{
    public class ExecuteWindowsBatch : BuildStep
    {
        public ExecuteWindowsBatch()
        {
            Name = "ExecuteWindowsBatch";
        }

        public override ParameterType[] GetSettingDefenitions()
        {
            return new ParameterType[] {
            };
        }
        public override void Action(string[] paramValues)
        {
            System.Diagnostics.Process.Start("CMD.exe", paramValues[0]);
        }
    }
}
