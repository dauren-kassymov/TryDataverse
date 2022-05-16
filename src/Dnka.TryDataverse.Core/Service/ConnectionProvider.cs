using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Dnka.TryDataverse.Core.Service
{
    public class ConnectionProvider
    {
        private readonly HttpClient _httpClient;

        public ConnectionProvider(HttpClient httpClient) => _httpClient = httpClient;

        public OdataResponse<T> ProcessRequest<T>(BaseRequest<T> baseRequest) where T : class
        {
            var requestUri = baseRequest.GetRequest();
            var content = baseRequest.GetBody();

            var request = new HttpRequestMessage(baseRequest.HttpMethod, requestUri);

            if (content != null)
            {
                request.Content = new StringContent(JsonSerializer.Serialize(content));
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            }

            var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();

            var data = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();

#if DEBUG
            var debugStringData = ReadResponseAsString(response);
            data.Position = 0;
#endif

            //TODO get Entity id from Location Header

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<OdataResponse<T>>(data)!;
            }

            var responseContent = ReadResponseAsString(response);

            var errorMessage = string.IsNullOrWhiteSpace(responseContent)
                ? $"Failed with a status of '{response.ReasonPhrase}'"
                : $"Failed with content: {responseContent.Replace("\"", string.Empty)}";

            throw new Exception(errorMessage);
        }

        private static string ReadResponseAsString(HttpResponseMessage message)
            => new StreamReader(message.Content.ReadAsStreamAsync().GetAwaiter().GetResult()).ReadToEnd();
    }
}
