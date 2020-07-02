using Library.Class;
using Library.Plugins.BuildStepDefinition;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Library.StandardImplementation.WindowsBatchBuildStepDefinition
{
    public class WindowsBatchBuildStepDefinition : BuildStepDefinition
    {
        public WindowsBatchBuildStepDefinition()
        {
            ClassName = "WindowsBatchBuildStepDefinition";
        }

        public sealed override void Apply(Environment env, Parameter[] parameters)
        {
            string command = parameters.Single(o => o.Name == "command").Value;

            ProcessStartInfo processInfo = new ProcessStartInfo("CMD.exe", command);

            string directory = Path.Combine(System.Environment.CurrentDirectory, "jobs", env.Properties["name"]);

            processInfo.WorkingDirectory = @"F:\Prog\Ludovik\App\WebApi\jobs\" + env.Properties["name"];

            Process process = Process.Start(processInfo);

            process.Close();
        }
    }
}
