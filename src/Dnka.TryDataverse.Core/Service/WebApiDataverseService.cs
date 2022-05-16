using Dnka.TryDataverse.Core.Contract;
using Dnka.TryDataverse.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.Core.Service
{
    public class WebApiDataverseService : IDataverseService
    {
        public Task<IEnumerable<Guid>> CreateTimeEntries(IEnumerable<TimeEntryEntity> entities)
        {
            const string tenantId = "ba20ff6a-75ef-4646-a751-484ddfcac7a8";
            const string clientId = "5844e71c-b693-4232-aa98-71360e0a8b40";
            const string clientSecret = "UEd8Q~C72n0WtesLt_CCWSykgKMd-Yf2633WkawC";
            const string scope = "https://org6506b57b.api.crm4.dynamics.com/";

            var authenticationHeader = AuthProvider.GetAuthHeader(tenantId, clientId, clientSecret, scope);
            Console.WriteLine($"authenticationHeader: {authenticationHeader}");

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri($"{scope}api/data/v9.2/");
            httpClient.DefaultRequestHeaders.Authorization = authenticationHeader;
            var cp = new ConnectionProvider(httpClient);
            //var r  = cp.ProcessRequest(new GetTimeEntriesRequest());

            var r2  = cp.ProcessRequest(new CreateTimeEntryRequest
            {
                Entity = entities.First()
            });

            var r  = cp.ProcessRequest(new GetTimeEntriesRequest());

            throw new Exception();
        }




        private class GetTimeEntriesRequest : BaseRequest<GetTimeEntriesResponse[]>
        {
            public override HttpMethod HttpMethod => HttpMethod.Get;

            public override object GetBody() => null;

            public override string GetRequest() => "msdyn_timeentries?$select=msdyn_start&$top=10";
        }

        private class GetTimeEntriesResponse
        {
            [JsonPropertyName("msdyn_timeentryid")]
            public string Id { get; set; }

            [JsonPropertyName("msdyn_start")]
            public DateTime Start { get; set; }
        }

        private class CreateTimeEntryRequest : BaseRequest<GetTimeEntriesResponse[]>
        {
            public override HttpMethod HttpMethod => HttpMethod.Post;

            public override object GetBody() => Entity;

            public override string GetRequest() => "msdyn_timeentries";

            public TimeEntryEntity Entity { get; set; }
        }

        private class CreateTimeEntryResponse
        {
            [JsonPropertyName("msdyn_timeentryid")]
            public string Id { get; set; }

            [JsonPropertyName("msdyn_start")]
            public DateTime Start { get; set; }
        }
    }

}
