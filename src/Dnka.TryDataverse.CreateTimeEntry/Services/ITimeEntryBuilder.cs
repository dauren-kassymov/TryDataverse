using Dnka.TryDataverse.Core.Model;
using Dnka.TryDataverse.CreateTimeEntry.Model;
using System.Collections.Generic;

namespace Dnka.TryDataverse.CreateTimeEntry.Services
{
    public interface ITimeEntryBuilder
    {
        /// <summary>
        /// Generate list of <see cref="TimeEntryEntity"/> between StartOn and EndOn
        /// </summary>
        IEnumerable<TimeEntryEntity> Build(CreateTimeEntryRequest request);
    }
}
