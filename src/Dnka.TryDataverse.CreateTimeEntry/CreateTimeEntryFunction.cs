using Dnka.TryDataverse.Core.Contract;
using Dnka.TryDataverse.Core.Model;
using Dnka.TryDataverse.CreateTimeEntry.Model;
using Dnka.TryDataverse.CreateTimeEntry.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.CreateTimeEntry
{
    public class CreateTimeEntryFunction
    {
        private readonly IDataverseService _dataverseService;
        private readonly ITimeEntryBuilder _timeEntryBuilder;

        public CreateTimeEntryFunction(
            IDataverseService dataverseService,
            ITimeEntryBuilder timeEntryBuilder
            )
        {
            _dataverseService = dataverseService;
            _timeEntryBuilder=timeEntryBuilder;
        }

        [FunctionName("CreateTimeEntry")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "timeentry")] HttpRequest req,
            ILogger logger)
        {
            logger.LogInformation("Request Started");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<CreateTimeEntryRequest>(requestBody);

            IEnumerable<TimeEntryEntity> entities = _timeEntryBuilder.Build(request);
            var list = entities.ToList();
            await _dataverseService.CreateTimeEntries(list);

            return new OkResult();
        }
    }
}
