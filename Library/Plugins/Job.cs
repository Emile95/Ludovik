using Library.Class;
using Library.Class.Node;
using Library.Exception;
using Library.Interface;
using Library.StandardImplementation.JobBuildLogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Library.Plugins.Job
{
    public abstract class Job : ILoadable, IConfigurable, IRunnable, IRepository, IConvertable
    {
        #region Properties and Constructor

        public string ClassName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Property> Properties { get; set; }

        public Node Node { get; set; }

        public Job()
        {
            Properties = new List<Property>();
        }

        #endregion

        #region Private Methods

        private Build CreateBuild()
        {
            string buildNumberPath = "jobs\\" + Name + "\\nextBuildNumber";
            string buildNumberStr = File.ReadAllText(buildNumberPath);
            int buildNumber = Convert.ToInt32(buildNumberStr);

            Build build = new Build(buildNumber++, "#" + buildNumberStr, "");

            build.CreateRepository("jobs\\" + Name + "\\builds");

            //Incremente the build in the file
            File.WriteAllText(buildNumberPath, buildNumber.ToString());

            return build;
        }

        #endregion

        #region Protected Methods

        protected void CheckIfBuildCanceled(CancellationToken taskCancelToken, Build build, JobBuildLogger logger, Action action = null)
        {
            //Verify if the build was cancelled
            if (taskCancelToken.IsCancellationRequested)
            {
                logger.Log(new Log("Cancelled at " + DateTime.Now));
                action();
                build.Status = "Cancelled";
                taskCancelToken.ThrowIfCancellationRequested();
            }
        }

        #endregion

        #region Abstract Methods

        public abstract void Build(Build build, Class.Environment env, CancellationToken taskCancelToken, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers);

        public virtual void PreBuild(Build build, Class.Environment env, CancellationToken taskCancelToken, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers) { }

        public virtual void AfterBuild(Build build, Class.Environment env, CancellationToken taskCancelToken, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers) { }

        #endregion

        #region ILoadable implementation

        public abstract void LoadFromFolder(string path, string folderName);

        #endregion

        #region IConvertable Implementation

        public abstract string ToJson(bool beautify, int nbTab = 0);

        #endregion

        #region IConfigurable implementation

        public abstract Config GetConfig();

        public abstract void LoadConfig(Config config);

        #endregion

        #region IRunnable Implementation

        public void Run(CancellationToken taskCancelToken, LoggerList loggers)
        {
            //Create Build Object
            Build build = CreateBuild();
            //Create Job Build logger
            JobBuildLogger buildLogger = new JobBuildLogger(Name, build.Number);
            loggers.AddLogger(buildLogger);

            //Start Execution

            buildLogger.Log(new Log("Start at " + DateTime.Now + "\n"));

            FailedBuildTokenSource failedTokenSource = new FailedBuildTokenSource();

            try
            {
                //Create the build Environment
                Class.Environment env = new Class.Environment();

                foreach(Property prop in Properties)
                {
                    prop.Definition.Apply(env, prop.Parameters.ToArray(), Node, failedTokenSource, loggers);
                    CheckIfBuildCanceled(taskCancelToken,build,buildLogger);
                    failedTokenSource.Token.ThrowIfFailed();
                }

                env.Properties.Add("buildNumber", build.Number.ToString());

                PreBuild(build, env, taskCancelToken, failedTokenSource, loggers);
                Build(build, env, taskCancelToken, failedTokenSource, loggers);
                AfterBuild(build, env, taskCancelToken, failedTokenSource, loggers);

                buildLogger.Log(new Log("\nBUILD SUCCESS"));
                buildLogger.Log(new Log("\nEnd at " + DateTime.Now));

                build.Status = "SUCCESS";
            }
            catch(OperationCanceledException e)
            {
                throw e;
            }
            catch(FailedBuildException e)
            {
                build.Status = "FAILED";
                buildLogger.Log(new Log("BUILD FAILED"));
                buildLogger.Log(new Log("\nEnd at " + DateTime.Now));
            }
        }

        #endregion

        #region IRepository Implementation

        public virtual void CreateRepository(string path)
        {
            Directory.CreateDirectory(path+"\\"+Name);

            File.WriteAllText(path + "\\" + Name + "\\nextBuildNumber", "1");

            string dirPath = path + "\\" + Name;

            string configFile = dirPath + "\\config.json";

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(File.Create(configFile)))
            {
                file.WriteLine(ToJson(true));
            }

            //Create Builds Directory
            Directory.CreateDirectory(dirPath + "\\builds");
        }

        #endregion
    }
}
