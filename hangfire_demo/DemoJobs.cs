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

    }
}
