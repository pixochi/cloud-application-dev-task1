using CloudApplicationDevTask1.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.Parsers
{
    // Parser for a configuration text file
    public static class ConfigFileParser
    {
        /// <summary>
        /// Extracts coefficient values
        /// </summary>
        public static List<float> GetCoefficients(string configFileContent)
        {
            Regex coefficientsRx = new Regex($@"COEFFICIENT-ID,VALUE(?:{FileParser.NewLineRx})(?:\s*\d+,(-?\d+(?:\.?\d+)?)(?:{FileParser.NewLineRx})?)+");
            List<string> coefficients = FileParser.GetCaptureValues(configFileContent, coefficientsRx);
            return ListConverter.StringToFloat(coefficients);
        }

        /// <summary>
        /// Extracts frequencies of provided processors
        /// </summary>
        public static List<float> GetProcessorFrequencies(string configFileContent)
        {
            Regex frequenciesRx = new Regex($@"PROCESSOR-ID,FREQUENCY(?:{FileParser.NewLineRx})(?:\s*\d+,(\d+(?:\.?\d+)?)(?:{FileParser.NewLineRx})?)+");
            List<string> frequencies = FileParser.GetCaptureValues(configFileContent, frequenciesRx);

            return ListConverter.StringToFloat(frequencies);
        }

        /// <summary>
        /// Extracts ids of provided processors
        /// </summary>
        public static List<string> GetProcessorIds(string configFileContent)
        {
            Regex processorIdsRx = new Regex($@"PROCESSOR-ID,FREQUENCY(?:{FileParser.NewLineRx})(?:\s*(\d+),\d+\.\d+(?:{FileParser.NewLineRx})?)+");
            return FileParser.GetCaptureValues(configFileContent, processorIdsRx);
        }

        /// <summary>
        /// Extracts runtimes of each task
        /// </summary>
        public static List<float> GetTaskRuntimes(string configFileContent)
        {
            Regex runtimesRx = new Regex($@"TASK-ID,RUNTIME(?:{FileParser.NewLineRx})(?:\s*\d+,(\d+(?:\.\d+)?)(?:{FileParser.NewLineRx})?)+");
            List<string> runtimes = FileParser.GetCaptureValues(configFileContent, runtimesRx);

            return ListConverter.StringToFloat(runtimes);
        }

        /// <summary>
        /// Extracts a reference frequency used to determine runtime of each task
        /// </summary>
        /// <returns>
        /// Reference frequency or -1 if it is not provided in a configuration file
        /// </returns>
        public static float GetRuntimeReferenceFrequency(string configFileContent)
        {
            Regex runtimeReferenceFrequencyRx = new Regex(@"RUNTIME-REFERENCE-FREQUENCY,(\d+(?:\.\d+)?)");
            List<string> captureValues = FileParser.GetCaptureValues(configFileContent, runtimeReferenceFrequencyRx);

            if (captureValues.Count != 0)
            {
                return float.Parse(captureValues.First());
            }

            return -1;
        }

        /// <summary>
        /// Extracts the maximum program duration
        /// </summary>
        /// <returns>
        /// Reference the maximum duration or -1 if it is not provided in a configuration file
        /// </returns>
        public static float GetMaximumDuration(string configFileContent)
        {
            Regex maximumDurationRx = new Regex(@"PROGRAM-MAXIMUM-DURATION,(\d+(?:\.\d+)?)");
            List<string> captureValues = FileParser.GetCaptureValues(configFileContent, maximumDurationRx);

            if (captureValues.Count != 0)
            {
                return float.Parse(captureValues.First());
            }

            return -1;
        }
    }
}
