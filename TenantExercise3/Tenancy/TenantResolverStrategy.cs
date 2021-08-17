using Autofac.Multitenant;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantExercise3.Tenancy
{
    public class TenantResolverStrategy : ITenantIdentificationStrategy
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public TenantResolverStrategy(
          IHttpContextAccessor httpContextAccessor
        )
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public bool TryIdentifyTenant(out object tenantId)
        {
            var context = httpContextAccessor.HttpContext;
            if (context == null)
            {
                // No current HttpContext. This happens during app startup
                // and isn't really an error, but is something to be aware of.
                tenantId = null;
                return false;
            }

            // Caching the value both speeds up tenant identification for
            // later and ensures we only see one log message indicating
            // relative success or failure for tenant ID.
            if (context.Items.TryGetValue("_tenantId", out tenantId))
            {
                // We've already identified the tenant at some point
                // so just return the cached value (even if the cached value
                // indicates we couldn't identify the tenant for this context).
                return tenantId != null;
            }

            if (context.Request.Headers.TryGetValue("tenantid", out StringValues tenantValues))
            {
                tenantId = tenantValues[0];
                context.Items["_tenantId"] = tenantId;
                // _logger.LogInformation("Identified tenant: {tenant}", tenantId);
                return true;
            }

            // if (context.Request.Query.TryGetValue("tenant", out StringValues tenantValues))
            // {
            //     tenantId = tenantValues[0];
            //     context.Items["_tenantId"] = tenantId;
            //     _logger.LogInformation("Identified tenant: {tenant}", tenantId);
            //     return true;
            // }

            // _logger.LogWarning("Unable to identify tenant from query string. Falling back to default.");
            tenantId = null;
            context.Items["_tenantId"] = null;
            return false;
        }
    }
}
