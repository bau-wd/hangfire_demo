using Hangfire;
using Hangfire.Common;

namespace hangfire_demo
{
    public static class ConfigureRecurringJobs
    {

        public static void Configure(IConfiguration configuration)
        {
            RecurringJob.AddOrUpdate(JobIds.RecuringJob, () => new DemoJobs().RecurringJob(),
               "0 0 1 1 2", TimeZoneInfo.Local);
        }
    }

    public static class JobIds
    {
        public const string RecuringJob = "RecuringJob";
    }

    public static class WorkerQueues
    {
        public const string Default = "default";
    }
}
