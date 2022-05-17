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
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.CreateTimeEntry
{
    public class TimeEntryFunction
    {
        private readonly ITimeEntryService _dataverseService;
        private readonly ITimeEntryBuilder _timeEntryBuilder;
        private readonly IRequestParser _requestParser;


        public TimeEntryFunction(
            ITimeEntryService dataverseService,
            ITimeEntryBuilder timeEntryBuilder,
            IRequestParser requestParser)
        {
            _dataverseService = dataverseService;
            _timeEntryBuilder=timeEntryBuilder;
            _requestParser=requestParser;
        }

        [FunctionName("TimeEntry")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "timeentries")] HttpRequest req,
            ILogger logger)
        {
            logger.LogInformation("Request Started");

            try
            {
            
                CreateTimeEntryRequest request = await _requestParser.Parse(req);
                IEnumerable<TimeEntryEntity> entities = _timeEntryBuilder.Build(request);
                IEnumerable<Guid> ids = await _dataverseService.CreateTimeEntriesAsync(entities);

                return new ObjectResult(new CreateTimeEntryResponse
                {
                    TimeEntryIds = ids
                })
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured", ex);
                var error = new CreateTimeEntryErrorResponse { Error = ex.Message };
                return new BadRequestObjectResult(error);
            }
        }
    }
}
