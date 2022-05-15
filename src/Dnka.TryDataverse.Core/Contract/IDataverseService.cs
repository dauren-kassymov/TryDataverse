using Dnka.TryDataverse.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.Core.Contract
{
    public interface IDataverseService
    {
        Task<IEnumerable<Guid>> CreateTimeEntries(IEnumerable<TimeEntryEntity> entities);
    }
}
