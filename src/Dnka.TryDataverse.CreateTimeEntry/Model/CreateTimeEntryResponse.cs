using System;
using System.Collections.Generic;

namespace Dnka.TryDataverse.CreateTimeEntry.Model
{
    public class CreateTimeEntryResponse
    {
        public IEnumerable<Guid> TimeEntryIds { get; set; }
    }
}
