using System;
using System.IO;
using System.Text.RegularExpressions;

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

                return isConfigPathValid ? "" : "The provided CONFIGURATION is invalid";
            }
        }
    }
}