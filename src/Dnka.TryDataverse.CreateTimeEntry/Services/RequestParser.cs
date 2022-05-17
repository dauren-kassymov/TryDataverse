using Dnka.TryDataverse.CreateTimeEntry.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.CreateTimeEntry.Services
{
    public class RequestParser : IRequestParser
    {
        public async Task<CreateTimeEntryRequest> Parse(HttpRequest req)
        {
            using var sr = new StreamReader(req.Body);
            string requestBody = await sr.ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<CreateTimeEntryRequest>(requestBody);

            return request;
        }
    }
}
