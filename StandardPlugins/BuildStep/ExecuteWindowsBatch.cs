using Library;
using Library.Plugins;

namespace StandardPlugins
{
    public class ExecuteWindowsBatch : BuildStep
    {
        public ExecuteWindowsBatch()
        {
            ParamSettings = new ParameterSetting[] {
                new ParameterSetting("Command", new StringParameter())
            };
        }

        public override void Action(string[] paramValues)
        {
            System.Diagnostics.Process.Start("CMD.exe", paramValues[0]);
        }
    }
}
