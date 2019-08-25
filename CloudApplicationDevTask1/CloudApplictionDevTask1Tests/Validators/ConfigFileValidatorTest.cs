using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudApplicationDevTask1.validators;
using System.Collections.Generic;
using CloudApplicationDevTask1.Services;

namespace CloudApplictionDevTask1Tests
{
    [TestClass]
    public class ConfigFileValidatorTest
    {
        [TestMethod]
        public void ContainsValidLogFilePath()
        {
            // Arrange
            string configFileContent = @"
                // The default log file name.
                DEFAULT-LOGFILE,""AT1 - log.txt""
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsLogFilePath(configFileContent);

            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidLogFilePath()
        {
            // Arrange
            string configFileContent = @"
                Log file path is missing
                in this 'file content'
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsLogFilePath(configFileContent);

            Assert.AreEqual(ConfigFileValidator.ConfigErrors["LogFilePath"], errorMsg);
        }

        [TestMethod]
        public void ContainsValidLimitSection()
        {
            // Arrange
            string configFileContent = @"
                // Minimum and maximum limits on the number of
                // tasks and processors and processor frequencies.
                LIMITS-TASKS,1,500
                LIMITS-PROCESSORS,1,1000
                LIMITS-PROCESSOR-FREQUENCIES,0,10
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsLimitSection(configFileContent);

            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidLimitSection()
        {
            // Arrange
            string configFileContent = @"
                LIMITS-TASKS,1,500
                missing limits for processors
                LIMITS-PROCESSOR-FREQUENCIES,0,10
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsLimitSection(configFileContent);

            Assert.AreEqual(ConfigFileValidator.ConfigErrors["LimitSection"], errorMsg);
        }

        [TestMethod]
        public void ContainsValidParallelSection()
        {
            // Arrange
            string configFileContent = @"
                PROGRAM-MAXIMUM-DURATION,9
                PROGRAM-TASKS,5
                PROGRAM-PROCESSORS,3
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsParallelSection(configFileContent);

            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidParallelSection()
        {
            // Arrange
            string configFileContent = @"
                missing max duration
                PROGRAM-TASKS,5
                PROGRAM-PROCESSORS,3
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsParallelSection(configFileContent);

            Assert.AreEqual(ConfigFileValidator.ConfigErrors["ParallelSection"], errorMsg);
        }

        [TestMethod]
        public void ContainsValidFrequency()
        {
            // Arrange
            string configFileContent = @"
                // Task runtimes are based on tasks executing on
                // a processor running at the following frequency (GHz).
                RUNTIME-REFERENCE-FREQUENCY,2
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsFrequency(configFileContent);

            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidFrequency()
        {
            // Arrange
            string configFileContent = @"
                Config without frequency
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsFrequency(configFileContent);

            Assert.AreEqual(ConfigFileValidator.ConfigErrors["Frequency"], errorMsg);
        }

        [TestMethod]
        public void ContainsValidRuntimes()
        {
            // Arrange
            string configFileContent = @"   
                // Task IDs and their runtime values.
                TASK-ID,RUNTIME
                1,1
                2,3
                3,5
                4,7
                5,9
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsRuntimes(configFileContent);

            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidRuntimes()
        {
            // Arrange
            string configFileContent = @"   
                // Task IDs and their runtime values.
                TASK-ID,RUNTIME
                something else
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsRuntimes(configFileContent);

            Assert.AreEqual(ConfigFileValidator.ConfigErrors["Runtimes"], errorMsg);
        }


        [TestMethod]
        public void ContainsInvalidRuntimesIds()
        {
            // Arrange
            string configFileContent = @"   
                // The task with id 1 is specified twice
                TASK-ID,RUNTIME
                1,1
                1,3
                3,5
                4,7
                5,9
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsRuntimes(configFileContent);

            Assert.AreEqual(ConfigFileValidator.ConfigErrors["RuntimesIds"], errorMsg);
        }

        [TestMethod]
        public void ContainsValidProcessorFrequencies()
        {
            // Arrange
            string configFileContent = @"   
                // Processor IDs and their frequency values.
                PROCESSOR-ID,FREQUENCY
                1,1.7
                2,2.3
                3,2.9
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsProcessorFrequencies(configFileContent);

            Assert.AreEqual("", errorMsg);
        }


        [TestMethod]
        public void ContainsInvalidProcessorFrequencies()
        {
            // Arrange
            string configFileContent = @"   
                // Processor IDs and their frequency values.
                2,2.9
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsProcessorFrequencies(configFileContent);

            Assert.AreEqual(ConfigFileValidator.ConfigErrors["ProcessorFrequencies"], errorMsg);
        }


        [TestMethod]
        public void ContainsInvalidProcessorFrequenciesIds()
        {
            // Arrange
            string configFileContent = @"   
                // Processor with id 1 is used more than once
                PROCESSOR-ID,FREQUENCY
                1,1.7
                2,2.3
                1,2.9
            ";

            // Act
            string errorMsg = ConfigFileValidator.ContainsProcessorFrequencies(configFileContent);

            Assert.AreEqual(ConfigFileValidator.ConfigErrors["ProcessorFrequenciesIds"], errorMsg);
        }

    }
}
