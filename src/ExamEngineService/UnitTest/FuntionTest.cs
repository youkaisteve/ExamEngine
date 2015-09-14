using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class FuntionTest
    {
        [TestMethod]
        public void PlayTest()
        {
            WorkflowCallWapper.WorkflowProxy proxy = new WorkflowCallWapper.WorkflowProxy();
            //var data = proxy.GetUnProcessTaskByUser("007", 0, 100);
            var data = proxy.GetUnProcessTaskByUser("007", 0, 1);
            Console.Write(data);
        }
    }
}
