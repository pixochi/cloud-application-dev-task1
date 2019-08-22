using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.Parsers
{
    public static class FileParser
    {
        public static bool ContainsRegex(string fileContent, Regex rx)
        {
            Match match = rx.Match(fileContent);
            return match.Success;
        }

        public static Match GetRegexMatch(string fileContent, Regex rx)
        {
            Match match = rx.Match(fileContent);
            return match;
        }

        public static List<string> GetCaptureValues(string fileContent, Regex rx)
        {
            List<string> captureValues = new List<string>();
            Match match = GetRegexMatch(fileContent, rx);

            if (match.Success) {
                for (int captureGroupIndex = 1; captureGroupIndex < match.Groups.Count; captureGroupIndex++) {
                    foreach (Capture capture in match.Groups[captureGroupIndex].Captures) {
                        captureValues.Add(capture.Value);
                    }
                }
            }

            return captureValues;
        }
    }
}
