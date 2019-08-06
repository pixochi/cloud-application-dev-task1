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
            bool isValid = ContainsRegex(fileContent, logFileRx);

            return isValid ? "" : ConfigErrors["LogFilePath"];
        }

        public static string ContainsLimitSection(string fileContent)
        {
            // TODO: check if the first number is always greater than the other one
            Regex limitSectionRx = new Regex(@"LIMITS-TASKS,\d+,\d+(\n|\r|\r\n)\s*LIMITS-PROCESSORS,\d+,\d+(\n|\r|\r\n)\s*LIMITS-PROCESSOR-FREQUENCIES,\d+,\d+");
            bool isValid = ContainsRegex(fileContent, limitSectionRx);

            return isValid ? "" : ConfigErrors["LimitSection"];
        }

        public static string ContainsParallelSection(string fileContent)
        {
            // TODO: save (\n|\r|\r\n) as a newline
            Regex parallelSectionRx = new Regex(@"PROGRAM-MAXIMUM-DURATION,\d+(\n|\r|\r\n)\s*PROGRAM-TASKS,\d+(\n|\r|\r\n)\s*PROGRAM-PROCESSORS,\d+");
            bool isValid = ContainsRegex(fileContent, parallelSectionRx);

            return isValid ? "" : ConfigErrors["ParallelSection"];
        }

        public static string ContainsFrequency(string fileContent)
        {
            Regex frequencyConfigRx = new Regex(@"RUNTIME-REFERENCE-FREQUENCY,\d+");
            bool isValid = ContainsRegex(fileContent, frequencyConfigRx);

            return isValid ? "" : ConfigErrors["Frequency"];
        }

        public static string ContainsRuntimes(string fileContent)
        {
            Regex runtimeRx = new Regex(@"TASK-ID,RUNTIME(?:\n|\r|\r\n)(?:\s*(\d+),\d+(?:\n|\r|\r\n)?)+");
            Match match = GetRegexMatch(fileContent, runtimeRx);

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
            Regex runtimeRx = new Regex(@"PROCESSOR-ID,FREQUENCY(?:\n|\r|\r\n)(?:\s*(\d+),\d+\.\d+(?:\n|\r|\r\n)?)+");
            Match match = GetRegexMatch(fileContent, runtimeRx);

            if (!match.Success) {
                return ConfigErrors["ProcessorFrequencies"];
            }
            else {
                List<string> processorIds = new List<string>();

                // check if processor ids are repeating
                for (int captureGroupIndex = 1; captureGroupIndex < match.Groups.Count; captureGroupIndex++) {
                    foreach (Capture capture in match.Groups[captureGroupIndex].Captures) {
                        processorIds.Add(capture.Value);
                    }
                }

                // processor ids are not unique
                if (processorIds.Distinct().Count() != processorIds.Count) {
                    return ConfigErrors["ProcessorFrequenciesIds"];
                }
                return "";
            }
        }

    }
}
