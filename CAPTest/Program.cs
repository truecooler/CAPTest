using NATS.Client;
using NATS.Client.JetStream;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using static NATS.Client.JetStream.PublishOptions;

var task = Task.CompletedTask;
task.Wait(2000);

var opts =  ConnectionFactory.GetDefaultOptions();
opts.Url ??= "nats://167.71.7.187:31222";
opts.ClosedEventHandler = ConnectedEventHandler;
opts.DisconnectedEventHandler = ConnectedEventHandler;
opts.AsyncErrorEventHandler = AsyncErrorEventHandler;
opts.Timeout = 5000;


IConnection c = new ConnectionFactory().CreateConnection(opts);

var stream = "lolka";
var info = StreamConfiguration.Builder()
                    .WithName(stream)
                    .WithSubjects(stream)
                    //.WithReplicas(3)
                    .WithStorageType(StorageType.Memory)
.Build();

//c.CreateJetStreamManagementContext().AddStream(info);


EventHandler<MsgHandlerEventArgs> h = (sender, args) =>
{
    // print the message
    Console.WriteLine(args.Message);

    // Here are some of the accessible properties from
    // the message:
    // args.Message.Data;
    // args.Message.Reply;
    // args.Message.Subject;
    // args.Message.ArrivalSubcription.Subject;
    // args.Message.ArrivalSubcription.QueuedMessageCount;
    // args.Message.ArrivalSubcription.Queue;

    // Unsubscribing from within the delegate function is supported.
    args.Message.ArrivalSubcription.Unsubscribe();
};
// The simple way to create an asynchronous subscriber
// is to simply pass the event in.  Messages will start
// arriving immediately.

IJetStream js = c.CreateJetStreamContext();


js.PushSubscribeAsync(stream, h, true).Start();
await Task.Delay(1000);

PublishOptionsBuilder builder = PublishOptions.Builder()
          .WithExpectedStream(stream);
await js.PublishAsync(stream, Encoding.UTF8.GetBytes("euebok2"), builder.Build());

Console.ReadLine();


void ConnectedEventHandler(object? sender, ConnEventArgs e)
{
    Console.WriteLine(e.Error);
}

 void AsyncErrorEventHandler(object? sender, ErrEventArgs e)
{
    Console.WriteLine(e.Error);
}
