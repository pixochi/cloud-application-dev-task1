using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudApplicationDevTask1.validators;
using CloudApplictionDevTask1Tests.Dummies;
using System.Collections.Generic;

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
            string configFileContent = @"
                // Quadratic coefficient IDs and their values.
                MISSING HEADER LINE
                0,25
                1,-25
                2,10
            ";

            List<List<bool>> allocation = new List<List<bool>>();
            List<bool> processor1 = new List<bool>() {
                true, true, false, false, false
            };
            allocation.Add(processor1);

            float energyConsumed = ConfigFileValidator.GetTotalEnergyConsumed(configFileContent, allocation);

            Assert.AreEqual(-1, energyConsumed);
        }

        [TestMethod]
        public void CalculatesEnergyConsumedCorrectly()
        {
            string configFileContent = @"
                // Task runtimes are based on tasks executing on
                // a processor running at the following frequency (GHz).
                RUNTIME-REFERENCE-FREQUENCY,2

                // Task IDs and their runtime values.
                TASK-ID,RUNTIME
                1,1
                2,1
                3,2
                4,1
                5,3

                // Processor IDs and their frequency values.
                PROCESSOR-ID,FREQUENCY
                1,1.7
                2,2.3
                3,2.9

                // Quadratic coefficient IDs and their values.
                COEFFICIENT-ID,VALUE
                0,25
                1,-25
                2,10
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

            float energyConsumed = ConfigFileValidator.GetTotalEnergyConsumed(configFileContent, allocation);

            Assert.AreEqual(193.8, Math.Round(energyConsumed, 2));
        }
    }
}
