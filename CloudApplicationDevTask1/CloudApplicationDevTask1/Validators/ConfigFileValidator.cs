using CloudApplicationDevTask1.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.validators
{
    public class ConfigFileValidator: FileValidator
    {
        delegate string ValidationFunction(string fileContent);
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

        public static string ContainsLogFilePath(string fileContent)
        {
            Regex logFileRx = new Regex(@"DEFAULT-LOGFILE,"".*\.txt""");
            bool isValid = FileParser.ContainsRegex(fileContent, logFileRx);

            return isValid ? "" : ConfigErrors["LogFilePath"];
        }

        public static string ContainsLimitSection(string fileContent)
        {
            // TODO: check if the first number is always greater than the other one
            Regex limitSectionRx = new Regex(@"LIMITS-TASKS,\d+,\d+(\n|\r|\r\n)\s*LIMITS-PROCESSORS,\d+,\d+(\n|\r|\r\n)\s*LIMITS-PROCESSOR-FREQUENCIES,\d+,\d+");
            bool isValid = FileParser.ContainsRegex(fileContent, limitSectionRx);

            return isValid ? "" : ConfigErrors["LimitSection"];
        }

        public static string ContainsParallelSection(string fileContent)
        {
            // TODO: save (\n|\r|\r\n) as a newline
            Regex parallelSectionRx = new Regex(@"PROGRAM-MAXIMUM-DURATION,\d+(\n|\r|\r\n)\s*PROGRAM-TASKS,\d+(\n|\r|\r\n)\s*PROGRAM-PROCESSORS,\d+");
            bool isValid = FileParser.ContainsRegex(fileContent, parallelSectionRx);

            return isValid ? "" : ConfigErrors["ParallelSection"];
        }

        public static string ContainsFrequency(string fileContent)
        {
            Regex frequencyConfigRx = new Regex(@"RUNTIME-REFERENCE-FREQUENCY,\d+");
            bool isValid = FileParser.ContainsRegex(fileContent, frequencyConfigRx);

            return isValid ? "" : ConfigErrors["Frequency"];
        }

        public static string ContainsRuntimes(string fileContent)
        {
            Regex runtimesRx = new Regex(@"TASK-ID,RUNTIME(?:\n|\r|\r\n)(?:\s*(\d+),\d+(?:\n|\r|\r\n)?)+");
            Match match = FileParser.GetRegexMatch(fileContent, runtimesRx);

            if (!match.Success) {
                return ConfigErrors["Runtimes"];
            }
            else {
                List<string> taskIds = new List<string>();

                // check if task ids are repeating
                for (int captureGroupIndex = 1; captureGroupIndex < match.Groups.Count; captureGroupIndex++) {
                    foreach (Capture capture in match.Groups[captureGroupIndex].Captures) {
                        taskIds.Add(capture.Value);
                    }
                }

                // task ids are not unique
                if (taskIds.Distinct().Count() != taskIds.Count) {
                    return ConfigErrors["RuntimesIds"];
                }
                return "";
            }
        }

        public static string ContainsProcessorFrequencies(string fileContent)
        {
            List<float> processorFrequencies = ConfigFileParser.GetProcessorFrequencies(fileContent);
            List<string> processorIds = ConfigFileParser.GetProcessorIds(fileContent);

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

        public static float GetTotalEnergyConsumed(string configFileContent, List<List<bool>> allocation)
        {
            List<float> coefficients = ConfigFileParser.GetCoefficients(configFileContent);
            List<float> processorFrequencies = ConfigFileParser.GetProcessorFrequencies(configFileContent);
            List<float> taskRuntimes = ConfigFileParser.GetTaskRuntimes(configFileContent);
            float runtimeReferenceFrequency = ConfigFileParser.GetRuntimeReferenceFrequency(configFileContent);
            float totalEnergyConsumed = 0;

            if (coefficients.Count == 0 || processorFrequencies.Count == 0 || taskRuntimes.Count == 0 || allocation.Count == 0 || runtimeReferenceFrequency == -1) {
                return -1;
            }
            else
            {
                // Calculate energy consumed by each task
                for (int taskId = 0; taskId < taskRuntimes.Count; taskId++)
                {
                    int processorId = 0;
                    
                    // find the processor that handles a given task
                    for (int allocationProcessorId = 0; allocationProcessorId < allocation.Count; allocationProcessorId++)
                    {
                        if (allocation[allocationProcessorId][taskId])
                        {
                            processorId = allocationProcessorId;
                            break;
                        }
                    }

                    float runtimeOnGivenProcessor = GetRuntimePerFrequency(runtimeReferenceFrequency, taskRuntimes[taskId], processorFrequencies[processorId]);
                    float energyConsumedPerTask = GetEnergyConsumedPerTask(coefficients, processorFrequencies[processorId], runtimeOnGivenProcessor);
                    totalEnergyConsumed += energyConsumedPerTask;
                }

                return totalEnergyConsumed;
            }
        }

        public static float GetEnergyConsumedPerTask(List<float> coefficients, float frequency, float runtime)
        {
            return (coefficients[2] * frequency * frequency + coefficients[1] * frequency + coefficients[0]) * runtime;
        }

        public static List<string> ValidateAll(string fileContent)
        {
            List<string> errorsList = new List<string>();

            foreach (var taskAllocationFileValidator in validationMethods) {
                string errorMsg = taskAllocationFileValidator(fileContent);

                if (errorMsg != "") {
                    errorsList.Add(errorMsg);
                }
            }

            return errorsList;
        }

        private static float GetRuntimePerFrequency(float referenceFrequency, float runtime, float frequency)
        {
            float runtimePerUnit = referenceFrequency / runtime;
            return runtimePerUnit / frequency;
        }

    }
}
