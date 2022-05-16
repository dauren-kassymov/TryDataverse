using Dnka.TryDataverse.Core.Model;
using Dnka.TryDataverse.CreateTimeEntry.Model;
using System.Collections.Generic;

namespace Dnka.TryDataverse.CreateTimeEntry.Services
{
    public interface ITimeEntryBuilder
    {
        IEnumerable<TimeEntryEntity> Build(CreateTimeEntryRequest request);
    }
}
