using Hangfire;
using Hangfire.Console;
using Hangfire.SqlServer;
using hangfire_demo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5), // use higher value for huge workloads!
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(1),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    })
    .UseConsole() // used by hangfire.console
);

// Add the processing server as IHostedService
//builder.Services.AddHangfireServer();


builder.Services.AddHangfireServer(options =>
{
    options.Queues = new[] { "default", "test_queue" };
    options.WorkerCount = 1;
});


builder.Services.AddScoped<DemoJobs, DemoJobs>();

var app = builder.Build();

app.UseHangfireDashboard();

ConfigureRecurringJobs.Configure(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
