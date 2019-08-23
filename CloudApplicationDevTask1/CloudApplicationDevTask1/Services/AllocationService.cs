using CloudApplicationDevTask1.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.Services
{
    public static class AllocationService
    {
        public static float GetEnergyConsumedPerTask(List<float> coefficients, float frequency, float runtime)
        {
            return (coefficients[2] * frequency * frequency + coefficients[1] * frequency + coefficients[0]) * runtime;
        }

        public static float GetTotalEnergyConsumed(string configFileContent, List<List<bool>> allocation)
        {
            List<float> coefficients = ConfigFileParser.GetCoefficients(configFileContent);
            List<float> processorFrequencies = ConfigFileParser.GetProcessorFrequencies(configFileContent);
            List<float> taskRuntimes = ConfigFileParser.GetTaskRuntimes(configFileContent);
            float runtimeReferenceFrequency = ConfigFileParser.GetRuntimeReferenceFrequency(configFileContent);
            float totalEnergyConsumed = 0;

            if (coefficients.Count == 0 || processorFrequencies.Count == 0 || taskRuntimes.Count == 0 || allocation.Count == 0 || runtimeReferenceFrequency == -1) {
                return -1;
            }
            else {
                // Calculate energy consumed by each task
                for (int taskId = 0; taskId < taskRuntimes.Count; taskId++) {
                    int processorId = 0;

                    // find the processor that handles a given task
                    for (int allocationProcessorId = 0; allocationProcessorId < allocation.Count; allocationProcessorId++) {
                        if (allocation[allocationProcessorId][taskId]) {
                            processorId = allocationProcessorId;
                            break;
                        }
                    }

                    float runtimeOnGivenProcessor = GetTaskRuntime(runtimeReferenceFrequency, taskRuntimes[taskId], processorFrequencies[processorId]);
                    float energyConsumedPerTask = GetEnergyConsumedPerTask(coefficients, processorFrequencies[processorId], runtimeOnGivenProcessor);
                    totalEnergyConsumed += energyConsumedPerTask;
                }

                return totalEnergyConsumed;
            }
        }

        public static float GetTaskRuntime(float referenceFrequency, float runtime, float frequency)
        {
            return runtime * (referenceFrequency / frequency);
        }

        public static float GetAllocationRuntime(string configFileContent, List<List<bool>> allocation)
        {
            List<float> processorFrequencies = ConfigFileParser.GetProcessorFrequencies(configFileContent);
            List<float> taskRuntimes = ConfigFileParser.GetTaskRuntimes(configFileContent);
            float runtimeReferenceFrequency = ConfigFileParser.GetRuntimeReferenceFrequency(configFileContent);
            float allocationRuntime = 0;

            if (processorFrequencies.Count == 0 || taskRuntimes.Count == 0 || allocation.Count == 0 || runtimeReferenceFrequency == -1) {
                return -1;
            }
            else
            {
                for (int processorId = 0; processorId < allocation.Count; processorId++)
                {
                    float processorRuntime = 0;

                    for (int taskId = 0; taskId < allocation[processorId].Count; taskId++)
                    {
                        if (allocation[processorId][taskId])
                        {
                            processorRuntime += GetTaskRuntime(runtimeReferenceFrequency, taskRuntimes[taskId], processorFrequencies[processorId]);
                        }
                    }

                    if (processorRuntime >= allocationRuntime)
                    {
                        allocationRuntime = processorRuntime;
                    }
                }

                return allocationRuntime;
            }
        }

        public static string IsAllocationRuntimeValid(string configFileContent, float allocationRuntime)
        {
            string errorMsg = "Runtime of the given allocation exceeds maximum duration of the program.";
            float maxDuration = ConfigFileParser.GetMaximumDuration(configFileContent);
    
            return allocationRuntime <= maxDuration ? "" : errorMsg;
        }
    }
}
