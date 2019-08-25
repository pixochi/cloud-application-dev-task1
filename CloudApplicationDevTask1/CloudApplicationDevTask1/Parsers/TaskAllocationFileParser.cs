using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.Parsers
{
    // Parser for a task allocation text file
    public static class TaskAllocationFileParser
    {
        /// <summary>
        /// Extracts a name of configuration file
        /// </summary>
        /// <returns>
        /// Reference a name of configuration file
        /// or an empty string if it is not provided in a configuration file
        /// </returns>
        /// <param name="TANFileContent">Content of a task allocation file</param>
        public static string GetConfigFilename(string TANFileContent)
        {
            Regex rx = new Regex(@"CONFIGURATION,""(.*\.csv)""");
            Match match = FileParser.GetRegexMatch(TANFileContent, rx);

            if (!match.Success)
            {
                return "";
            }
            else
            {
                return match.Groups[1].Captures[0].Value;
            }
        }

        /// <summary>
        /// Extracts all allocations
        /// </summary>
        /// <returns>
        /// All allocations found in a file content
        /// </returns>
        /// <param name="TANFileContent">Content of a task allocation file</param>
        public static Dictionary<string, List<List<bool>>> GetAllocations(string TANFileContent)
        {
            var allocations = new Dictionary<string, List<List<bool>>>();
            Regex allocationsRx = new Regex($@"ALLOCATION-ID,\d+(?:{FileParser.NewLineRx})(?:\s*((?:(?:0|1),?)+)(?:{FileParser.NewLineRx})?)+");
            MatchCollection allocationMatches = allocationsRx.Matches(TANFileContent);
            Console.WriteLine(allocationMatches);
            
            foreach (Match allocationMatch in allocationMatches)
            {
                List<string> allocationProcessors = FileParser.GetCaptureValuesFromMatch(allocationMatch);
                List<List<bool>> allocation = new List<List<bool>>();

                foreach (var processor in allocationProcessors)
                {
                    List<bool> processorTasks = new List<bool>();
                    string[] tasksOnProcessor = processor.Split(',');

                    foreach (var task in tasksOnProcessor)
                    {    
                        processorTasks.Add(task == "1");
                    }

                    allocation.Add(processorTasks);
                }

                string allocationId = (allocations.Count + 1).ToString();
                allocations.Add(allocationId, allocation);
            }

            return allocations;
        }
    }
}
