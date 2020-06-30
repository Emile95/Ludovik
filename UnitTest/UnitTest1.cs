using Library.Class;
using Library.Plugins;
using Library.Plugins.Job;
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

            Job job = new StandardJob();
            job.LoadConfig(config);

            PropertyDefinition prop = new ParameterizedRunPropertyDefinition();
            job.Properties.Add(prop);

            PropertyDefinition prop2 = new ParameterizedRunPropertyDefinition();
            job.Properties.Add(prop2);

            job.CreateRepository("jobs");

            Assert.AreEqual(job.Name,"job2");
        }
    }
}
