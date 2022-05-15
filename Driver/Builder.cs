using System;
using System.Collections.Generic;

namespace Driver
{
    internal class Builder
    {
        internal static List<DateTime> Build(RangeRequest req)
        {
            return new List<DateTime>
            {
                req.StartOn,
                req.StartOn.AddDays(1),
            };
        }
    }
}