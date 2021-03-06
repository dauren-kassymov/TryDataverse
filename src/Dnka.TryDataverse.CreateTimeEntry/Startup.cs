using Dnka.TryDataverse.Core.Contract;
using Dnka.TryDataverse.Core.Service;
using Dnka.TryDataverse.CreateTimeEntry;
using Dnka.TryDataverse.CreateTimeEntry.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Dnka.TryDataverse.CreateTimeEntry
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IRequestParser, RequestParser>();
            builder.Services.AddScoped<ITimeEntryBuilder, TimeEntryBuilder>();
            builder.Services.AddScoped<ITimeEntryService, TimeEntryService>();
            builder.Services.AddScoped<ITimeEntryRepository, TimeEntryRepository>(s => new TimeEntryRepository(Environment.GetEnvironmentVariable("DataverseConnection")));
        }
    }
}
