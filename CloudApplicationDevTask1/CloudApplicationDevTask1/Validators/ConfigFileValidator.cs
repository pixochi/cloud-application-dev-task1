using CloudApplicationDevTask1.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.validators
{
    // Validator for a configuration file
    public class ConfigFileValidator: FileValidator
    {
        delegate string ValidationFunction(string configFileContent);
        private static List<ValidationFunction> validationMethods = new List<ValidationFunction>() {
               DataMixedWithComments,
               ContainsLogFilePath,
               ContainsLimitSection,
               ContainsParallelSection,
               ContainsFrequency,
               ContainsRuntimes,
               ContainsProcessorFrequencies,
        };
   
        public static Dictionary<string, string> ConfigErrors = new Dictionary<string, string>(){
            {"LogFilePath", "The config file contains an invalid path to a log file."},
            {"LimitSection", "The config file contains an invalid section of minimum and maximum limits for the number of tasks, the number of processors, and the processor frequencies."},
            {"ParallelSection", "The config file contains an invalid section containing data related to the parallel program."},
            {"Frequency", "The config file contains an invalid frequency of a reference processor"},
            {"Runtimes", "The config file contains an invalid section containing tasks runtimes." },
            {"RuntimesIds", "The config file contains repeating task ids"},
            {"ProcessorFrequencies", "The config file contains an invalid section with processor frequencies"},
            {"ProcessorFrequenciesIds", "The config file contains repeating processor ids"}
        };

        /// <summary>
        /// Checks if a provided configuration file contains a path of a log file
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="configFileContent">Content of a configuration file</param>
        public static string ContainsLogFilePath(string configFileContent)
        {
            Regex logFileRx = new Regex(@"DEFAULT-LOGFILE,"".*\.txt""");
            bool isValid = FileParser.ContainsRegex(configFileContent, logFileRx);

            return isValid ? "" : ConfigErrors["LogFilePath"];
        }

        /// <summary>
        /// Checks if a provided configuration file contains a section with limits
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="configFileContent">Content of a configuration file</param>
        public static string ContainsLimitSection(string configFileContent)
        {
            Regex limitSectionRx = new Regex($@"LIMITS-TASKS,\d+,\d+({FileParser.NewLineRx})\s*LIMITS-PROCESSORS,\d+,\d+({FileParser.NewLineRx})\s*LIMITS-PROCESSOR-FREQUENCIES,\d+,\d+");
            bool isValid = FileParser.ContainsRegex(configFileContent, limitSectionRx);

            return isValid ? "" : ConfigErrors["LimitSection"];
        }

        /// <summary>
        /// Checks if a provided configuration file contains a section with a parallel section
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="configFileContent">Content of a configuration file</param>
        public static string ContainsParallelSection(string configFileContent)
        {
            Regex parallelSectionRx = new Regex($@"PROGRAM-MAXIMUM-DURATION,\d+({FileParser.NewLineRx})\s*PROGRAM-TASKS,\d+({FileParser.NewLineRx})\s*PROGRAM-PROCESSORS,\d+");
            bool isValid = FileParser.ContainsRegex(configFileContent, parallelSectionRx);

            return isValid ? "" : ConfigErrors["ParallelSection"];
        }

        /// <summary>
        /// Checks if a provided configuration file contains a reference frequency
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="configFileContent">Content of a configuration file</param>
        public static string ContainsFrequency(string configFileContent)
        {
            Regex frequencyConfigRx = new Regex(@"RUNTIME-REFERENCE-FREQUENCY,\d+");
            bool isValid = FileParser.ContainsRegex(configFileContent, frequencyConfigRx);

            return isValid ? "" : ConfigErrors["Frequency"];
        }

        /// <summary>
        /// Checks if a provided configuration file contains runtime of each task
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="configFileContent">Content of a configuration file</param>
        public static string ContainsRuntimes(string configFileContent)
        {
            Regex runtimesRx = new Regex($@"TASK-ID,RUNTIME(?:{FileParser.NewLineRx})(?:\s*(\d+),\d+(?:{FileParser.NewLineRx})?)+");
            Match match = FileParser.GetRegexMatch(configFileContent, runtimesRx);

            if (!match.Success)
            {
                return ConfigErrors["Runtimes"];
            }
            else 
            {
                List<string> taskIds = new List<string>();

                // check if task ids are repeating
                for (int captureGroupIndex = 1; captureGroupIndex < match.Groups.Count; captureGroupIndex++)
                {
                    foreach (Capture capture in match.Groups[captureGroupIndex].Captures)
                    {
                        taskIds.Add(capture.Value);
                    }
                }

                // task ids are not unique
                if (taskIds.Distinct().Count() != taskIds.Count)
                {
                    return ConfigErrors["RuntimesIds"];
                }
                return "";
            }
        }

        /// <summary>
        /// Checks if a provided configuration file contains frequencies of processors
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="configFileContent">Content of a configuration file</param>
        public static string ContainsProcessorFrequencies(string configFileContent)
        {
            List<float> processorFrequencies = ConfigFileParser.GetProcessorFrequencies(configFileContent);
            List<string> processorIds = ConfigFileParser.GetProcessorIds(configFileContent);

            if (processorFrequencies.Count == 0 || processorIds.Count == 0 || processorFrequencies.Count != processorIds.Count)
            {
                return ConfigErrors["ProcessorFrequencies"];
            }
            else
            {
                // check if processor ids are unique
                if (processorIds.Distinct().Count() != processorIds.Count)
                {
                    return ConfigErrors["ProcessorFrequenciesIds"];
                }

                return "";
            }
        }

        /// <summary>
        /// Checks validity of a provided configuration file
        /// </summary>
        /// <returns>
        /// A list of all error messages
        /// </returns>
        /// <param name="configFileContent">Content of a configuration file</param>
        public static List<string> ValidateAll(string configFileContent)
        {
            List<string> errorsList = new List<string>();

            foreach (var taskAllocationFileValidator in validationMethods)
            {
                string errorMsg = taskAllocationFileValidator(configFileContent);

                if (errorMsg != "")
                {
                    errorsList.Add(errorMsg);
                }
            }

            return errorsList;
        }

    }
}
