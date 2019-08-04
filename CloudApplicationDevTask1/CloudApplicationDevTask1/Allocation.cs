using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1
{
    class Allocation
    {
        private char TASK_DELIMITER = ',';
        private string id;
        private List<List<bool>> processors;
        private static Regex processorLineRx = new Regex(@"([01],?)+");

        public Allocation(string id, List<string> processors)
        {
            this.id = id;
            this.processors = new List<List<bool>>();
            processors.ForEach((string processorLine) =>
            {
                List<string> tasks = new List<string>(processorLine.Split(TASK_DELIMITER));
                List<bool> tasksPerProcessor = tasks.ConvertAll((string task) =>
                {
                    return task.Trim() == "1";
                });
                this.processors.Add(tasksPerProcessor);
            });
        }

        public static bool IsProcessorLine(string line)
        {
            Match processorLineMatch = processorLineRx.Match(line);
            return processorLineMatch.Success;
        }

        public bool IsAllocationValid()
        {
            if (this.processors.Count == 0) {
                return false;
            }

            int tasksAllocated = 0;

            this.processors.ForEach((List<bool> tasksPerProcessor) =>
            {
                tasksPerProcessor.ForEach((bool task) => {
                    tasksAllocated += task ? 1 : 0;
                });
            });

            return tasksAllocated == this.processors.First().Count;
        }
    }
}
