using CloudApplicationDevTask1.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CloudApplicationDevTask1
{

    public class TaskAllocationFileValidator: FileValidator 
    {
        delegate string ValidationFunction(string fileContent);
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

        // There will be a line containing the file name of a configuration file.
        // It commences with the keyword CONFIGURATION, followed by a comma,
        // and ends with the filename that is delimited by double quotes.
        public static string ContainsValidConfigPath(string fileContent)
        {
            using (StringReader reader = new StringReader(fileContent)) {
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

        // There will be a line containing the number of program tasks.
        // It commences with the keyword TASKS, followed by a comma,
        // and ends with a number.
        public static string ContainsValidTasksLine(string fileContent)
        {
            using (StringReader reader = new StringReader(fileContent)) {
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

        // There will be a line containing the number of processors.
        // It commences with the keyword PROCESSORS, followed by a comma,
        // and ends with a number.  
        public static string ContainsValidProcessorsLine(string fileContent)
        {
            using (StringReader reader = new StringReader(fileContent)) {
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

        // There will be a line containing the number of allocations.
        // It commences with the keyword ALLOCATIONS, followed by a comma,
        // and ends with a number.
        public static string ContainsValidAllocationsLine(string fileContent)
        {
            using (StringReader reader = new StringReader(fileContent)) {
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

        // There will be a section of data for each allocation.
        // In general, each allocation commences an allocation ID
        // which is followed by a table representing the allocation
        // of 0 or more tasks to each processor.
        public static string ContainsValidAllocations(string fileContent)
        {
            string errorMsg = "";
            int allocationConfigCount = 0;
            int totalAllocationCount = 0;

            using (StringReader reader = new StringReader(fileContent)) {
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

        public static List<string> ValidateAll(string fileContent) {
            List<string> errorsList = new List<string>();

            foreach (var taskAllocationFileValidator in validationMethods) {
                string errorMsg = taskAllocationFileValidator(fileContent);

                if (errorMsg != "") {
                    errorsList.Add(errorMsg);
                }
            }

            return errorsList;
        }

    }
}