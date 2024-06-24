using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Orchestration
{
    public static class Activities
    {
        [Function(nameof(PerformTask))]
        public static string PerformTask([ActivityTrigger] string taskName, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("PerformTask");
            logger.LogInformation($"Performing task: {taskName}");

            // Simulate task execution
            return $"Task {taskName} completed.";
        }
    }
}
