using CloudApplicationDevTask1.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.Parsers
{
    public static class ConfigFileParser
    {
        public static List<float> GetCoefficients(string fileContent)
        {
            Regex coefficientsRx = new Regex(@"COEFFICIENT-ID,VALUE(?:\n|\r|\r\n)(?:\s*\d+,(-?\d+(?:\.?\d+)?)(?:\n|\r|\r\n)?)+");
            List<string> coefficients = FileParser.GetCaptureValues(fileContent, coefficientsRx);
            return ListConverter.StringToFloat(coefficients);
        }

        public static List<float> GetProcessorFrequencies(string fileContent)
        {
            Regex frequenciesRx = new Regex(@"PROCESSOR-ID,FREQUENCY(?:\n|\r|\r\n)(?:\s*\d+,(\d+(?:\.?\d+)?)(?:\n|\r|\r\n)?)+");
            List<string> frequencies = FileParser.GetCaptureValues(fileContent, frequenciesRx);

            return ListConverter.StringToFloat(frequencies);
        }

        public static List<string> GetProcessorIds(string fileContent)
        {
            Regex processorIdsRx = new Regex(@"PROCESSOR-ID,FREQUENCY(?:\n|\r|\r\n)(?:\s*(\d+),\d+\.\d+(?:\n|\r|\r\n)?)+");
            return FileParser.GetCaptureValues(fileContent, processorIdsRx);
        }

        public static List<float> GetTaskRuntimes(string fileContent)
        {
            Regex runtimesRx = new Regex(@"TASK-ID,RUNTIME(?:\n|\r|\r\n)(?:\s*\d+,(\d+(?:\.\d+)?)(?:\n|\r|\r\n)?)+");
            List<string> runtimes = FileParser.GetCaptureValues(fileContent, runtimesRx);

            return ListConverter.StringToFloat(runtimes);
        }

        public static float GetRuntimeReferenceFrequency(string fileContent)
        {
            Regex runtimeReferenceFrequencyRx = new Regex(@"RUNTIME-REFERENCE-FREQUENCY,(\d+(?:\.\d+)?)");
            List<string> captureValues = FileParser.GetCaptureValues(fileContent, runtimeReferenceFrequencyRx);

            if (captureValues.Count != 0) {
                return float.Parse(captureValues.First());
            }

            return -1;
        }
    }
}
