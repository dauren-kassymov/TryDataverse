using Dnka.TryDataverse.CreateTimeEntry.Model;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.CreateTimeEntry.Services
{
    public interface IRequestParser
    {
        Task<CreateTimeEntryRequest> Parse(HttpRequest req);
    }
}
