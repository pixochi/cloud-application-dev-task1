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
        public static bool ContainsRegex(string fileContent, Regex rx)
        {
            Match match = rx.Match(fileContent);
            return match.Success;
        }

        /// <summary>
        /// Finds a match in a provided text with a given regex
        /// </summary>
        public static Match GetRegexMatch(string fileContent, Regex rx)
        {
            Match match = rx.Match(fileContent);
            return match;
        }

        /// <summary>
        /// Gets capture values in a provided text with a given regex
        /// </summary>
        public static List<string> GetCaptureValues(string fileContent, Regex rx)
        {
            Match match = GetRegexMatch(fileContent, rx);
            return GetCaptureValuesFromMatch(match);
        }

        /// <summary>
        /// Gets capture values from a given match
        /// </summary>
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
