using System.Net.Http;

namespace Dnka.TryDataverse.Core.Service
{
    public abstract class BaseRequest<TOut>
    {
        public abstract HttpMethod HttpMethod { get; }

        public abstract string GetRequest();

        public abstract object GetBody();
    }
}
