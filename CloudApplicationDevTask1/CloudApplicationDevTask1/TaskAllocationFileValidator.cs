using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CloudApplicationDevTask1
{

    public class TaskAllocationFileValidator
    {
        public static Dictionary<string, string> AllocationErrors = new Dictionary<string, string>(){
            {"WrongAllocationCount", "The number of allocations is not correct" }
        };

        // Mixing data and a comment on one line is not allowed
        public static string DataMixedWithComments(string fileContent)
        {
            using (StringReader reader = new StringReader(fileContent)) {
                string errorMsg = "";
                string line;
                while ((line = reader.ReadLine()) != null && errorMsg == "") {
                    line = line.Trim();
                    if (!line.StartsWith("//") && line.Contains("//")) {
                        errorMsg = "Mixing data and a comment on one line is not allowed";
                    }
                }
     
                return errorMsg;
            }
        }

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
            int allocationCount = 0;

            using (StringReader reader = new StringReader(fileContent)) {
                string line;
                Regex allocationCountRx = new Regex(@"ALLOCATIONS,(\d)");
                Regex allocationConfigCountRx = new Regex(@"ALLOCATION-ID,\d");
                while ((line = reader.ReadLine()) != null) {
                    line = line.Trim();
                    Match allocationCountMatch = allocationCountRx.Match(line);
                    Match allocationConfigCountMatch = allocationConfigCountRx.Match(line);

                    if (allocationCountMatch.Success) {
                        allocationCount = UInt16.Parse(allocationCountMatch.Groups[1].Value);
                    }
                    else if (allocationConfigCountMatch.Success) {
                        allocationConfigCount++;
                    }
                }
            }

            if (allocationConfigCount != allocationCount) {
                return TaskAllocationFileValidator.AllocationErrors["WrongAllocationCount"];
            }

            return errorMsg;
        }
    }
}