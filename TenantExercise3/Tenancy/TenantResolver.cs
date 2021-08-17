using Autofac.Multitenant;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantExercise3.Models;
using TenantExercise3.Respository;

namespace TenantExercise3.Tenancy
{
    public class TenantResolver : ITenantResolver
    {
        private readonly ITenantIdentificationStrategy tenantIdentificationStrategy;
        private readonly IMemoryCache memoryCache;
        private readonly ITenantStore<Tenant> tenantService;
        public TenantResolver(
          ITenantIdentificationStrategy tenantIdentificationStrategy,
          IMemoryCache memoryCache,
          ITenantStore<Tenant> tenantService
        )
        {
            this.tenantIdentificationStrategy = tenantIdentificationStrategy;
            this.memoryCache = memoryCache;
            this.tenantService = tenantService;
        }
        public async Task<Tenant> ResolveAsync(object tenantId)
        {
            Tenant tenant;
            var hostName = (string)tenantId;
            if (memoryCache.TryGetValue(hostName, out object cached))
            {
                tenant = (Tenant)cached;
            }
            else
            {
                tenant = await tenantService.GetTenantAsync(hostName);
            }
            return tenant ?? new Tenant();
        }
    }
}
