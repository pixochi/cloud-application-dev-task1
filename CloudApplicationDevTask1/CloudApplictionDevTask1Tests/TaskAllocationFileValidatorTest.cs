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
    }
}
