using FileCreateWorkerService;
using RabbitMQ.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using FileCreateWorkerService.Services;
using FileCreateWorkerService.Models;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {

        services.AddSingleton<RabbitMqClientsService>();
        IConfiguration configuration = hostContext.Configuration;
        services.AddDbContext<AdventureWorks2019Context>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
       
        services.AddSingleton(sp => new ConnectionFactory()
        {
            
            Uri = new Uri(configuration.GetConnectionString("RabbitMQ")),
            DispatchConsumersAsync = true
        });
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
