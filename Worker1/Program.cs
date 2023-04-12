using DotNetCore.CAP;
using NATS.Client.JetStream;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using Worker1;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddCap(capOptions =>
        {
            capOptions.UseInMemoryStorage();

            capOptions.UseNATS(natsOptions =>
            {
                natsOptions.Servers = "nats://1.1.1.1:31222"; //your nats server
            });
        });
    })
    .Build();

await host.Services.GetService<IBootstrapper>().BootstrapAsync();
host.Run();
