using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exam.Service.Implement;
using Exam.Service.Interface;
using System.ComponentModel.Composition;
using Exam.Model;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class FuntionTest
    {
        [TestMethod]
        public void PlayTest()
        {
            WorkflowCallWapper.WorkflowProxy proxy = new WorkflowCallWapper.WorkflowProxy();
            var allProcess = proxy.GetAllProcessDefinitions();

            var result = allProcess.Select(p => new ProcessWithNodeModel
            {
                ProcessName = p.ProcessName,
                Tasks = proxy.GetProcessAllTask(p.ProcessName)
            }).ToList();
        }
    }
}
