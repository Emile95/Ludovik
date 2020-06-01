using Library.Plugins.BuildStep;
using Library.Plugins.Parameter;

namespace StandardPlugins
{
    public class ExecuteWindowsBatch : BuildStep
    {
        public override Parameter[] GetSettingDefenitions()
        {
            return new Parameter[] {
                new StringParameter("Command") { }
            };
        }
        public override void Action(string[] paramValues)
        {
            System.Diagnostics.Process.Start("CMD.exe", paramValues[0]);
        }
    }
}
