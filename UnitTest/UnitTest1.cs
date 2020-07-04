using Library.Class.Node;
using Library.Class.Node.Encodable;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Node node = new Node("127.0.0.1");
            node.ConsoleLog(new ConsoleLog() { 
                Log = "hihohoho"
            });
        }
    }
}
