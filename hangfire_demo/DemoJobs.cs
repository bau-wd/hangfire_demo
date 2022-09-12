using System.Collections;
using Hangfire;
using Hangfire.Console;
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

            // save state in db

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

        public async Task HangfireConsole(PerformContext performContext)
        {
            performContext.WriteLine("Start LongRunningJob");
            var progressBar = performContext.WriteProgressBar("custom progress bar");

            // long running job, e.g. database cleanup
            for (int i = 0; i < 100; i++)
            {
                performContext.WriteLine("do sth");
                await Task.Delay(1000);
                // update value for previously created progress bar
                progressBar.SetValue(i+1);
            }

            performContext.WriteLine("Finished LongRunningJob");
        }

        public async Task HangfireConsoleEnumerationExtension(PerformContext performContext)
        {
            Console.WriteLine("Start LongRunningJob (with cancellationToken)");
            var progressBar = performContext.WriteProgressBar("custom progress bar");

            // long running job, e.g. database cleanup

            var mySequence = Enumerable.Range(0, 20);

            foreach (var item in mySequence.WithProgress(progressBar))
            {
                performContext.WriteLine($"do sth ({item})");
                await Task.Delay(1000);
            }
            
            Console.WriteLine("Finished LongRunningJob (with cancellationToken)");
        }


       
        public async Task ExceptionJob(PerformContext performContext)
        {
            Console.WriteLine("Start LongRunningJob");
            var progressBar = performContext.WriteProgressBar("custom progress bar");

          
            throw new Exception("this is not good");
            
            // long running job, e.g. database cleanup

            var mySequence = Enumerable.Range(0, 20).ToList();

            foreach (var item in mySequence.WithProgress(progressBar))
            {
                performContext.WriteLine($"do sth ({item})");
                await Task.Delay(1000);
            }

            Console.WriteLine("Finished LongRunningJob");
        }


        [Queue("{0}")]
        public async Task QueueExampleJob(string queueName) // check queue name constraints
        {
           
            // long running job, e.g. database cleanup

            var mySequence = Enumerable.Range(0, 10).ToList();

            foreach (var item in mySequence)
            {
                await Task.Delay(1000);
            }

            Console.WriteLine("Finished LongRunningJob (with cancellationToken)");
        }

        public async Task RecurringJob()
        {

            // long running job, e.g. database cleanup
            var mySequence = Enumerable.Range(0, 10).ToList();

            foreach (var item in mySequence)
            {
                await Task.Delay(1000);
            }
            
        }

    }
}
