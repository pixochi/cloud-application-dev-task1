using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudApplicationDevTask1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.Services.Tests
{
    [TestClass()]
    public class AllocationServiceTests
    {
        [TestMethod()]
        public void GetEnergyConsumedPerTaskTest()
        {
            // Arrange
            List<float> coefficients = new List<float>() { 1, 2, 3 };
            float frequency = 1;
            float runtime = 1;

            // Act
            float energyConsumed = AllocationService.GetEnergyConsumedPerTask(coefficients, frequency, runtime);

            // Assert
            Assert.AreEqual(6, energyConsumed);
        }

        [TestMethod()]
        public void GetTotalEnergyConsumedTest()
        {
            // Arrange
            string configFileContent = @"
                // Task runtimes are based on tasks executing on
                // a processor running at the following frequency (GHz).
                RUNTIME-REFERENCE-FREQUENCY,1

                // Task IDs and their runtime values.
                TASK-ID,RUNTIME
                1,1
                2,1
                3,1
                4,1
                5,1

                // Processor IDs and their frequency values.
                PROCESSOR-ID,FREQUENCY
                1,1
                2,1
                3,1

                // Quadratic coefficient IDs and their values.
                COEFFICIENT-ID,VALUE
                0,1
                1,1
                2,1
            ";

            List<List<bool>> allocation = new List<List<bool>>();
            List<bool> processor1 = new List<bool>() {
                true, true, false, false, false
            };
            List<bool> processor2 = new List<bool>() {
                false, false, true, true, false
            };
            List<bool> processor3 = new List<bool>() {
                false, false, false, false, true
            };

            allocation.Add(processor1);
            allocation.Add(processor2);
            allocation.Add(processor3);

            // Act
            float energyConsumed = AllocationService.GetTotalEnergyConsumed(configFileContent, allocation);

            // Assert
            Assert.AreEqual(15, Math.Round(energyConsumed, 2));
        }

        [TestMethod()]
        public void GetTaskRuntimeTest()
        {
            // Arrange
            float referenceFrequency = 2;
            float runtime = 1;
            float frequency = 1;

            // Act
            float taskRuntime = AllocationService.GetTaskRuntime(referenceFrequency, runtime, frequency);

            // Assert
            Assert.AreEqual(2, taskRuntime);
        }

        [TestMethod()]
        public void GetAllocationRuntimeTest()
        {
            // Arrange
            string configFileContent = @"
                // Task runtimes are based on tasks executing on
                // a processor running at the following frequency (GHz).
                RUNTIME-REFERENCE-FREQUENCY,1

                // Task IDs and their runtime values.
                TASK-ID,RUNTIME
                1,1
                2,1
                3,1
                4,1
                5,1

                // Processor IDs and their frequency values.
                PROCESSOR-ID,FREQUENCY
                1,1
                2,1
                3,1

                // Quadratic coefficient IDs and their values.
                COEFFICIENT-ID,VALUE
                0,1
                1,1
                2,1
            ";
            List<List<bool>> allocation = new List<List<bool>>();

            List<bool> processor1 = new List<bool>() {
                true, true, false, false, false
            };
            List<bool> processor2 = new List<bool>() {
                false, false, true, true, false
            };
            List<bool> processor3 = new List<bool>() {
                false, false, false, false, true
            };

            allocation.Add(processor1);
            allocation.Add(processor2);
            allocation.Add(processor3);

            // Act
            float allocationRuntime = AllocationService.GetAllocationRuntime(configFileContent, allocation);

            // Assert
            Assert.AreEqual(2, Math.Round(allocationRuntime, 2));
        }

        [TestMethod()]
        public void IsAllocationRuntimeValidTest()
        {
            // Arrange
            string configFileContent = @"
                ...
                PROGRAM-MAXIMUM-DURATION,8

                // Task runtimes are based on tasks executing on
                // a processor running at the following frequency (GHz).
                RUNTIME-REFERENCE-FREQUENCY,1

               ...
            ";
            float allocationRuntime = 7;

            // Act
            string errorMsg = AllocationService.IsAllocationRuntimeValid(configFileContent, allocationRuntime);
       
            // Assert
            Assert.AreEqual("", errorMsg);
        }
    }
}