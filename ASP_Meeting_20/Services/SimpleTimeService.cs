using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Meeting_20.Services
{
    public class SimpleTimeService : ITimeService
    {
        public string Time { get; }
        public SimpleTimeService()
        {
            Time = DateTime.Now.ToLongTimeString();
        }
    }
}
