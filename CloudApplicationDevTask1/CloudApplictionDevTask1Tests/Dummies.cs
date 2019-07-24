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
    }
}
