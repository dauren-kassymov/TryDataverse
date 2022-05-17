using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace Dnka.TryDataverse.Core.Model
{
    [EntityLogicalNameAttribute("msdyn_timeentry")]
    public class TimeEntryEntity : Entity
    {
        public DateTime Start
        {
            get
            {
                return GetOrInit("msdyn_start");
            }
            set
            {
                AddOrSet("msdyn_start", value);
            }
        }

        public DateTime End
        {
            get
            {
                return GetOrInit("msdyn_end");
            }
            set
            {
                AddOrSet("msdyn_end", value);
            }
        }

        private DateTime GetOrInit(string prop)
        {
            if (Attributes.TryGetValue(prop, out object value))
            {
                return (DateTime)value;
            }
            else
            {
                var newVal = new DateTime();
                AddOrSet(prop, newVal);
                return newVal;
            }
        }

        private void AddOrSet(string prop, DateTime newVal)
        {
            if (Attributes.Contains(prop))
                Attributes[prop] = newVal;
            else
                Attributes.Add(prop, newVal);

        }
    }
}
