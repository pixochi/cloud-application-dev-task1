using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CloudApplicationDevTask1
{
    // Validator for rules common to both configuration and task allocation files
    public class FileValidator
    {
        /// <summary>
        /// Checks if a provided file content contains data mixed with comments
        /// </summary>
        /// <returns>
        /// An empty string if it is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="fileContent">Content of a file</param>
        public static string DataMixedWithComments(string fileContent)
        {
            using (StringReader reader = new StringReader(fileContent))
            {
                string errorMsg = "";
                string line;
                while ((line = reader.ReadLine()) != null && errorMsg == "")
                {
                    line = line.Trim();
                    if (!line.StartsWith("//") && line.Contains("//"))
                    {
                        errorMsg = "Mixing data and a comment on one line is not allowed";
                    }
                }

                return errorMsg;
            }
        }
    }
}