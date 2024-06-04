using Data.Contexts;
using Data.GraphQL.ObjestTypes;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddPooledDbContextFactory<DataContext>(x =>
        {
            x.UseCosmos(Environment.GetEnvironmentVariable("Cosmos_Uri")!, Environment.GetEnvironmentVariable("Cosmos_Db")!).UseLazyLoadingProxies();
        });
        


        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var dbcontextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
        using var context = dbcontextFactory.CreateDbContext();
        context.Database.EnsureCreated();
    })
    .Build();

host.Run();
