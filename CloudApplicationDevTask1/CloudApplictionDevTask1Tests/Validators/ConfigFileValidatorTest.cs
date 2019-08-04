﻿using System;
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
    }
}
