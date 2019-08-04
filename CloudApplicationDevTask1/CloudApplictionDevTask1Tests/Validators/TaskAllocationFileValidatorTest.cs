using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudApplicationDevTask1;

namespace CloudApplictionDevTask1Tests
{
    [TestClass]
    public class TaskAllocationFileValidatorTest
    {
        [TestMethod]
        public void DataMixedWithCommentsIsInvalid()
        {
            string errorMsg = TaskAllocationFileValidator.DataMixedWithComments(TaskAllocationFileDummies.DataMixedWithComments());
            Assert.AreNotEqual("", errorMsg);
        }

        [TestMethod]
        public void DataNotMixedWithCommentsIsValid()
        {
            string errorMsg = TaskAllocationFileValidator.DataMixedWithComments(TaskAllocationFileDummies.DataNotMixedWithComments());
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsValidConfigPath()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidConfigPath(TaskAllocationFileDummies.ValidConfigPath());
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidConfigPath()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidConfigPath(TaskAllocationFileDummies.InvalidConfigPath());
            Assert.AreNotEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsValidTasksLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidTasksLine(TaskAllocationFileDummies.ValidTasksLine());
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidTasksLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidTasksLine(TaskAllocationFileDummies.InvalidTasksLine());
            Assert.AreNotEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsValidProcessorsLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidProcessorsLine(TaskAllocationFileDummies.ValidProcessorsLine());
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidProcessorsLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidProcessorsLine(TaskAllocationFileDummies.InvalidProcessorsLine());
            Assert.AreNotEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsValidAllocationsLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocationsLine(TaskAllocationFileDummies.ValidAllocationsLine());
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidAllocationsLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocationsLine(TaskAllocationFileDummies.InvalidAllocationsLine());
            Assert.AreNotEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsValidAllocations()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocations(TaskAllocationFileDummies.ValidAllocations());
            Assert.AreEqual("", errorMsg);
        }


        [TestMethod]
        public void WrongAllocationCount()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocations(TaskAllocationFileDummies.InvalidAllocationCount());
            Assert.AreEqual(TaskAllocationFileValidator.AllocationErrors["WrongAllocationCount"], errorMsg);
        }

        [TestMethod]
        public void InvalidTaskAllocation()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocations(TaskAllocationFileDummies.InvalidTaskAllocation());
            Assert.AreEqual(TaskAllocationFileValidator.AllocationErrors["InvalidTaskAllocation"], errorMsg);
        }
    }
}
