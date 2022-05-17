using Dnka.TryDataverse.Core.Model;
using Dnka.TryDataverse.CreateTimeEntry.Model;
using System;
using System.Collections.Generic;

namespace Dnka.TryDataverse.CreateTimeEntry.Services
{
    class TimeEntryBuilder : ITimeEntryBuilder
    {
        public IEnumerable<TimeEntryEntity> Build(CreateTimeEntryRequest request)
        {
            if (request.StartOn > request.EndOn)
            {
                throw new ArgumentException("StartOn should be less than EndOn");
            }

            var end = request.EndOn.ToUniversalTime();
            var date = request.StartOn.ToUniversalTime().Date;
            while (date < end)
            {
                yield return new TimeEntryEntity
                {
                    Start = date,
                    End = date.AddDays(1),
                };
                date = date.AddDays(1);
            }
        }
    }
}
