using System.Collections;
using Hangfire;
using Hangfire.Server;

namespace hangfire_demo
{
    public class DemoJobs 
    {

        public DemoJobs()
        {
        }
        
        public async Task SendEmail(long id)
        {
            Console.WriteLine("Start Email Send Job");

            // fetch data from db
            // send email / call api
            Console.WriteLine($"Send Email with Id {id}");
            await Task.Delay(500);

            Console.WriteLine("Finished Email Send Job");
        }

        public async Task LongRunningJob()
        {
            Console.WriteLine("Start LongRunningJob");

            // long running job, e.g. database cleanup
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("do sth (long running job without cToken)");
                await Task.Delay(1000);
            }

            Console.WriteLine("Finished LongRunningJob");
        }

        public async Task LongRunningJob(CancellationToken cancellationToken)
        {
            Console.WriteLine("Start LongRunningJob (with cancellationToken)");

            // long running job, e.g. database cleanup
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("do sth (long running job with cToken)");
                await Task.Delay(1000, cancellationToken);
            }

            Console.WriteLine("Finished LongRunningJob (with cancellationToken)");
        }

    }
}
