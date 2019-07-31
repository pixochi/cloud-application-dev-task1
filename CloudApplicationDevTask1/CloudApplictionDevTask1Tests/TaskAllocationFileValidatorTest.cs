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
            string errorMsg = TaskAllocationFileValidator.DataMixedWithComments(Dummies.DataMixedWithComments());
            Assert.AreNotEqual(errorMsg, "");
        }

        [TestMethod]
        public void DataNotMixedWithCommentsIsValid()
        {
            string errorMsg = TaskAllocationFileValidator.DataMixedWithComments(Dummies.DataNotMixedWithComments());
            Assert.AreEqual(errorMsg, "");
        }

        [TestMethod]
        public void ContainsValidConfigPath()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidConfigPath(Dummies.ValidConfigPath());
            Assert.AreEqual(errorMsg, "");
        }

        [TestMethod]
        public void ContainsInvalidConfigPath()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidConfigPath(Dummies.InvalidConfigPath());
            Assert.AreNotEqual(errorMsg, "");
        }

        [TestMethod]
        public void ContainsValidTasksLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidTasksLine(Dummies.ValidTasksLine());
            Assert.AreEqual(errorMsg, "");
        }

        [TestMethod]
        public void ContainsInvalidTasksLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidTasksLine(Dummies.InvalidTasksLine());
            Assert.AreNotEqual(errorMsg, "");
        }

        [TestMethod]
        public void ContainsValidProcessorsLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidProcessorsLine(Dummies.ValidProcessorsLine());
            Assert.AreEqual(errorMsg, "");
        }

        [TestMethod]
        public void ContainsInvalidProcessorsLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidProcessorsLine(Dummies.InvalidProcessorsLine());
            Assert.AreNotEqual(errorMsg, "");
        }

        [TestMethod]
        public void ContainsValidAllocationsLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocationsLine(Dummies.ValidAllocationsLine());
            Assert.AreEqual(errorMsg, "");
        }

        [TestMethod]
        public void ContainsInvalidAllocationsLine()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocationsLine(Dummies.InvalidAllocationsLine());
            Assert.AreNotEqual(errorMsg, "");
        }

        [TestMethod]
        public void ContainsValidAllocations()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocations(Dummies.ValidAllocations());
            Assert.AreEqual(errorMsg, "");
        }


        [TestMethod]
        public void WrongAllocationCount()
        {
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocations(Dummies.InvalidAllocationCount());
            Assert.AreEqual(TaskAllocationFileValidator.AllocationErrors["WrongAllocationCount"], errorMsg);
        }
    }
}
