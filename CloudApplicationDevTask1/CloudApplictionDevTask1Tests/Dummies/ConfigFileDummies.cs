using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApplictionDevTask1Tests.Dummies
{
    class ConfigFileDummies
    {
        public static string ValidLogFilePath()
        {
            return @"
                // The default log file name.
                DEFAULT-LOGFILE,""AT1 - log.txt""
            ";
        }

        public static string InvalidLogFilePath()
        {
            return @"
                Log file path is missing
                in this 'file content'
            ";
        }

        public static string ValidLimitSection()
        {
            return @"
                // Minimum and maximum limits on the number of
                // tasks and processors and processor frequencies.
                LIMITS-TASKS,1,500
                LIMITS-PROCESSORS,1,1000
                LIMITS-PROCESSOR-FREQUENCIES,0,10
            ";
        }


        public static string InvalidLimitSection()
        {
            return @"
                LIMITS-TASKS,1,500
                missing limits for processors
                LIMITS-PROCESSOR-FREQUENCIES,0,10
            ";
        }

        public static string ValidParallelSection()
        {
            return @"
                PROGRAM-MAXIMUM-DURATION,9
                PROGRAM-TASKS,5
                PROGRAM-PROCESSORS,3
            ";
        }
        public static string InvalidParallelSection()
        {
            return @"
                missing max duration
                PROGRAM-TASKS,5
                PROGRAM-PROCESSORS,3
            ";
        }

        public static string ValidFrequency()
        {
            return @"
                // Task runtimes are based on tasks executing on
                // a processor running at the following frequency (GHz).
                RUNTIME-REFERENCE-FREQUENCY,2
            ";
        }

        public static string InvalidFrequency()
        {
            return @"
                Config without frequency
            ";
        }

        public static string ValidRuntimes()
        {
            return @"   
                // Task IDs and their runtime values.
                TASK-ID,RUNTIME
                1,1
                2,3
                3,5
                4,7
                5,9
            ";
        }

        public static string InvalidRuntimes()
        {
            return @"   
                // Task IDs and their runtime values.
                TASK-ID,RUNTIME
                something else
            ";
        }

        public static string InvalidRuntimesIds()
        {
            return @"   
                // The task with id 1 is specified twice
                TASK-ID,RUNTIME
                1,1
                1,3
                3,5
                4,7
                5,9
            ";
        }
    }
}
