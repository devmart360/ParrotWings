using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;

namespace Devmart360.ParrotWings.EventModels
{
    public class UserCreatedEventData : EventData
    {
        public long UserId { get; set; }
    }
}
