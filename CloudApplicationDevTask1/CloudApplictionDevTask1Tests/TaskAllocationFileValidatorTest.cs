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
            string errorMsg = TaskAllocationFileValidator.DataMixedWithComments(Dummies.textContentDummy());
            Assert.AreNotEqual(errorMsg, "");
        }
    }
}
