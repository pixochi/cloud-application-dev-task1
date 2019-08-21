using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudApplicationDevTask1.validators;
using CloudApplictionDevTask1Tests.Dummies;

namespace CloudApplictionDevTask1Tests
{
    [TestClass]
    public class ConfigFileValidatorTest
    {
        [TestMethod]
        public void ContainsValidLogFilePath()
        {
            string errorMsg = ConfigFileValidator.ContainsLogFilePath(ConfigFileDummies.ValidLogFilePath());
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidLogFilePath()
        {
            string errorMsg = ConfigFileValidator.ContainsLogFilePath(ConfigFileDummies.InvalidLogFilePath());
            Assert.AreEqual(ConfigFileValidator.ConfigErrors["LogFilePath"], errorMsg);
        }

        [TestMethod]
        public void ContainsValidLimitSection()
        {
            string errorMsg = ConfigFileValidator.ContainsLimitSection(ConfigFileDummies.ValidLimitSection());
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidLimitSection()
        {
            string errorMsg = ConfigFileValidator.ContainsLimitSection(ConfigFileDummies.InvalidLimitSection());
            Assert.AreEqual(ConfigFileValidator.ConfigErrors["LimitSection"], errorMsg);
        }

        [TestMethod]
        public void ContainsValidParallelSection()
        {
            string errorMsg = ConfigFileValidator.ContainsParallelSection(ConfigFileDummies.ValidParallelSection());
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidParallelSection()
        {
            string errorMsg = ConfigFileValidator.ContainsParallelSection(ConfigFileDummies.InvalidParallelSection());
            Assert.AreEqual(ConfigFileValidator.ConfigErrors["ParallelSection"], errorMsg);
        }

        [TestMethod]
        public void ContainsValidFrequency()
        {
            string errorMsg = ConfigFileValidator.ContainsFrequency(ConfigFileDummies.ValidFrequency());
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidFrequency()
        {
            string errorMsg = ConfigFileValidator.ContainsFrequency(ConfigFileDummies.InvalidFrequency());
            Assert.AreEqual(ConfigFileValidator.ConfigErrors["Frequency"], errorMsg);
        }

        [TestMethod]
        public void ContainsValidRuntimes()
        {
            string errorMsg = ConfigFileValidator.ContainsRuntimes(ConfigFileDummies.ValidRuntimes());
            Assert.AreEqual("", errorMsg);
        }

        [TestMethod]
        public void ContainsInvalidRuntimes()
        {
            string errorMsg = ConfigFileValidator.ContainsRuntimes(ConfigFileDummies.InvalidRuntimes());
            Assert.AreEqual(ConfigFileValidator.ConfigErrors["Runtimes"], errorMsg);
        }


        [TestMethod]
        public void ContainsInvalidRuntimesIds()
        {
            string errorMsg = ConfigFileValidator.ContainsRuntimes(ConfigFileDummies.InvalidRuntimesIds());
            Assert.AreEqual(ConfigFileValidator.ConfigErrors["RuntimesIds"], errorMsg);
        }

        [TestMethod]
        public void ContainsValidProcessorFrequencies()
        {
            string errorMsg = ConfigFileValidator.ContainsProcessorFrequencies(ConfigFileDummies.ValidProcessorFrequencies());
            Assert.AreEqual("", errorMsg);
        }


        [TestMethod]
        public void ContainsInvalidProcessorFrequencies()
        {
            string errorMsg = ConfigFileValidator.ContainsProcessorFrequencies(ConfigFileDummies.InvalidProcessorFrequencies());
            Assert.AreEqual(ConfigFileValidator.ConfigErrors["ProcessorFrequencies"], errorMsg);
        }


        [TestMethod]
        public void ContainsInvalidProcessorFrequenciesIds()
        {
            string errorMsg = ConfigFileValidator.ContainsProcessorFrequencies(ConfigFileDummies.InvalidProcessorFrequenciesIds());
            Assert.AreEqual(ConfigFileValidator.ConfigErrors["ProcessorFrequenciesIds"], errorMsg);
        }

        [TestMethod]
        public void ContainsInValidCoefficients()
        {
            string fileContent = @"
                // Quadratic coefficient IDs and their values.
                MISSING HEADER LINE
                0,25
                1,-25
                2,10
            ";

            float energyConsumed = ConfigFileValidator.GetEnergyConsumed(fileContent);

            Assert.AreEqual(-1, energyConsumed);
        }

        [TestMethod]
        public void CalculatesEnergyConsumedCorrectly()
        {
            string fileContent = @"
                // Quadratic coefficient IDs and their values.
                COEFFICIENT-ID,VALUE
                0,25
                1,-25
                2,10
            ";

            float energyConsumed = ConfigFileValidator.GetEnergyConsumed(fileContent);

            Assert.AreEqual(-1, energyConsumed);
        }
    }
}
