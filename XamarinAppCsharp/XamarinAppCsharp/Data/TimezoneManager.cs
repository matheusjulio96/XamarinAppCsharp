using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinAppCsharp.Models;

namespace XamarinAppCsharp.Data
{
    public class TimezoneManager
    {
        TimezoneService restService;

        public TimezoneManager(TimezoneService service) // aqui usaria uma interface
        {
            restService = service;
        }

        public Task<Timezone> GetAsync()
        {
            return restService.GetTimezoneAsync();
        }
    }
}
