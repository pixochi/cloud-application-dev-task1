using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CloudApplicationDevTask1
{
    // TODO: check not only necessary info, but everything that shouldn't be in the file???
    // Rules in the assignment: 1, 2, 3
    public class FileValidator
    {
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

        public static bool ContainsRegex(string fileContent, Regex rx)
        {
            Match match = rx.Match(fileContent);
            return match.Success;
        }
    }
}