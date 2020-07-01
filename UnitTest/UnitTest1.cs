using Library.Class;
using Library.Plugins;
using Library.Plugins.Job;
using Library.StandardImplementation.DescriptionPropertyDefinition;
using Library.StandardImplementation.LabelParameterDefinition;
using Library.StandardImplementation.ParameterizedRunPropertyDefinition;
using Library.StandardImplementation.StandardJob;
using Library.StandardImplementation.StringParameterDefinition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Config config = new Config();

            config.AddProperty(new DescriptionPropertyDefinition(), new string[] { "job1", "standard job" });

            Job job = new StandardJob();
            job.LoadConfig(config);

            job.CreateRepository("jobs");

            Assert.AreEqual(job.Name,"job1");
        }
    }
}
