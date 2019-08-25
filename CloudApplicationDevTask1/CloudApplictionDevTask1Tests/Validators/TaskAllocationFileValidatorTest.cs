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
            // Arrange
            string TANFileContent = @"
                TASKS,5 // The number of tasks
                PROCESSORS,3
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.DataMixedWithComments(TANFileContent);

            // Assert
            Assert.AreNotEqual("", errorMsg);
        }

        [TestMethod]
        public void DataNotMixedWithCommentsIsValid()
        {
            // Arrange
            string TANFileContent = @"
                // The number of tasks
                TASKS,5
                PROCESSORS,3
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.DataMixedWithComments(TANFileContent);

            // Assert
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsValidConfigPath()
        {
            // Arrange
            string TANFileContent = @"
                CONFIGURATION,""C:\\temp\config.csv""
                TASKS,5
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidConfigPath(TANFileContent);

            // Assert
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidConfigPath()
        {
            // Arrange
            string TANFileContent = @"
                CONFIGURATION
                TASKS,5
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidConfigPath(TANFileContent);

            // Assert
            Assert.AreNotEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsValidTasksLine()
        {
            // Arrange
            string TANFileContent = @"
                CONFIGURATION,""C:\\temp\config.csv""
                TASKS,5
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidTasksLine(TANFileContent);

            // Assert
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidTasksLine()
        {
            // Arrange
            string TANFileContent = @"
                CONFIGURATION,""C:\\temp\config.csv""
                TASKS,with a missing number
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidTasksLine(TANFileContent);

            // Assert
            Assert.AreNotEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsValidProcessorsLine()
        {
            // Arrange
            string TANFileContent = @"
                TASKS,5
                PROCESSORS,3
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidProcessorsLine(TANFileContent);

            // Assert
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidProcessorsLine()
        {
            // Arrange
            string TANFileContent = @"
                TASKS,5
                PROCESSORS,missing number
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidProcessorsLine(TANFileContent);

            // Assert
            Assert.AreNotEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsValidAllocationsLine()
        {
            // Arrange
            string TANFileContent = @"
                PROCESSORS,3
                ALLOCATIONS,8
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocationsLine(TANFileContent);

            // Assert
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidAllocationsLine()
        {
            // Arrange
            string TANFileContent = @"
                PROCESSORS,3
                ALLOCATIONS,missing number
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocationsLine(TANFileContent);

            // Assert
            Assert.AreNotEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsValidAllocations()
        {
            // Arrange
            string TANFileContent = @"
                // The number of tasks and processors per allocation.
                TASKS,5
                PROCESSORS,3

                // The number of allocations in this file.
                ALLOCATIONS,2

                // The set of allocations.
                // The ith row is the allocation of tasks to the ith processor.
                // The jth column is the allocation of the jth task to a processor.
                ALLOCATION-ID,1
                1,1,0,0,0
                0,0,1,1,0
                0,0,0,0,1

                ALLOCATION-ID,2
                1,1,0,0,0
                0,0,0,0,1
                0,0,1,1,0
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocations(TANFileContent);

            // Assert
            Assert.AreEqual("", errorMsg);
        }


        [TestMethod]
        public void WrongAllocationCount()
        {
            // Arrange
            string TANFileContent = @"
                ALLOCATIONS,2

                ALLOCATION-ID,1
                1,1,0,0,0
                0,0,1,1,0
                0,0,0,0,1
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocations(TANFileContent);

            // Assert
            Assert.AreEqual(TaskAllocationFileValidator.AllocationErrors["WrongAllocationCount"], errorMsg);
        }

        [TestMethod]
        public void InvalidTaskAllocation()
        {
            // Arrange
            string TANFileContent = @"
                ALLOCATIONS,1

                ALLOCATION-ID,1
                1,1,0,0,0
                1,0,1,1,0
                0,0,0,0,1
            ";

            // Act
            string errorMsg = TaskAllocationFileValidator.ContainsValidAllocations(TANFileContent);

            // Assert
            Assert.AreEqual(TaskAllocationFileValidator.AllocationErrors["InvalidTaskAllocation"], errorMsg);
        }
    }
}
