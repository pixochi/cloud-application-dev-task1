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
            {"ParallelSection", "The config file contains an invalid section containing data related to the parallel program."}
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
    }
}
