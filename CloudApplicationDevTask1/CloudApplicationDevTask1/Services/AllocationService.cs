using CloudApplicationDevTask1.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.Services
{
    // Service for calculating and validation of allocation related data
    public static class AllocationService
    {
        /// <summary>
        /// Computes the energy consumed by a task allocated to an NGHz processor
        /// </summary>
        /// <returns>
        /// The energy consumed by a task
        /// </returns>
        /// <param name="coefficients">Coefficients of a quadratic formulas</param>
        /// <param name="frequency">Processor frequency</param>
        /// <param name="runtime">Runtime of the task</param>
        public static float GetEnergyConsumedPerTask(List<float> coefficients, float frequency, float runtime)
        {
            return (coefficients[2] * frequency * frequency + coefficients[1] * frequency + coefficients[0]) * runtime;
        }

        /// <summary>
        /// Computes the energy consumed by an allocation
        /// </summary>
        /// <returns>
        /// The energy consumed by an allocation
        /// </returns>
        /// <param name="configFileContent">Content of a configuration file</param>
        /// <param name="allocation">Allocation of tasks on processors</param>
        public static float GetTotalEnergyConsumed(string configFileContent, List<List<bool>> allocation)
        {
            List<float> coefficients = ConfigFileParser.GetCoefficients(configFileContent);
            List<float> processorFrequencies = ConfigFileParser.GetProcessorFrequencies(configFileContent);
            List<float> taskRuntimes = ConfigFileParser.GetTaskRuntimes(configFileContent);
            float runtimeReferenceFrequency = ConfigFileParser.GetRuntimeReferenceFrequency(configFileContent);
            float totalEnergyConsumed = 0;

            if (coefficients.Count == 0 || processorFrequencies.Count == 0 || taskRuntimes.Count == 0 || allocation.Count == 0 || runtimeReferenceFrequency == -1)
            {
                return -1;
            }
            else
            {
                // Calculate energy consumed by each task
                for (int taskId = 0; taskId < taskRuntimes.Count; taskId++)
                {
                    int processorId = 0;

                    // Find the processor that handles a given task
                    for (int allocationProcessorId = 0; allocationProcessorId < allocation.Count; allocationProcessorId++)
                    {
                        if (allocation[allocationProcessorId][taskId])
                        {
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

        /// <summary>
        /// Computes the runtime of a task allocated to an NGHz processor
        /// </summary>
        /// <returns>
        /// The runtime of a task
        /// </returns>
        /// <param name="referenceFrequency">A frequency used to determine task runtimes in a configuration file</param>
        /// <param name="runtime">Runtime determined with the reference frequency</param>
        /// <param name="frequency">Frequency of an actual processor to which a task is assigned</param>
        public static float GetTaskRuntime(float referenceFrequency, float runtime, float frequency)
        {
            return runtime * (referenceFrequency / frequency);
        }

        /// <summary>
        /// Computes the runtime of an allocation
        /// </summary>
        /// <returns>
        /// The runtime of an allocation
        /// </returns>
        /// <param name="configFileContent">Content of a configuration file</param>
        /// <param name="allocation">A single allocation of tasks on processors</param>
        public static float GetAllocationRuntime(string configFileContent, List<List<bool>> allocation)
        {
            List<float> processorFrequencies = ConfigFileParser.GetProcessorFrequencies(configFileContent);
            List<float> taskRuntimes = ConfigFileParser.GetTaskRuntimes(configFileContent);
            float runtimeReferenceFrequency = ConfigFileParser.GetRuntimeReferenceFrequency(configFileContent);
            float allocationRuntime = 0;

            if (processorFrequencies.Count == 0 || taskRuntimes.Count == 0 || allocation.Count == 0 || runtimeReferenceFrequency == -1)
            {
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

        /// <summary>
        /// Checks if the runtime of an allocation exceeds the maximum program duration
        /// </summary>
        /// <returns>
        /// An empty string if the runtime is valid,
        /// an error message if invalid
        /// </returns>
        /// <param name="configFileContent">Content of a configuration file</param>
        /// <param name="allocationRuntime">Runtime of an allocation</param>
        public static string IsAllocationRuntimeValid(string configFileContent, float allocationRuntime)
        {
            string errorMsg = "Runtime of the given allocation exceeds maximum duration of the program.";
            float maxDuration = ConfigFileParser.GetMaximumDuration(configFileContent);
    
            return allocationRuntime <= maxDuration ? "" : errorMsg;
        }
    }
}
