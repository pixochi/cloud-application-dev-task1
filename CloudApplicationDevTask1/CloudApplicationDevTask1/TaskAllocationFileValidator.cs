using System;
using System.IO;

namespace CloudApplicationDevTask1
{
    public class TaskAllocationFileValidator
    {
        public static string DataMixedWithComments(string fileContent)
        {
            using (StringReader reader = new StringReader(fileContent)) {
                string errorMsg = "";
                string line;
                while ((line = reader.ReadLine()) != null) {
                    Console.WriteLine(line);
                    errorMsg = line;
                }
     
                return errorMsg;
            }
        }
    }
}