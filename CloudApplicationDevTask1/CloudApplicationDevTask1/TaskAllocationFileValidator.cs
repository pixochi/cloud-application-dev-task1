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
                while ((line = reader.ReadLine()) != null && errorMsg == "") {
                    line = line.Trim();
                    if (!line.StartsWith("//") && line.Contains("//")) {
                        errorMsg = "Mixing data and a comment on one line is not allowed";
                    }
                }
     
                return errorMsg;
            }
        }
    }
}