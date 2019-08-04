using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApplictionDevTask1Tests
{
    class Dummies
    {
        public static string DataMixedWithComments()
        {
            return @"
                TASKS,5 // The number of tasks
                PROCESSORS,3
            ";
        }

        public static string DataNotMixedWithComments()
        {
            return @"
                // The number of tasks
                TASKS,5
                PROCESSORS,3
            ";
        }

        public static string ValidConfigPath()
        {
            return @"
                CONFIGURATION,""C:\\temp\config.csv""
                TASKS,5
            ";
        }

        public static string InvalidConfigPath()
        {
            return @"
                CONFIGURATION
                TASKS,5
            ";
        }

        public static string ValidTasksLine()
        {
            return @"
                CONFIGURATION,""C:\\temp\config.csv""
                TASKS,5
            ";
        }

        public static string InvalidTasksLine()
        {
            return @"
                CONFIGURATION,""C:\\temp\config.csv""
                TASKS,with a missing number
            ";
        }

        public static string ValidProcessorsLine()
        {
            return @"
                TASKS,5
                PROCESSORS,3
            ";
        }

        public static string InvalidProcessorsLine()
        {
            return @"
                TASKS,5
                PROCESSORS,missing number
            ";
        }

        public static string ValidAllocationsLine()
        {
            return @"
                PROCESSORS,3
                ALLOCATIONS,8
            ";
        }

        public static string InvalidAllocationsLine()
        {
            return @"
                PROCESSORS,3
                ALLOCATIONS,missing number
            ";
        }

        public static string ValidAllocations()
        {
            return @"
                // The number of tasks and processors per allocation.
                TASKS,5
                PROCESSORS,3

                // The number of allocations in this file.
                ALLOCATIONS,2

                // The set of allocations.
                // The ith row is the allocation of tasks to the ith processor.
                // The jth column is the allocation of the jth task to a processor.
                ALLOCATION-ID,1
                1,1,0,0,0
                0,0,1,1,0
                0,0,0,0,1

                ALLOCATION-ID,2
                1,1,0,0,0
                0,0,0,0,1
                0,0,1,1,0
            ";

            
        }

        public static string InvalidAllocationCount()
        {
            return @"
                ALLOCATIONS,2

                ALLOCATION-ID,1
                1,1,0,0,0
                0,0,1,1,0
                0,0,0,0,1
            ";
        }

        public static string InvalidTaskAllocation()
        {
            // A task(T1) cannot be assigned to more than one processor(P1, P2)
            return @"
                ALLOCATIONS,1

                ALLOCATION-ID,1
                1,1,0,0,0
                1,0,1,1,0
                0,0,0,0,1
            ";
        }
    }
}
