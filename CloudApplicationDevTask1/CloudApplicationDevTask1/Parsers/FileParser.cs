using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.Parsers
{
    // Contains common methods for text parsing
    public static class FileParser
    {
        public static string NewLineRx = "\n|\r|\r\n";

        /// <summary>
        /// Checks if a provided text contains a given regex
        /// </summary>
        /// <returns>
        /// true if a match is found
        /// </returns>
        /// <param name="fileContent">Content of a file to be tested against a provided regular expression</param>
        /// <param name="rx">Regular expression to be used for searching in a provided text</param>
        public static bool ContainsRegex(string fileContent, Regex rx)
        {
            Match match = rx.Match(fileContent);
            return match.Success;
        }

        /// <summary>
        /// Finds a match in a provided text with a given regex
        /// </summary>
        /// <returns>
        /// a found match
        /// </returns>
        /// <param name="fileContent">Content of a file to be tested against a provided regular expression</param>
        /// <param name="rx">Regular expression to be used for searching in a provided text</param>
        public static Match GetRegexMatch(string fileContent, Regex rx)
        {
            Match match = rx.Match(fileContent);
            return match;
        }

        /// <summary>
        /// Gets capture values in a provided text with a given regex
        /// </summary>
        /// <returns>
        /// Capture values found in the text
        /// </returns>
        /// <param name="fileContent">Content of a file to be tested against a provided regular expression</param>
        /// <param name="rx">Regular expression to be used for searching in a provided text</param>
        public static List<string> GetCaptureValues(string fileContent, Regex rx)
        {
            Match match = GetRegexMatch(fileContent, rx);
            return GetCaptureValuesFromMatch(match);
        }

        /// <summary>
        /// Gets capture values from a given match
        /// </summary>
        /// <returns>
        /// Capture values found in a match
        /// </returns>
        /// <param name="match">A match with capture values</param>
        public static List<string> GetCaptureValuesFromMatch(Match match)
        {
            List<string> captureValues = new List<string>();

            if (match.Success)
            {
                for (int captureGroupIndex = 1; captureGroupIndex < match.Groups.Count; captureGroupIndex++)
                {
                    foreach (Capture capture in match.Groups[captureGroupIndex].Captures)
                    {
                        captureValues.Add(capture.Value);
                    }
                }
            }

            return captureValues;
        }

    }
}
