using MassTransit;
using ProjectName.ServiceName.Domain.Events;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((host, log) =>
{
    if (host.HostingEnvironment.IsProduction())
        log.MinimumLevel.Information();
    else
        log.MinimumLevel.Debug();

    log.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
    log.MinimumLevel.Override("Quartz", LogEventLevel.Information);
    log.WriteTo.Console();
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host
    .UseMassTransit((hostContext, x) =>
    {
        //x.AddMongoDbConfiguration(hostContext.Configuration);

        x.UsingInMemory();

        x.AddRider(r =>
        {
            r.AddProducer<string, TestEvent>("test-topic", (context, cfg) =>
            {
                // Configure the AVRO serializer, with the schema registry client
                //cfg.SetValueSerializer(new AvroSerializer<ShipmentManifestReceived>(context.GetRequiredService<ISchemaRegistryClient>()));
            });

            r.UsingKafka((context, cfg) =>
            {
                cfg.ClientId = "TestKafkaApi";
                cfg.Host("localhost:9094");
            });
        });
    });
var app = builder.Build();

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
