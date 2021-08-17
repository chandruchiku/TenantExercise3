using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantExercise3.Models
{
    public class Tenant
    {
        public string Id { get; set; }
        public string HostName { get; set; }
        public string ConnectionString { get; set; }
    }
}
