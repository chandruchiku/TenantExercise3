using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantExercise3.Models;

namespace TenantExercise3.Respository
{
    public class InMemoryTenantStore : ITenantStore<Tenant>
    {
        /// <summary>
        /// Get a tenant for a given identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<Tenant> GetTenantAsync(string identifier)
        {
            var tenant = new[]
                {
                new Tenant{ Id = "80fdb3c0-5888-4295-bf40-ebee0e3cd8f3", HostName = "localhost" },
                new Tenant{ Id = "b0ed668d-7ef2-4a23-a333-94ad278f45d7", HostName = "tcl.com" }
            }.SingleOrDefault(t => t.Id == identifier);

            return await Task.FromResult(tenant);
        }
    }
}
