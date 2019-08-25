using CloudApplicationDevTask1.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CloudApplicationDevTask1
{
    // Validator for content of a task allocation file
    public class TaskAllocationFileValidator: FileValidator 
    {
        delegate string ValidationFunction(string TANFileContent);
        private static List<ValidationFunction> validationMethods = new List<ValidationFunction>() {
               DataMixedWithComments,
               ContainsValidConfigPath,
               ContainsValidTasksLine,
               ContainsValidProcessorsLine,
               ContainsValidAllocationsLine,
        };
  
        public static Dictionary<string, string> AllocationErrors = new Dictionary<string, string>(){
            {"WrongAllocationCount", "The number of allocations is not correct" },
            {"InvalidTaskAllocation", "Tasks are incorrectly assigned to processors" }
        };

        /// <summary>
        /// Checks if a provided file content contains a valid path of configuration file
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="TANFileContent">Content of a file</param>
        public static string ContainsValidConfigPath(string TANFileContent)
        {
            using (StringReader reader = new StringReader(TANFileContent)) {
                bool isConfigPathValid = false;
                string line;
                Regex rx = new Regex(@"CONFIGURATION,"".*\.csv""");
                while ((line = reader.ReadLine()) != null && !isConfigPathValid) {
                    line = line.Trim();
                    MatchCollection matches = rx.Matches(line);

                    if (matches.Count == 1) {
                        isConfigPathValid = true;
                    }
                }

                return isConfigPathValid ? "" : "The provided 'CONFIGURATION' line is invalid";
            }
        }

        /// <summary>
        /// Checks if a provided file content contains a valid number of tasks
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="TANFileContent">Content of a file</param>
        public static string ContainsValidTasksLine(string TANFileContent)
        {
            using (StringReader reader = new StringReader(TANFileContent)) {
                bool isTasksLineValid = false;
                string line;
                Regex rx = new Regex(@"TASKS,\d");
                while ((line = reader.ReadLine()) != null && !isTasksLineValid) {
                    line = line.Trim();
                    MatchCollection matches = rx.Matches(line);

                    if (matches.Count == 1) {
                        isTasksLineValid = true;
                    }
                }

                return isTasksLineValid ? "" : "The provided 'TASKS' line is invalid";
            }
        }

        /// <summary>
        /// Checks if a provided file content contains a valid number of processors
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="TANFileContent">Content of a file</param>
        public static string ContainsValidProcessorsLine(string TANFileContent)
        {
            using (StringReader reader = new StringReader(TANFileContent)) {
                bool isAllocationsLineValid = false;
                string line;
                Regex rx = new Regex(@"PROCESSORS,\d");
                while ((line = reader.ReadLine()) != null && !isAllocationsLineValid) {
                    line = line.Trim();
                    MatchCollection matches = rx.Matches(line);

                    if (matches.Count == 1) {
                        isAllocationsLineValid = true;
                    }
                }

                return isAllocationsLineValid ? "" : "The provided 'PROCESSORS' line is invalid";
            }
        }

        /// <summary>
        /// Checks if a provided file content contains a valid number of allocations
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="TANFileContent">Content of a file</param>
        public static string ContainsValidAllocationsLine(string TANFileContent)
        {
            using (StringReader reader = new StringReader(TANFileContent)) {
                bool isAllocationsLineValid = false;
                string line;
                Regex rx = new Regex(@"ALLOCATIONS,\d");
                while ((line = reader.ReadLine()) != null && !isAllocationsLineValid) {
                    line = line.Trim();
                    MatchCollection matches = rx.Matches(line);

                    if (matches.Count == 1) {
                        isAllocationsLineValid = true;
                    }
                }

                return isAllocationsLineValid ? "" : "The provided 'ALLOCATIONS' line is invalid";
            }
        }

        /// <summary>
        /// Checks if a provided file content contains a valid definition of all allocations
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="TANFileContent">Content of a file</param>
        public static string ContainsValidAllocations(string TANFileContent)
        {
            string errorMsg = "";
            int allocationConfigCount = 0;
            int totalAllocationCount = 0;

            using (StringReader reader = new StringReader(TANFileContent)) {
                string line;
                Regex totalAllocationCountRx = new Regex(@"ALLOCATIONS,(\d)");
                Regex singleAllocationRx = new Regex(@"ALLOCATION-ID,(\d)");
                
                while ((line = reader.ReadLine()) != null) {
                    line = line.Trim();
                    Match totalAllocationCountMatch = totalAllocationCountRx.Match(line);
                    Match singleAllocationMatch = singleAllocationRx.Match(line);

                    if (totalAllocationCountMatch.Success) {
                        totalAllocationCount = UInt16.Parse(totalAllocationCountMatch.Groups[1].Value);
                    }
                    else if (singleAllocationMatch.Success) {
                        allocationConfigCount++;
                    
                        // Read tasks per processor and check validity
                        string allocationId = singleAllocationMatch.Groups[1].Value;
                        List<string> processors = new List<string>();
                        while ((line = reader.ReadLine()) != null && Allocation.IsProcessorLine(line)) {
                            processors.Add(line);
                        }
                        Allocation allocation = new Allocation(allocationId, processors);
                        if (!allocation.IsAllocationValid()) {
                            return AllocationErrors["InvalidTaskAllocation"];
                        }
                    }
                }
            }

            if (allocationConfigCount != totalAllocationCount) {    
                return TaskAllocationFileValidator.AllocationErrors["WrongAllocationCount"];
            }

            return errorMsg;
        }

        /// <summary>
        /// Checks validity of a provided task allocation file
        /// </summary>
        /// <returns>
        /// A list of all error messages
        /// </returns>
        /// <param name="TANFileContent">Content of a file</param>
        public static List<string> ValidateAll(string TANFileContent) {
            List<string> errorsList = new List<string>();

            foreach (var taskAllocationFileValidator in validationMethods) {
                string errorMsg = taskAllocationFileValidator(TANFileContent);

                if (errorMsg != "") {
                    errorsList.Add(errorMsg);
                }
            }

            return errorsList;
        }

    }
}