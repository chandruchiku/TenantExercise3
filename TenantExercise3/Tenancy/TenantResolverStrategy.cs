using Autofac.Multitenant;
using Microsoft.AspNetCore.Http;
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
            tenantId = null;
            var context = httpContextAccessor.HttpContext;
            if (context == null)
                return false;
            var hostName = context?.Request?.Headers["tenantid"];
            tenantId = hostName;
            return (tenantId != null || tenantId == (object)"");
        }
    }
}
